
using System;
namespace NBehaviour.NCondition
{
	public class Condition
	{
		public Condition (Entity entity)
		{
		}
		public virtual int Process(Entity entity, Room room,int bhvProcessResult){
			return bhvProcessResult;
		}
	}
}

