using UnityEngine;
using System.Collections.Generic;


namespace NWorld.NUnit.NOrder {
	
	public class Request : Order
	{
		Entity entityRequested;
		KEnums.TypeRequest request;
		public Request(World world, Entity entity, Entity entityGetRequested, KEnums.TypeRequest request):base(world,entity){
			this.entityRequested = entityGetRequested;
			this.request = request;
		}

		public override void Update_Process (World world, Unit unit)
		{
			entityRequested.Requested (unit, request);
		}

	}
	
}
