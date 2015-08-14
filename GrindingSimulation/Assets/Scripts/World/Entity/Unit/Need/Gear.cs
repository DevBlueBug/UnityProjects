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
			//find the slot the put the gear on
			//check if the current gear is "better"
			//then put it on then be happy.
			if(! item.IsSameType(KEnums.TypeItem.Equipment) )return;
			var unit = ((Unit)entity);
			var equip = (NItem.NEquip.Equipment)item;
			var slots = HelperGetEquipmentSlots (item.typeEquip);
		

			bool isItemEquipped = false;
			for (int i = 0; i < slots.Length; i++) {
				if(slots[i]== KEnums.TypeSlot.Undefined) continue;
				//var slot = unit.equipments[slots[i]];
				if(unit.equipments[slots[i]].item !=null && 
				   unit.equipments[slots[i]].item.stats.GetTotalStats() > equip.stats.GetTotalStats()) continue;

				NItem.NEquip.Equipment itemEquipppedOut = null;
				if(unit.equipments.Equip(slots[i], equip,ref itemEquipppedOut) ){
					isItemEquipped = true;
					if(itemEquipppedOut!=null)entity.inventory.Add(itemEquipppedOut);
					break;
				}
			}

			if (isItemEquipped) {
				entity.Debug_Text("Item is Equipped and I am very happy");
				this.point = 0;
			}

		}
		KEnums.TypeSlot[] HelperGetEquipmentSlots(KEnums.TypeItemEquip type){
			KEnums.TypeSlot[] equipSlots = new KEnums.TypeSlot[2]{KEnums.TypeSlot.Undefined,KEnums.TypeSlot.Undefined };
			switch (type) {
			case KEnums.TypeItemEquip.Arms:
				equipSlots[0] = KEnums.TypeSlot.Arms;
				break;
			case KEnums.TypeItemEquip.Chest:
				equipSlots[0] = KEnums.TypeSlot.Chest;
				break;
			case KEnums.TypeItemEquip.Feet:
				equipSlots[0] = KEnums.TypeSlot.Feet;
				break;
			case KEnums.TypeItemEquip.Hands:
				equipSlots[0] = KEnums.TypeSlot.Hands;
				break;
			case KEnums.TypeItemEquip.Head:
				equipSlots[0] = KEnums.TypeSlot.Head;
				break;
			case KEnums.TypeItemEquip.Legs:
				equipSlots[0] = KEnums.TypeSlot.Legs;
				break;
			case KEnums.TypeItemEquip.Neck:
				equipSlots[0] = KEnums.TypeSlot.Neck;
				break;
			case KEnums.TypeItemEquip.Rings:
				equipSlots[0] = KEnums.TypeSlot.RingL;
				equipSlots[1] = KEnums.TypeSlot.RingR;
				break;
			case KEnums.TypeItemEquip.Shoulders:
				equipSlots[0] = KEnums.TypeSlot.Shoulders;
				break;
			case KEnums.TypeItemEquip.Waist:
				equipSlots[0] = KEnums.TypeSlot.Waist;
				break;
			case KEnums.TypeItemEquip.Weapon:
				equipSlots[0] = KEnums.TypeSlot.WeaponL;
				equipSlots[1] = KEnums.TypeSlot.WeaponR;
				break;
			}
			return equipSlots;
		}
		
	}
	
}
