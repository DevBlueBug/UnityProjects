using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NItem{
	
	public class Price{
		public class Pair{
			internal KEnums.IdItem id;
			internal int count;
			public Pair(KEnums.IdItem id, int count){
				this.id = id;
				this.count = count;
			}
		}
		internal List<Pair> prices = new List<Pair>();
		public Price(){
		}
		public Price Add(Pair pair){
			prices.Add (pair);
			return this;
		}
		public float GetSum(){
			float value = 0;
			for(int i = 0;  i< prices.Count;i++){
				value += NItem.ItemList.GetValue(prices[i].id) * prices[i].count;
			}
			return value;
		}
	}

}