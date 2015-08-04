using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NEntity{
	
	
	public class EntityRequestHandler : MonoBehaviour
	{
		public static Entity LinkBasic(Entity entity){
			entity.E_ReceiveRequestGiveMe = H_GiveMe_Default;
			entity.E_ReceiveRequestTakeIt = H_TakeIt_Default;
			entity.E_ReceiveRequestShowMe = H_ShowMe_Default;
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
		public static bool IsItemValid(NItem.Item item, RequestItem_Condition condition){
			return false;
		}
		public static bool H_ShowMe_Default(Entity me, Entity other, params System.Object[] arguments){
			//add items that you want to show to the list
			var list = (List<NItem.Item>)arguments[0];
			var condition = (RequestItem_Condition)arguments[1];
			for (int i = 0; i < me.inventory.Count; i++) {

			}
			return false;
		}
	}
	

}