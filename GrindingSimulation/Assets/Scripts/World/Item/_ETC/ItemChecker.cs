using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NItem{
	
	public class ItemChecker
	{

		internal List<KEnums.TypeItem> typeItem = new List<KEnums.TypeItem>();
		internal List<KEnums.TypeItemEquip> typeEquip = new List<KEnums.TypeItemEquip>();

		public ItemChecker SetType(params KEnums.TypeItem[] type){
			for (int i = 0; i < type.Length; i++) {
				this.typeItem.Add(type[i]);
			}
			return this;
		}
		public ItemChecker SetTypeEquip(params KEnums.TypeItemEquip[] type){
			for (int i = 0; i < type.Length; i++) {
				this.typeEquip.Add(type[i]);
			}
			return this;
		}
		public virtual bool IsItemValid(Item item){
			if (item.IsSameType (typeItem) && 
				IsSameTypeEquip (item))
				return true;
			return false;
		}
		public virtual float GetScore(Item item){
			float score = 0;
			if (item.IsSameType (typeItem) && IsSameTypeEquip(item))
				score ++;
			return score; // 0 means unmatch
		}
		bool IsSameTypeEquip(Item item){
			for (int i =0; i < typeEquip.Count; i++) {
				//Debug.Log("IS IT " + typeEquip[i] + " BUT IT IS " + item.typeEquip);
				if(typeEquip[i] == item.typeEquip) return true;
			}
			//Debug.Log("NOT THE SAME TYPE EQUIP");
			return false;
		}
	}
}

