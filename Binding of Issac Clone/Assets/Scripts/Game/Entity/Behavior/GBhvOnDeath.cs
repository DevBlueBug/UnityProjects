//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Collections.Generic;
namespace Game.Entity.Behavior
{
	public class GBhvOnDeath : GBehavior
	{
		public List<GBehavior> behaviorsOnDeath;
		public override void Init (GEntity entity)
		{
			base.Init (entity);
		}

		public override void KUpdate (GEntity entity, GRoom room)
		{
			base.KUpdate (entity, room);
			if (entity.hp <= 0) {
				//is going to die
				foreach (var bhv in behaviorsOnDeath) {
					bhv.Init(entity);
					bhv.KUpdate(entity,room);
				}
			}
		}
	}
}
