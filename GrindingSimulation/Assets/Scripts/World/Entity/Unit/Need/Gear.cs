using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NEntity.NUnit.NNeed{
	
	public class Gear:Need
	{
		/**
		 * Will always require better gear to be satisfied.
		 * 
		 */
		int slotIndex = -1;
		List<EquipmentSlots.Slot> slotsPriority;

		public Gear(float currentPoint, float pointMax):base(currentPoint,pointMax){
			slotsPriority = new List<EquipmentSlots.Slot> ();
			for (int i = 0; i<13; i++) {
				slotsPriority.Add(null);
			}

		}
		public override void Update (World world, Unit entity)
		{
			point = Mathf.Min (point + .1f, pointMax);
		}
		public override void UpdateAction (World world, Unit entity)
		{
			Debug.Log ("UpdateAction Gear");
			entity.Debug_Text ("Gear! My Items suck!");

			if (slotIndex == -1) {
				UpdateSlotPriority(world, entity);
			}
			//will now require to get a new equipment
			//world.gettask (KEnums.TypeNeed.Gear);
		}
		void UpdateSlotPriority(World world, Unit entity){
			slotIndex = 0;
			for(int i = 0 ; i< 13 ; i++){
				slotsPriority[i] = entity.equipments.Get(i);
			}
			slotsPriority.Sort (IComparison_Slots);

		}
		float GetScore(NItem.Equipment item){
			if (item == null) return 0;
		
			return item.stats.armor + item.stats.damage + item.stats.speed + item.stats.vitality;
		}
		int IComparison_Slots(EquipmentSlots.Slot x, EquipmentSlots.Slot y){
			return (int)( (GetScore(x.item)- GetScore(y.item) ) * 10);
		}
		
	}
	
}
