
using System;
namespace NBehaviour.NCondition
{
	public class CdtCustomize:Cdt
	{
		public delegate int D_Process(Entity entity, Room room, int bhvProcessResul);
		public D_Process E_Process = delegate {return 0;};
		public CdtCustomize(Entity entity):base(entity){
		}
		public override int Process (Entity entity, Room room, int bhvProcessResult)
		{
			return E_Process (entity, room, bhvProcessResult);
			//return base.Process (entity, room, bhvProcessResult);
		}
	}
}

