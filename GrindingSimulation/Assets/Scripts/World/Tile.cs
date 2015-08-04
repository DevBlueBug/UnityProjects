using UnityEngine;
using System.Collections.Generic;
namespace NWorld{
	
	public class Tile
	{

		bool isOccupied = false;

		void IsAvailable(NEntity.Entity requesting){
		}
		public int GetTurnRequiredToGetHere(NEntity.Entity entity){
			if (isOccupied) return -1;
			return 1; // cannot get here
		}
	}
	

}