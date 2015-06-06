using UnityEngine;
using System.Collections.Generic;
namespace NBehaviour
{
	public class BhvFollowPlayerFly :NBehaviour.Behaviour
	{
		Utility.EasyTimer timer;
		Vector3 direction;
		
		public BhvFollowPlayerFly(Entity Entity):base(Entity){
			timer = new Utility.EasyTimer (10, .51f);
		
		}
		
		public override int Process (Entity entity, Room room)
		{
			var entityMove = (EntityMove) entity;
			if (timer.Tick (UnityEngine.Time.deltaTime)) {
				direction = (PlayerManager.PlayerPosFloat- entity.transform.localPosition)
					.normalized;
			}
			entityMove.Move(direction);
			return 0;
			
		}
	}
}

