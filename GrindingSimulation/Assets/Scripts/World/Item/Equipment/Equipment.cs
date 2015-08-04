using UnityEngine;
using System.Collections;

namespace NWorld.NItem{

	public class Equipment : Item
	{
		internal Stats stats = new Stats();
		public Equipment(){
			this.type.Add (KEnums.TypeItem.Equipment);
		}
		public void Link(NEntity.Entity entity){
		}
		public void UnLink(NEntity.Entity entity){
		}
	}

}