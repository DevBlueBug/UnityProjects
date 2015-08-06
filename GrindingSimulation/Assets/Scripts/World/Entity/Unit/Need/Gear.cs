using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NEntity.NUnit.NNeed{
	
	public class Gear:Need
	{
		/**
		 * Will always require better gear to be satisfied.
		 * 
		 */
		int slotIndex = 999;
		List<EquipmentSlots.Slot> slotsPriority;

		public Gear(float currentPoint, float pointMax):base(currentPoint,pointMax){
			slotsPriority = new List<EquipmentSlots.Slot> ();
			for (int i = 0; i<13; i++) {
				slotsPriority.Add(null);
			}
		}
		public override void Init (Entity entity)
		{
			entity.E_InventoryItemNew += H_NewItem;
			base.Init (entity);
		}
		public override void Update (World world, Unit entity)
		{
			point = Mathf.Min (point + .1f, pointMax);
		}
		public override void UpdateAction (World world, Unit entity)
		{
			Debug.Log ("UpdateAction Gear");
			if (slotIndex >= slotsPriority.Count) {
				UpdateSlotPriority(world, entity);
			}

			var slot = slotsPriority [slotIndex++];
			var tasks = new List<NTask.Task> ();
			var check =(NItem.NEquip.EquipmentCheck) new NItem.NEquip.EquipmentCheck().SetTypeEquip (slot.typeEquip);


			if(slot.item != null)
				check.SetStatsMin( slot.item.stats );

			world.GetTask_EquipmentAcquisition (entity, ref tasks, check);
			entity.Debug_Text ("Gear! My Items suck ! Task : " + tasks.Count);
	

			NTask.Task taskSelected = null;
			float weight = 0;
			//should search for the best task
			for (int i = 0; i < tasks.Count; i++) {
				var weightNew = tasks[i].GetWeight();
				if(weightNew > weight){
					weight = weightNew;
					taskSelected = tasks[i];
				}

			}
			if (taskSelected != null)
				entity.SetTask (world,taskSelected);
		}
		void UpdateSlotPriority(World world, Unit entity){
			slotIndex = 0;
			for(int i = 0 ; i< 13 ; i++){
				slotsPriority[i] = entity.equipments.Get(i);
			}
			slotsPriority.Sort (IComparison_Slots);

		}
		float GetScore(NItem.NEquip.Equipment item){
			if (item == null) return 0;
		
			return item.stats.armor + item.stats.damage + item.stats.speed + item.stats.vitality;
		}
		int IComparison_Slots(EquipmentSlots.Slot x, EquipmentSlots.Slot y){
			return (int)( (GetScore(x.item)- GetScore(y.item) ) * 10);
		}
		void H_NewItem(Entity entity, NItem.Inventory inventory, NItem.Item item){

			Debug.Log ("GEAR CHECKING... " + item.IsSameType(KEnums.TypeItem.Equipment) );
		
			//find the slot the put the gear on
			//check if the current gear is "better"
			//then put it on then be happy.

		}
		
	}
	
}
