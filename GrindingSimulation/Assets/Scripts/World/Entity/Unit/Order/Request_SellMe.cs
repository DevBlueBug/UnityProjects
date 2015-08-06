using UnityEngine;
using System.Collections;


namespace NWorld.NEntity.NUnit.NOrder {
	
	public class Request_SellMe:Order
	{
		Entity requested;
		NWorld.NItem.ItemChecker check;
		public Request_SellMe(World world, Entity entity, Entity other, NWorld.NItem.ItemChecker check):base(world,entity){
			this.requested = other;
			this.check = check;
		}
		
		public override void Update_Process (World world, Unit unit)
		{
			this.stateMe = (requested.RequestSellMe (unit, check)) ? State.Success : State.Failure; 
		}

		
	}
}

