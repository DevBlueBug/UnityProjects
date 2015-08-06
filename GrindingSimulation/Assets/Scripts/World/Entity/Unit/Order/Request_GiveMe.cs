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
		public override void Update_Process (World world, Unit unit)
		{
			this.stateMe = (requested.RequestGiveMe (unit, arguments)) ? State.Success : State.Failure; 
		}

	}
}

