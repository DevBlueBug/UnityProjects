using UnityEngine;
using System.Collections.Generic;
namespace NBehaviour
{
	public class BhvFireAtPlayer :NBehaviour.Behaviour
	{
		Utility.EasyTimer timer;
		
		public BhvFireAtPlayer(Entity Entity, float timeInterval):base(Entity){
			timer = new Utility.EasyTimer (0, timeInterval);
		}
		
		public override int Process (Entity entity, Room room)
		{
			if (timer.Tick (Time.deltaTime)) {
				entity.Attack(room, 
				              (PlayerManager.PlayerPosFloat- entity.transform.localPosition).normalized);
			}
			return 1;
		}
	}
}

