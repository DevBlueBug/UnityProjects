using System;
namespace NBehaviour.NCondition
{
	public class HpChanged_Threshold :Condition
	{
		float hpBefore,hpChangeNeeded;
		public HpChanged_Threshold (Entity entity, int hpChangeNeeded):base(entity)
		{
			hpBefore = entity.hp;
		}
		public override int Process (Entity entity, Room room, int bhvProcessResult)
		{
			if ( entity.hp - hpBefore <= hpChangeNeeded)
				return 1;
			return 0;
		}
	}
}

