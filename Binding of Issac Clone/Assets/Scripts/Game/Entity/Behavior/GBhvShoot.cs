using UnityEngine;
using System.Collections;

namespace Game.Entity.Behavior{
	
	public class GBhvShoot : GBehavior
	{
		public GEntity myBullet;
		public GameObject posFrom;
		public override void Init (GEntity entity)
		{
			base.Init (entity);
		}

	}
	

}