using UnityEngine;
using System.Collections;

namespace NWorld.NTreasure{
	
	public class Treasure :Entity
	{
		public float amount,amountMax;
		public Treasure(int x, int y, float amount, float amountMax):base(x,y){
			this.amount = amount;
			this.amountMax = amountMax;
		}
		public float GetPercentage(){
			return amount / amountMax;
		}
		public void Get(NUnit.Unit unit){

		}
		
	}
}

