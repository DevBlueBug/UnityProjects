using UnityEngine;
using System.Collections.Generic;
namespace NWorld{
	
	public class World
	{
		public delegate void D_UnitAdded(World world, NUnit.Unit unit);
		public D_UnitAdded E_UnitAdded = delegate {	};
		public AStar.KMap mapAStar;

		int width, height;
		public Tile[,] tiles;

		List<NWorld.NUnit.Unit> units = new List<NWorld.NUnit.Unit>();
		List<NTreasure.Treasure> treasures = new List<NWorld.NTreasure.Treasure>();
		List<NTask.Task> jobs = new List<NWorld.NTask.Task>();


		public World(int width, int height){
			this.width = width;
			this.height = height;
			tiles = new Tile[width, height];
			for (int i = 0; i < width; i++)
				for (int j = 0; j < height; j++) {
				tiles[i,j] = new Tile();
				}
			this.mapAStar = new AStar.KMap (width, height);

			
		}
		public void AddUnit(NUnit.Unit unit){
			units.Add (unit);
			E_UnitAdded (this,unit);

		}
		public void AddTreasure(NTreasure.Treasure treasure){
			treasures.Add (treasure);
		}

		public void Update(){
			for (int i = 0; i < units.Count; i++) {
				units[i].Update(this);
			}
		}
		//return tasks required to fulfill the need
		//Unit will select one of them 
		public List<NTask.Task> RequestNeed(KEnums.TypeNeed type){
			List<NTask.Task> tasks = new List<NWorld.NTask.Task> ();
			if(type == KEnums.TypeNeed.Treasure){
				GetTreasureTasks(ref tasks);
			}
			return tasks;

		}
		public void GetTreasureTasks(ref List<NTask.Task> tasks){
			foreach (var treasure in treasures) {
				var task = new NTask.TreasureHunt(treasure);
				tasks.Add(task);
			}


		}
	}
	


}