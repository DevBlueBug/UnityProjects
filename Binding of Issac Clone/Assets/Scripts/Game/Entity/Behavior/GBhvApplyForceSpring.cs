//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
namespace Game.Entity.Behavior
{
	public class GBhvApplyForceSpring : GBehavior
	{
		public GameObject springCenter;
		public float force, springLength;
		public override void Do (GEntity entity, GRoom room)
		{
			base.Do (entity, room);
			var dir = (entity.transform.position - springCenter.transform.position);
			//entity.body.position += ( dir.normalized * (entity.velo + force) * Time.fixedDeltaTime );
			//return;
			var ratio = (springLength-dir.magnitude)/springLength;
			//Debug.Log (ratio);
			entity.AddForce(new Vector3 (dir.x, 0, dir.z).normalized*(ratio*ratio)*force);
		}
	}
}

