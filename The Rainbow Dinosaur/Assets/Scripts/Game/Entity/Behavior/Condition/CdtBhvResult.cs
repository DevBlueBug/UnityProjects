using System;
namespace NBehaviour.NCondition
{
	public class CdtBhvResult :Cdt
	{
		public CdtBhvResult (Entity entity):base(entity)
		{
		}
		public override int Process (Entity entity, Room room, int bhvProcessResult)
		{
			return bhvProcessResult;
		}
	}
}

