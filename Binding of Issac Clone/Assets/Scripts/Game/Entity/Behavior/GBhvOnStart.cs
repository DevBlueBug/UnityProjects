using UnityEngine;
using System.Collections.Generic;

namespace Game.Entity.Behavior{
	
	public class GBhvOnStart : GBehavior
	{
		public List<GBehavior> bhvsInit;
		public override void Do (GEntity entity, GRoom room)
		{
			base.Do (entity, room);
			this.isAlive = false;
			this.isContinueUpdating = true;
			foreach (var b in bhvsInit) {
				b.Init(entity);
				b.KUpdate(entity,room);
			}


		}
	}

}
