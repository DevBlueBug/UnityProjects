using UnityEngine;
using System.Collections;

namespace NWorld.NUnit.NNeed{
	
	public class Treasure:Need
	{
		// needed acquisition amount of treasure per point
		// will increase gradually to require "better treasure"
		float treasurePerPoint = 10; 

		public Treasure(float currentPoint, float pointMax):base(currentPoint,pointMax){

		}
		public override void Update (World world, Unit entity)
		{
			point += 1;
			Debug.Log ("Need Point : " + point);
		}
		public override void UpdateAction (World world, Unit entity)
		{
			entity.Debug_Text ("I want to find some treasure!");
			var tasks = world.RequestNeed (KEnums.TypeNeed.Treasure);
			float treasureNeededAmount = this.point * treasurePerPoint;
			
			for(int i = 0 ; i < tasks.Count;i++){
				if(entity.SetTask (world, tasks [i])) return;
			}
		}
		
	}
	
}
