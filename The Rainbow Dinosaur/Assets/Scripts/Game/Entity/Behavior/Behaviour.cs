using System;
using System.Collections.Generic;
using NBehaviour.NCondition;
namespace NBehaviour{
	
	public class Behaviour
	{
		public bool isAlive = true;

		public Behaviour 
			bhvParent,
			bhvSuccess,bhvFail;
		public List<Behaviour> others = new List<NBehaviour.Behaviour>();
		public  NCondition.Condition condition;
		
		public Behaviour(){
			condition = new Condition (null);
		}
		public Behaviour(Entity entity){
			condition = new Condition (entity);
		}
		//  1 success
		// -1 fail
		// -2 back to parent
		// 0 : null : nothing happens is not supposed to happen... no implementation
		public Behaviour Update(Entity entity, Room room){
			var result = condition.Process(entity,room, Process (entity, room));
			//UnityEngine.Debug.Log("returning " +result);
			if (result == 1 && bhvSuccess != null) {
				return bhvSuccess;
			} else if (result == -1&& bhvFail != null) {
				return bhvFail;
			} else if (result == -2&& bhvParent != null) {
				return bhvParent;
			}
			return null;
		}
		public virtual int Process(Entity entity, Room room){
			return 0;
		}
		public void Link(Behaviour child){
			child.bhvParent = this;
		}

	}

}