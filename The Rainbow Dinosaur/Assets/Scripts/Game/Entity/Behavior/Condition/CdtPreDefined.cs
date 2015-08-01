
using System;
namespace NBehaviour.NCondition
{
	public class CdtPreDefined:Cdt
	{
		int result;
		public CdtPreDefined(Entity entity, int n):base(entity){
			this.result = n;
		}
		public override int Process (Entity entity, Room room, int bhvProcessResult)
		{
			return result;
		}
	}
}

