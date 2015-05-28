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
			timer = new Utility.EasyTimer (0, .51f);
		}

		public override int Process (Entity entity, Room room)
		{
			var entityMove = (EntityMove) entity;
			if (timer.Tick (UnityEngine.Time.deltaTime)) {
				//renew the stack
				UpdateDestinations(entity,room);
			}
			if (destination == null) return -1;
			//Debug.Log ("DESTINATION GIVEN AT " + destination);
			var dir = destination.Value - entity.transform.localPosition;
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
				Debug.Log("FAILED TO FIND THE PATH");
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

