//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
namespace NItem
{
	public class Item
	{
		public enum KId {
			Unknown, Bomb,Money, Weapon,
			Equip_Body_default,
			Equip_Head_Default,
			Equip_Head_Kaonash,
			Equip_Body_Heart
		};
		public enum KTypeEquip	{None, Head, Head_Hat, Body, Body_Front,Body_Back, Body_Pants, Floor};
		public KId id;
		public KTypeEquip typeEquip;
		public int Count = 1;
		public static Item Get(KId id){
			switch (id) {
			case KId.Equip_Body_default:
				return new Item(id,1).SetTypeEquip(KTypeEquip.Body);
			case KId.Equip_Head_Default:
				return new Item(id,1).SetTypeEquip(KTypeEquip.Head);
			}
			return null;
		}
		
		public Item ()
		{
		}
		
		public Item (KId type, int count)
		{
			this.id = type;
			this.Count = count;
			typeEquip = KTypeEquip.None;
		}
		public Item SetTypeEquip(KTypeEquip equip){
			this.typeEquip = equip;
			return this;
		}
		public static Item GetMoney(){
			return new Item(KId.Money,1);
		}
		public static Item GetBomb(){
			return new Item(KId.Bomb,1);
		}
		public virtual void Equip(Entity entity){
		}
		public virtual void UnEquip(Entity entity){
		}

	}
}

