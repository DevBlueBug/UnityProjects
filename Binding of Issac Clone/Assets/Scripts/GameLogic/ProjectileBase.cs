using UnityEngine;
using System.Collections;

namespace GameLogic.Entity.Unit {
	public class ProjectileBase : UnitBase
	{
		public Collider myCollider;
		void OnTriggerEnter(Collider c){
			body.velocity = Vector3.zero;
			body.useGravity = false;
		}	
	}
}