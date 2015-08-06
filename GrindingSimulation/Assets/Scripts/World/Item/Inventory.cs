using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NItem{
	
	public class Inventory
	{
		public delegate void D_MeItem(Inventory me, Item item);
		public D_MeItem 
		E_ItemAdded = delegate {	},
		E_ItemRemoved = delegate {	};

		List<Item> items = new List<Item>();
		public Item this[int n]{
			get{
				return items[n];
			}
		}
		public int Count{get { return items.Count; }}
		public int Add(Item item){
			// 1 added fully
			// 2 Added partially
			// -1 cannot add

			//could not be added if my inventry is full or something

			if (item.isStackable) {
				for(int i = 0 ; i < items.Count;i++){
					if(items[i].isStackable &&  items[i].IsSame(item) ){ 
						items[i].Count += item.Count;
						E_ItemAdded(this,item);
						return 1;
					}
				}
			}
			items.Add(item);
			E_ItemAdded (this,item);
			return 1;
		}
		public void Update(){
			for(int i = items.Count-1 ; i > -1;i--){
				if(items[i].Count <=0) Remove(i);
			}
		}
		public void Remove(int n ){
			E_ItemRemoved (this, items [n]);
			items.RemoveAt (n);
		}
		
	}
	
	
}