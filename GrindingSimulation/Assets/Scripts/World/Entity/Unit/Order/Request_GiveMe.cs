using UnityEngine;
using System.Collections;


namespace NWorld.NEntity.NUnit.NOrder {
	
	public class Request_GiveMe:Order
	{
		Entity requested;
		int[] arguments;
		public Request_GiveMe(World world, Entity entity, Entity other,params int[] arguments):base(world,entity){
			this.requested = other;
			this.arguments = arguments;
		}
		public override void UpdateInit_Process (World world, Unit unit)
		{
			base.UpdateInit_Process (world, unit);
			requested.RequestGiveMe (unit, arguments); 

		}	

	}
}

