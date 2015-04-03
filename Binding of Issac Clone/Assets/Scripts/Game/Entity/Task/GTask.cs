using System;
using System.Collections.Generic;

namespace Game.Entity.Task
{
	public class GTask
	{
		public bool isAlive =true;

		public List<GTask> otherTasks = new List<GTask>();
		public void KUpdate(GEntity entity, GRoom room){
			isAlive = TestIsAlive (entity, room);
			if (!isAlive) return;
			for(int i = 0 ; i < otherTasks.Count;i++){
				otherTasks[i].KUpdate(entity,room);
			}
			Do (entity, room);
		}
		public virtual bool TestIsAlive(GEntity entity, GRoom room){
			return isAlive;
		}
		public virtual void Do(GEntity entity, GRoom room){
		}
	}
}

