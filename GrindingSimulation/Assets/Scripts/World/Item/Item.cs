using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NItem{
	
	public class Item
	{
		internal KEnums.IdItem id;
		internal List<KEnums.TypeItem> type = new List<KEnums.TypeItem>();
		internal KEnums.TypeItemEquip typeEquip= KEnums.TypeItemEquip.UnEquippable;
		internal float value = 0;
		internal int Count = 1;

		internal bool isStackable = false;
		internal List<NProperty.Property> properties = new List<NProperty.Property>();

		internal string 
			name = "Unknown", 
			description = "Description unavailable.";

		public static Item GetRandomItem(float value){
			return GetJewerly ();
			//return null;
		}
		public static Item GetJewerly(){
			Item item = new Item ();
			item.id = KEnums.IdItem.Jewerly_Red;
			item.value = 10;
			item.isStackable = true;
			return item;
		}
		public bool IsSame(Item other){
			if (other.id != this.id) return false;
			for(int i = 0 ; i < other.type.Count;i++){
				bool isTypeSame = false;
				for(int j = 0 ; j < this.type.Count;j++){
					if(other.type[i] == this.type[j]){
						isTypeSame = true;
						break;
					}
				}
				if(!isTypeSame) return false;
			}
			return true;
		}
		public Item SetType(KEnums.TypeItem type){
			this.type.Add (type);
			return this;
		}
		public Item SetType(KEnums.TypeItemEquip type){
			this.typeEquip = type;
			return this;
		}



	}
	

}