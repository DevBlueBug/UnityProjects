using UnityEngine;
using System.Collections;

namespace Game.Entity.Behavior
{
	public class GBhvLookAtPlayer : GBhvTimer
	{

		public override void Do (GEntity entity, GRoom room)
		{
			var dir = GPlayer.PlayerPosition - entity.position;
			int x = (int)dir.x, y = (int)dir.z;
			//Debug.Log (x + " " + y);

			if(Mathf.Abs(x) > Mathf.Abs(y) ){
				//x
				if(x >0){
					entity.rotation = GInfo.dirLook[1];
				}
				else{
					entity.rotation = GInfo.dirLook[3];
				}
			}
			else{
				if(y > 0){
					entity.rotation = GInfo.dirLook[0];
				}
				else{
					entity.rotation = GInfo.dirLook[2];
				}
				//y
			}

		}
	}
}