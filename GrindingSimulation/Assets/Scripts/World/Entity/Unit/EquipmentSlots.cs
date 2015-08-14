using UnityEngine;
using System.Collections.Generic;
using NWorld.NItem.NEquip;

namespace NWorld.NEntity.NUnit{
	
	public class EquipmentSlots
	{
		public class Slot{
			KEnums.TypeSlot type = KEnums.TypeSlot.Head;
			internal KEnums.TypeItemEquip typeEquip;
			internal NItem.NEquip.Equipment item = null;
			public Slot(KEnums.TypeSlot type){
				//this.type = type;
				SetType(type);
			}
			public Slot SetType(KEnums.TypeSlot typeSlot){
				this.type = typeSlot;
				switch (typeSlot) {
				case KEnums.TypeSlot.Arms:
					this.typeEquip= KEnums.TypeItemEquip.Arms;
					break;
				case KEnums.TypeSlot.Chest:
					this.typeEquip= KEnums.TypeItemEquip.Chest;
					break;
				case KEnums.TypeSlot.Feet:
					this.typeEquip= KEnums.TypeItemEquip.Feet;
					break;
				case KEnums.TypeSlot.Hands:
					this.typeEquip= KEnums.TypeItemEquip.Hands;
					break;
				case KEnums.TypeSlot.Head:
					this.typeEquip= KEnums.TypeItemEquip.Head;
					break;
				case KEnums.TypeSlot.Legs:
					this.typeEquip= KEnums.TypeItemEquip.Legs;
					break;
				case KEnums.TypeSlot.Neck:
					this.typeEquip= KEnums.TypeItemEquip.Neck;
					break;
				case KEnums.TypeSlot.RingL:
				case KEnums.TypeSlot.RingR:
					this.typeEquip= KEnums.TypeItemEquip.Rings;
					break;
				case KEnums.TypeSlot.Shoulders:
					this.typeEquip= KEnums.TypeItemEquip.Shoulders;
					break;
				case KEnums.TypeSlot.Waist:
					this.typeEquip= KEnums.TypeItemEquip.Waist;
					break;
				case KEnums.TypeSlot.WeaponL:
				case KEnums.TypeSlot.WeaponR:
					this.typeEquip= KEnums.TypeItemEquip.Weapon;
					break;
				}
				return this;
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

		
		public bool Equip(KEnums.TypeSlot slotType, Equipment equipped, ref Equipment equippedOut){
			if(equipped.typeEquip == KEnums.TypeItemEquip.UnEquippable)
				return false;
			equippedOut = this[slotType].item;
			this [slotType].item = equipped;
			return true;
		}


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
		public Slot this[KEnums.TypeSlot type]{
			get{
				return Get(type);
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
