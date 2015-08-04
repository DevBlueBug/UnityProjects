using UnityEngine;
using System.Collections.Generic;
using NWorld.NEntity.NNPC.NMerchant;

namespace NWorld{
	
	public class World
	{
		public delegate void D_UnitAdded(World world, NEntity.NUnit.Unit unit);
		public D_UnitAdded E_UnitAdded = delegate {	};
		public AStar.KMap mapAStar;

		int width, height;
		public Tile[,] tiles;
		
		List<NWorld.NEntity.NUnit.Unit> units = new List<NWorld.NEntity.NUnit.Unit>();
		List<Merchant> merchants = new List<Merchant>();
		List<NEntity.NTreasure.Treasure> treasures = new List<NWorld.NEntity.NTreasure.Treasure>();
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
		public void AddUnit(NEntity.NUnit.Unit unit){
			units.Add (unit);
			E_UnitAdded (this,unit);

		}
		public void AddMerchant(Merchant merchant){

		}
		public void AddTreasure(NEntity.NTreasure.Treasure treasure){
			treasures.Add (treasure);
		}

		public void Update(){
			for (int i = 0; i < units.Count; i++) {
				units[i].Update(this);
			}
		}

		public void GetTask_Treasure(ref List<NTask.Task> tasks){
			foreach (var treasure in treasures) {
				var task = new NTask.TreasureHunt(treasure);
				tasks.Add(task);
			}
		}
		public void GetTask_EquipmentAcquisition(ref List<NTask.Task> tasks, KEnums.TypeItem typeItem,KEnums.TypeItemEquip equip){

			foreach (var merchant in this.merchants) {

				//var task = new NTask.MerchantPurchase();
				//tasks.Add(task);
			}
		}
	}
	


}