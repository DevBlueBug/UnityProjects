using UnityEngine;
using System.Collections.Generic;

using Utility;
namespace Game.Entity.Behavior{
	
	public class GBhvTimer : GBehavior
	{
		public float timeInit,time;
		public List<GBehavior> ticks;
		internal EasyTimer timer = null;
		public override void Awake ()
		{
			base.Awake ();
			timer = new EasyTimer (0,time);
		}

		public override void Init (GEntity entity)
		{
			base.Init (entity);
			foreach (var b in ticks) b.Init (entity);
		}
		public override void KUpdate (GEntity entity, GRoom room)
		{
			base.KUpdate (entity, room);
			if (timer.Tick (Time.deltaTime)) {
				UpdateTheRest(ticks,entity,room);
			}
		}
	}

}
