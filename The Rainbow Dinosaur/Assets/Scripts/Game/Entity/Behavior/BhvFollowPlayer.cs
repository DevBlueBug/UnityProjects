using UnityEngine;
using System.Collections.Generic;
namespace NBehaviour
{
	public class BhvFollowPlayer :NBehaviour.Behaviour
	{
		Utility.EasyTimer timer;
		Stack<Vector3> destinations = new Stack<Vector3>();
		Vector3? destination = null;

		public BhvFollowPlayer(Entity Entity):base(Entity){
			timer = new Utility.EasyTimer (0, .50f);
		}

		public override int Process (Entity entity, Room room)
		{
			var entityMove = (EntityMove) entity;
			if (timer.Tick (UnityEngine.Time.deltaTime)) {
				//renew the stack
				UpdateDestinations(entity,room);
			}
			if (destination == null) return -1;
			var dir = destination.Value - new Vector3(entity.transform.localPosition.x,entity.transform.localPosition.y,0);
			//Debug.Log ("DESTINATION GIVEN AT " + destination + " to " + entity.transform.localPosition 
			//           + " AND MAGNITUDE WOULD BE " + dir.magnitude);
			entityMove.SetVelocity (dir.normalized * entityMove.velo	);

			if ( dir.sqrMagnitude < .05f) {
				if(destinations.Count >0 ) destination = destinations.Pop();
				else destination = null;
			}
			return 0;
		
		}
		bool UpdateDestinations(Entity entity,Room room){
			destinations = ((EntityMove)entity).GetPathToPlayer (room);
			if (destinations == null) {
				Debug.Log("FAILED TO FIND THE PATH " + PlayerManager.PlayerPosInt + " " +PlayerManager.PlayerPosFloat);
				Debug.Log(room.aStarMap[(int)PlayerManager.PlayerPosInt.x,(int)PlayerManager.PlayerPosInt.y].isAlive);
				destination = null;
				return false;
			}
			else if (destinations.Count > 0)
				destination = destinations.Pop ();
			else {
				destination = PlayerManager.PlayerPosFloat;
			}
			return true;
		}
	}
}

