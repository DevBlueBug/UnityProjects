using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NEntity.NUnit.NNeed{
	
	public class Treasure:Need
	{
		/**
		 * treasurePerPoint :	Needed acquisition amount of treasure per point.
		 * 						Will increase gradually to require "higher value treasure".
		 * 						Acquired item has to be of type "treasure".
		 */

		float treasurePerPoint = 10; 

		public Treasure(float currentPoint, float pointMax):base(currentPoint,pointMax){

		}
		public override void Init (Entity entity)
		{
			base.Init (entity);
			entity.E_InventoryItemNew += H_InventoryItemNew;
			entity.E_InventoryItemRemoved += H_InventoryItemRemoved;
		}

		public override void Update (World world, Unit entity)
		{
			point = Mathf.Min(point+.1f, pointMax);
			//Debug.Log ("Need Point : " + point);
		}
		public override void UpdateAction (World world, Unit entity)
		{
			Debug.Log ("UpdateAction Treasure");
			entity.Debug_Text ("I want to find some treasure!");
			var tasks = new List<NTask.Task>();
			world.GetTask_Treasure(entity, ref tasks);

			float treasureNeededAmount = this.point * treasurePerPoint;
			
			for(int i = 0 ; i < tasks.Count;i++){
				if(entity.SetTask (world, tasks [i])) return;
			}
		}
		public void H_InventoryItemNew(Entity entity, NItem.Inventory inventory,  NItem.Item item ){
			var pointAfter = Mathf.Min(point - (item.value / treasurePerPoint ), pointMax);
			entity.Debug_Text ("TreasureAcquired VeryHappy " + point + " -> " + pointAfter);
			point = pointAfter;
		
		}
		
		public void H_InventoryItemRemoved(Entity entity, NItem.Inventory inventory,  NItem.Item item ){
			point = Mathf.Min(point+ (item.value / treasurePerPoint) * .333f , pointMax);
		}
		
	}
	
}
