using System;
namespace NBehaviour.NCondition
{
	public class CdtCdtHpChanged :Cdt
	{
		float hpBefore;
		public CdtCdtHpChanged (Entity entity):base(entity)
		{
			hpBefore = entity.hp;
		}
		public override int Process (Entity entity, Room room, int bhvProcessResult)
		{
			if (hpBefore > entity.hp)
				return -1;
			else if (hpBefore < entity.hp)
				return 1;
			return 0;
		}
	}
}

