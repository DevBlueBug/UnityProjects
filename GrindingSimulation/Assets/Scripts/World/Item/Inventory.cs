using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NItem{
	
	public class Inventory
	{
		List<Item> items = new List<Item>();
		public Item this[int n]{
			get{
				return items[n];
			}
		}
		public int Count{get { return items.Count; }}
		public bool Add(Item item){
			//could not be added if my inventry is full or something

			if (item.isStackable) {
				for(int i = 0 ; i < items.Count;i++){
					if(items[i].isStackable &&  items[i].type == item.type){ 
						items[i].Count += item.Count;
						return true;
					}
				}
			}
			items.Add(item);
			return true;
		}
		public void Remove(int n ){
			items.RemoveAt (n);
		}
		
	}
	
	
}