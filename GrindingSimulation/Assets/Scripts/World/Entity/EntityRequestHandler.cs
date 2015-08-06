using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NEntity{
	
	
	public class EntityRequestHandler : MonoBehaviour
	{
		public static Entity LinkBasic(Entity entity){
			entity.E_ReceiveRequestGiveMe = H_GiveMe_Default;
			entity.E_ReceiveRequestTakeIt = H_TakeIt_Default;
			entity.E_ReceiveRequestShowMe = H_ShowMe_Default;
			entity.E_ReceiveRequestPayMe = H_PayMe_Default;
			return entity;
		}
		public static bool H_GiveMe_Default(Entity me, Entity requested, params int[] arguments){
			//if no arguments, then try to give all
			if (arguments.Length == 0) {
				arguments = new int[me.inventory.Count];
				for(int i = 0; i < arguments.Length ; i++){
					arguments[i] = (arguments.Length -1)-i;
				}
			}
			return requested.RequestTakeIt (me, arguments);
		}
		
		public static bool H_TakeIt_Default(Entity me, Entity requested, params int[] arguments){
			//var item = (NItem.Item) arguments[0];
			//return me.inventory.Add (item);
			bool isAdded = false;
			for (int i = arguments.Length-1; i > -1; i--) {
				bool isAddedInThisCycle = false;
				int index = (int)arguments[i];
				var item = requested.inventory[index];
				var result = me.inventory.Add(item);
				switch(result){
				case 1:
					requested.inventory.Remove(index);
					isAddedInThisCycle = true;
					break;
					
				case 2:
					//in case it is 2, you could need to implement other thingings
					isAddedInThisCycle = true;
					break;
				case -1:
					isAddedInThisCycle = false;
					break;
				}

				if(isAddedInThisCycle){
					isAdded = true;
				}
				else if(!isAdded){
					return false;
				}
			}
			return isAdded;

		}
		public static bool H_ShowMe_Default(Entity me, Entity other, params System.Object[] arguments){
			//add items that you want to show to the list
			var list = (List<NItem.Item>)arguments[0];
			var condition = (RequestItem_Condition)arguments[1];
			for (int i = 0; i < me.inventory.Count; i++) {

			}
			return false;
		}
		static int helperPay(NItem.Inventory inventory, NItem.Price.Pair pair){ 
			for (int i = 0; i< inventory.Count; i++) {
				if(inventory[i].id == pair.id){
					if(inventory[i].Count >= pair.count) return i;
					else return -1;

				}
			}
			return -1;
		}
		public static bool H_PayMe_Default(Entity me, Entity other, NItem.Price price){
			List<int> indexs = new List<int> ();

			for (int i = 0; i < price.prices.Count; i++) {

				for(int j = 0 ; j < me.inventory.Count;j++){
					int result = helperPay(me.inventory, price.prices[i] );
					if(result == -1 )return false;
					indexs.Add(j);
					break;
				}
			}
			for (int i = 0; i < indexs.Count; i++) {
				//Debug.Log(me.inventory[indexs[i] ].Count);
				//Debug.Log( price.prices[i].count);
				me.inventory[indexs[i] ].Count -= price.prices[i].count;
			}
			me.inventory.Update ();
			return true;
		}
		public static bool H_SellMe_Default(Entity me, Entity other, NItem.ItemChecker check){
			
			//Debug.Log ("TRYING TO SELL ");
			int index = -1;
			float price = 0;
			for (int i  = 0; i < me.inventory.Count; i++) {
				if(check.IsItemValid(me.inventory[i])){
					//get the value of the item then provide with the highest scored item
					var priceNew = me.inventory[i].GetValue() / me.inventory[i].price.GetSum();
					if(priceNew > price) {
						index = i;
						price = priceNew;
					}

				}
			}
			if (index == -1) {
				//Debug.Log ("FAIL : INDEX NOT FOUND ");
				return false;
			}
			if(other.RequestPayMe(me,me.inventory[index].price) ){
				if(other.RequestTakeIt(me,index) ) {
					//Debug.Log ("SOLD TAKEN ");
					return true;
				}
			}
			//Debug.Log ("FAIL : WHY? ");
			return false;
		}
	}
	

}