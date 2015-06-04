using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NItem{
	
	public class Inventory {
		//Entity player;

		public Dictionary<Item.KType, Item> items;
		public Inventory(){
			items = new Dictionary<Item.KType, Item> ();
		}
		public Item this[int index]{
			get{
				var k =items.Keys.GetEnumerator();
				for(int i = 0 ; i < index;i++)k.MoveNext();
				return items[k.Current];
			}
		}
		public int Get(Item.KType type){
			Item item;
			items.TryGetValue (type, out item);
			if (item == null)
				return 0;
			return item.Count;
		}
		public Inventory Add(Item itemAdded){
			Item item;
			items.TryGetValue (itemAdded.meType, out item);
			if (item == null)
				items.Add (itemAdded.meType, itemAdded);
			else
				items [itemAdded.meType].Count += itemAdded.Count;
			return this;
		}
		public void Reset(){
			
		}
	}
}