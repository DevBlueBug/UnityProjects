using UnityEngine;
using System.Collections;
using NWorld.NEntity.NUnit.NOrder;
namespace NWorld.NTask{
	
	public class TreasureHunt :Task 
	{
		public enum State {Init, Processing, Failure,Success};
		public State stateMe = State.Init;

		NEntity.NTreasure.Treasure treasure;
		float treasureX,treasureY;
		float treasureAmount;

		public TreasureHunt(NEntity.NTreasure.Treasure treasure){
			this.treasure = treasure;
			this.type = KEnums.TypeTask.TreasureHunt;
			//skillsNeeded.Add (KEnums.TypeSkill.Walk);
			//skillsNeeded.Add (KEnums.TypeSkill.Get);
		}
		public override void Init (World world, NWorld.NEntity.NUnit.Unit unit)
		{
			var path = world.mapAStar.GetPath (new Vector2 (unit.x,unit.y), new Vector2 (treasure.x,treasure.y));
			if (path == null) {
				Debug.Log("Path Not Found");
				stateMe= State.Failure;
				return;
			}
			stateMe = State.Processing;

			//Debug.Log("Path Found");
			//string s = "";
			for (int i = 0; i< path.Count; i++) {
				unit.AddOrder(world,new Move(world,unit,path[i].x,path[i].y) );
				//s+= " [ " +path[i].x + " " + path[i].y + " ] " ;
			}
			unit.AddOrder (world, new Request_GiveMe (world, unit,treasure));
			//Debug.Log (s);	
		}
	}
	
	
}