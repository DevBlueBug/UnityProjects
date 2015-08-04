using UnityEngine;
using System.Collections;

namespace NWorld{
	public class KEnums
	{
		public enum IdItem{Jewerly_Red};
		public enum TypeItem{Treasure,Equipment};
		public enum TypeStats{Strength,Vitality,Intelligence,Dexterity};

		public enum TypeItemEquip{UnEquippable, Head,Shoulders,Neck,Chest,Waist,Legs,Feet,Hands,Rings,Arms,Weapon };
		public enum TypeSlot{Head,Shoulders,Neck,Chest,Waist,Legs,Feet,Hands,RingL,RingR,Arms,WeaponL,WeaponR};
		//head, shoulders, neck, chest, waist, legs, feet, hands, rings, arms,
		//weaponL, weaponR;
		
		public enum TypeRequest{GiveMe,ShowMe, TakeIt};
		public enum TypeNeed{Treasure,Gear};
		public enum TypeTask{
			TreasureHunt,
			NewGear_Head,
			NewGear_Shoulders,
			NewGear_Neck,
			NewGear_Chest,
			NewGear_Waist,
			NewGear_Legs,
			NewGear_Feet,
			NewGear_Hands,
			NewGear_Rings,
			NewGear_Weapon
		}
		public enum TypeSkill{Walk,Carry,Drop,Get};
	}

}