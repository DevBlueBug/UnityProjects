
using System;
namespace NBehaviour.NCondition
{
	public class Condition
	{
		internal bool isOr = false; //if isOr give second chance to the next condition;
		public Condition conditionNext;
		public Condition (Entity entity)
		{
		}
		public Condition SetOr(Condition condition){
			isOr = true;
			conditionNext = condition;
			return this;
		}
		public int IsCondition(Entity entity, Room room,int bhvProcessResult){
			var result = Process (entity, room, bhvProcessResult);
			if (result == 0 && isOr ) {
				return conditionNext.IsCondition(entity,room,bhvProcessResult);
			}
			return result;
		}
		public virtual int Process(Entity entity, Room room, int bhvProcessResult){
			return bhvProcessResult;
		}
	}
}

