using UnityEngine;
using System.Collections.Generic;
namespace NBehaviour
{
	public class BhvFollowPlayerFlyWave :NBehaviour.Behaviour
	{
		Utility.EasyTimer timer;
		Vector3 direction,directionFinal;
		float cos = 0.0f,cosChange,power;
		
		public BhvFollowPlayerFlyWave(Entity Entity,float waveChange,float power):base(Entity){
			timer = new Utility.EasyTimer (10, .51f);
			this.cosChange = waveChange *(3.14f/180.0f);
			this.power = power;
			
		}
		
		public override int Process (Entity entity, Room room)
		{
			var entityMove = (EntityMove) entity;
			cos += cosChange *Time.deltaTime;
			//directionFinal = direction + new Vector3(Mathf.Cos(cos),Mathf.Sin(cos),0) *power ;
			directionFinal = direction + new Vector3(-direction.y,direction.x,0) *Mathf.Cos(cos) *power ;
			directionFinal.Normalize();
			if (timer.Tick (UnityEngine.Time.deltaTime)) {
				direction = (PlayerManager.PlayerPosFloat- entity.transform.localPosition)
					.normalized;
			}

			entityMove.Move(directionFinal);
			return 0;
			
		}
	}
}

