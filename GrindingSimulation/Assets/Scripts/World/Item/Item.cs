using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NItem{
	
	public class Item
	{

		internal KEnums.IdItem id;
		internal List<KEnums.TypeItem> type = new List<KEnums.TypeItem>();
		internal KEnums.TypeItemEquip typeEquip= KEnums.TypeItemEquip.UnEquippable;
		internal bool isStackable = false;
		internal List<NProperty.Property> properties = new List<NProperty.Property>();

		internal float value = 0;
		internal Price price = new Price(); // price is used when it comes to trading
		internal int Count = 1;


		internal string 
			name = "Unknown", 
			description = "Description unavailable.";

		public static Item GetRandomItem(float value){
			return ItemList.GetJewerly_Red ();
			//return null;
		}
		public bool IsSameType(Item other){
			return IsSameType (other.type);
		}
		public bool IsSameType(params KEnums.TypeItem[] type){
			for(int i = 0 ; i < type.Length;i++){
				bool isTypeSame = false;
				for(int j = 0 ; j < this.type.Count;j++){
					if(type[i] == this.type[j]){
						isTypeSame = true;
						break;
					}
				}
				if(!isTypeSame) return false;
			}
			return true;

		}
		public bool IsSameType(List<KEnums.TypeItem> type){
			for(int i = 0 ; i < type.Count;i++){
				bool isTypeSame = false;
				for(int j = 0 ; j < this.type.Count;j++){
					if(type[i] == this.type[j]){
						isTypeSame = true;
						break;
					}
				}
				if(!isTypeSame) return false;
			}
			return true;
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
		public virtual float GetValue(){
			return this.value * this.Count;
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