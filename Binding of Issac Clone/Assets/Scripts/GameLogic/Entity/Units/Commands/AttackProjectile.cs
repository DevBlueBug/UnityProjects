using UnityEngine;
using System.Collections;

namespace GameLogic.Entity.Unit.Action{
	public class AttackProjectile : AttackBase
	{
		public ProjectileBase projectile;
		public GameObject positionFrom;
		// Use this for initialization
		public override void Dir (Vector3 dir)
		{
			base.Dir (dir);
			var obj = Instantiate (projectile);
			//obj.MoveDir (dir);
			obj.transform.position = positionFrom.transform.position;
		}
	}
}
