using System;
using System.Collections.Generic;

namespace Game.Entity.Task
{
	public class GTask : IEntityComponent
	{
		public bool isAlive =true;
		public bool IsAlive{ get { return isAlive; } }
		public List<GTask> otehrs = new List<GTask>();
		//List<IEntityComponent> OthersToUpdate { get{return others;} }

		public virtual bool TestIsAlive (GEntity entity, GRoom room)
		{
			return true;
		}
		
		public virtual void Init(GEntity entity){
		}
		public void KUpdate(GEntity entity, GRoom room){
			isAlive = TestIsAlive (entity, room);
			if (!isAlive) return;
			
			for(int i = 0 ; i < otehrs.Count;i++){
				otehrs[i].KUpdate(entity,room);
			}
			Do (entity, room);
		}
		public virtual void Do(GEntity entity, GRoom room){
		}
	}
}

