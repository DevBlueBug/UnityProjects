using UnityEngine;
using System.Collections.Generic;
namespace NWorld{
	
	public class Tile
	{

		bool isOccupied = false;

		void IsAvailable(Entity requesting){
		}
		public int GetTurnRequiredToGetHere(Entity entity){
			if (isOccupied) return -1;
			return 1; // cannot get here
		}
	}
	

}