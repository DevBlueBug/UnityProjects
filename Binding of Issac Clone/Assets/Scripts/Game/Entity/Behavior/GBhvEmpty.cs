using UnityEngine;
using System.Collections;

namespace Game.Entity.Behavior{
	
	public class GBhvEmpty : GBehavior
	{
		public D_Custom_Do E_Do = delegate {	};
		public override void Do (GEntity entity, GRoom room)
		{
			base.Do (entity, room);
			E_Do (entity, room);
		}
	}

}
