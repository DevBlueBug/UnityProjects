
using System.Collections;
namespace NBehaviour{

	public class BhvCustomize : Behaviour
	{
		public delegate int D_Process (Entity entity,Room room);
		public D_Process E_Process = delegate {return 0;};
		public override int Process (Entity entity, Room room)
		{
			return E_Process (entity, room);
			//return base.Process (entity, room);
		}
		public BhvCustomize SetMethod(D_Process method){
			this.E_Process = method;
			return this;
		}
		public BhvCustomize AddMethod(D_Process method){
			this.E_Process += method;
			return this;
		}

	}
}

