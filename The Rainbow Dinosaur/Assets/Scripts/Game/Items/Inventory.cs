using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NItem{
	
	public class Inventory {
		public delegate void D_EquipItem(Item Item);
		public D_EquipItem E_EquipItem = delegate { };
		//Entity player;

		public Dictionary<Item.KId, Item> items;

		Item 
			equipHead,
			equipHead_Hat,
			equipBody,
			equipBody_Front,
			equipBody_Back,
			equipBody_Pants,
			equipFloor;

		public Inventory(){
			items = new Dictionary<Item.KId, Item> ();
		}
		public Item this[int index]{
			get{
				var k =items.Keys.GetEnumerator();
				for(int i = 0 ; i < index;i++)k.MoveNext();
				return items[k.Current];
			}
		}
		public int Get(Item.KId type){
			Item item;
			items.TryGetValue (type, out item);
			if (item == null)
				return 0;
			return item.Count;
		}
		public Inventory Add(Entity owner, Item itemAdded){
			Item item;
			items.TryGetValue (itemAdded.id, out item);
			if (item == null) {
				if(itemAdded.typeEquip != Item.KTypeEquip.None){
					AddSlot(itemAdded);

				}else {
					items.Add (itemAdded.id, itemAdded);
				}

			}
			else
				items [itemAdded.id].Count += itemAdded.Count;
			return this;
		}
		void AddSlot(Item itemAdded){
			switch (itemAdded.typeEquip) {
			case Item.KTypeEquip.Head:
				this.equipHead = itemAdded;
				break;
			case Item.KTypeEquip.Body:
				this.equipBody = itemAdded;
				break;
			}
			E_EquipItem (itemAdded);
		}
		public void Reset(){
			
		}
	}
}