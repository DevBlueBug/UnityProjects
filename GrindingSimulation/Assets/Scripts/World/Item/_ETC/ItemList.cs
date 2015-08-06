using UnityEngine;
using System.Collections;


namespace NWorld.NItem{

	public class ItemList
	{
		public static Item GetJewerly_Red(){
			Item item = new Item ();
			item.id = KEnums.IdItem.Jewerly_Red;
			item.value = 10;
			item.isStackable = true;
			return item;
		}
		
		public static Item GetJewerly_Blue(){
			Item item = new Item ();
			item.id = KEnums.IdItem.Jewerly_Blue;
			item.value = 10;
			item.isStackable = true;
			return item;
		}
		public static float GetValue(KEnums.IdItem id){
			switch (id) {
			default: return 0;
			case KEnums.IdItem.Jewerly_Red:
			case KEnums.IdItem.Jewerly_Green:
			case KEnums.IdItem.Jewerly_Blue:
				return 10;
			}

		}

	}

}