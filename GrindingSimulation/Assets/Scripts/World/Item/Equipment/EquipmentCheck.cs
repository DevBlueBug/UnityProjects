using UnityEngine;
using System.Collections;

namespace NWorld.NItem.NEquip{
	
	public class EquipmentCheck : ItemChecker
	{
		internal Stats statsMin = new Stats(); //inclusive
		//Stats statsMax = null; //inclusive
		public EquipmentCheck():base(){
			this.SetType (KEnums.TypeItem.Equipment);
		}
		public EquipmentCheck SetStatsMin(Stats stat){
			this.statsMin = stat;
			return this;
		}
		/**
		public EquipmentCheck SetStatsMax(Stats stat){
			this.statsMax = stat;
			return this;
		}
		**/
		public override bool IsItemValid (Item item)
		{
			var result = base.IsItemValid (item);
			if (!result) {
				//Debug.Log ("FAILURE ");
				return false;
			}
			if (!item.IsSameType (KEnums.TypeItem.Equipment)) {
				//Debug.Log ("FAILURE ");
				return false;
			}
			var equip = (NItem.NEquip.Equipment) item;
			//Debug.Log("Final Stat Test Result "   + equip.stats.IsGreaterThan (statsMin) );
			return equip.stats.IsGreaterThan (statsMin);
		}

		public override float GetScore (Item item)
		{
			//float itemTotalAttributes = item.GetValue ();
			return base.GetScore (item) ;
		}
	}
}

