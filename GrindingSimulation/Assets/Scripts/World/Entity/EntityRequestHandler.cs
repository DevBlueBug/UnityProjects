using UnityEngine;
using System.Collections;

namespace NWorld{
	
	
	public class EntityRequestHandler : MonoBehaviour
	{
		public static Entity LinkBasic(Entity entity){
			entity.E_ReceiveRequestGiveMe = H_GiveMe_Default;
			entity.E_ReceiveRequestTakeIt = H_TakeIt_Default;
			return entity;
		}
		public static bool H_GiveMe_Default(Entity me, Entity other, params System.Object[] arguments){
			for (int i = me.inventory.Count-1; i >-1; i--) {
				if(other.Requested(me, KEnums.TypeRequest.TakeIt, me.inventory[i])) {
					me.inventory.Remove(i);
				}else{
					return false;
				}
			}
			return true;
		}
		
		public static bool H_TakeIt_Default(Entity me, Entity other, params System.Object[] arguments){

			return false;
		}
	}
	

}