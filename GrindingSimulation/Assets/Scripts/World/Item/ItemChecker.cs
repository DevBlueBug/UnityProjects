using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NItem{
	
	public class ItemChecker
	{
		
		List<KEnums.TypeItem> typeItem;
		List<KEnums.TypeItemEquip> typeEquip = new List<KEnums.TypeItemEquip>();
		public bool IsItemValid(Item item){
			return false;
		}
	}
}

