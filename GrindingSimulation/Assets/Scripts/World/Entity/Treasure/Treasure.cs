using UnityEngine;
using System.Collections;

namespace NWorld.NEntity.NTreasure{
	
	public class Treasure :Entity
	{
		public static float RatioPointToValue = 10;
		public float point,pointMax;
		public Treasure(int x, int y, float pointInit, float pointMax):base(x,y){
			this.point = pointInit;
			this.pointMax = pointMax;
			EntityRequestHandler.LinkBasic (this);
			E_ReceiveRequestGiveMe = H_ReceiveRequestGiveMe;
		}
		public float GetPercentage(){
			return point / pointMax;
		}
		public override void Update (World world)
		{
			base.Update (world);
			this.point = Mathf.Min (point + 1, pointMax);
			//Debug.Log ("Treasure LEFT " + point);
		}
		public bool H_ReceiveRequestGiveMe(Entity me, Entity other,params int[] arguments){
			//Debug.Log ("Treasure Opened.");
			float 
				treasureCount = 0 ,
				treasureCountMax = Mathf.Round(point / RatioPointToValue);
			while (treasureCount < treasureCountMax) {
				me.inventory.Add(NItem.Item.GetRandomItem(RatioPointToValue));
				if(other.RequestTakeIt(me, me.inventory.Count-1)){
					treasureCount++;
				}
				else
					break;

			}
			this.point = Mathf.Max(0, point- treasureCount * RatioPointToValue);
			//Debug.Log ("Treasure Transfered [ " + treasureCount + " ] " );
			return treasureCount != 0; // had given more than one treasure at least
		}
		
	}
}

