using UnityEngine;
using System.Collections;

namespace NWorld.NEntity.NNPC{
	
	public class Merchant : NPC
	{
		public Merchant(){
			this.E_ReceiveRequestSellMe = EntityRequestHandler.H_SellMe_Default;
		}
		public override NWorld.NTask.Task GetTask_GiveMe (Entity entity, NWorld.NItem.ItemChecker check)
		{
			// check
			// get bool if true, then get score
			// get the lowest costing item
			// return task with lowest costing item if such item exists

			NItem.Item itemChosen = null;
			float score = 0;
			for (int i = 0; i< inventory.Count; i++) {
				if(check.IsItemValid(inventory[i] ) ){
					var itemNew = inventory[i];
					var scoreNew = check.GetScore(itemNew);
					if(scoreNew != 0){
						if(itemNew.IsSameType(KEnums.TypeItem.Equipment))
						   scoreNew += ((NItem.NEquip.Equipment)itemNew).stats.GetTotalStats()
								/ itemNew.price.GetSum() ;
					}
					if(scoreNew > score){
						itemChosen = inventory[i];
						score = scoreNew;
					}
				}
			}
			if (itemChosen == null) return null;
			NTask.MerchantPurchase task = new NWorld.NTask.MerchantPurchase (this,check, score);

			return task;
		}
	}
	

}