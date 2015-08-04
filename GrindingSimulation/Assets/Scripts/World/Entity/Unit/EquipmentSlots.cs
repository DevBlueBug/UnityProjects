using UnityEngine;
using System.Collections.Generic;


namespace NWorld.NEntity.NUnit{
	
	public class EquipmentSlots
	{
		public class Slot{
			internal KEnums.TypeSlot type = KEnums.TypeSlot.Head;
			internal NItem.Equipment item = null;
			public Slot(KEnums.TypeSlot type){
				this.type = type;
			}

		}
		public Slot
			head = new Slot(KEnums.TypeSlot.Head), 
			shoulders = new Slot(KEnums.TypeSlot.Shoulders), 
			neck = new Slot(KEnums.TypeSlot.Neck), 
			chest=new Slot(KEnums.TypeSlot.Chest), 
			waist=new Slot(KEnums.TypeSlot.Waist), 
			legs=new Slot(KEnums.TypeSlot.Head), 
			feet=new Slot(KEnums.TypeSlot.Feet), 
			hands=new Slot(KEnums.TypeSlot.Hands), 
			ringL=new Slot(KEnums.TypeSlot.RingL),  
			ringR=new Slot(KEnums.TypeSlot.RingR),  
			arms=new Slot(KEnums.TypeSlot.Head),
			weaponL=new Slot(KEnums.TypeSlot.WeaponL), 
			weaponR=new Slot(KEnums.TypeSlot.WeaponR);
		public Slot Get(int n){
			switch (n) {
			default: return null;
			case 0: return head;
			case 1: return shoulders;
			case 2: return neck;
			case 3: return chest;
			case 4: return waist;
			case 5: return legs;
			case 6: return feet;
			case 7: return hands;
			case 8: return ringL;
			case 9: return ringR;
			case 10: return arms;
			case 11: return weaponL;
			case 12: return weaponR;
			}
		}
		public Slot Get(KEnums.TypeSlot type ){
			switch (type) {
			default: return null;
			case KEnums.TypeSlot.Head:		return head;
			case KEnums.TypeSlot.Shoulders: return shoulders;
			case KEnums.TypeSlot.Neck: 		return neck;
			case KEnums.TypeSlot.Chest:		return chest;
			case KEnums.TypeSlot.Waist:		return waist;
			case KEnums.TypeSlot.Legs:		return legs;
			case KEnums.TypeSlot.Feet:		return feet;
			case KEnums.TypeSlot.Hands:		return hands;
			case KEnums.TypeSlot.RingL:		return ringL;
			case KEnums.TypeSlot.RingR:		return ringR;
			case KEnums.TypeSlot.Arms:		return arms;
			case KEnums.TypeSlot.WeaponL:	return weaponL;
			case KEnums.TypeSlot.WeaponR:	return weaponR;
			}
		}
	}
	
}
