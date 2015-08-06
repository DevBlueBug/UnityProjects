using UnityEngine;
using System.Collections;

namespace NWorld{
	
	public class Stats
	{
		internal float
			speed=0,
			armor=0,
			damage=0,
			strength = 0,
			dexterity = 0,
			intelligence = 0 ,
			vitality = 0 ;
		public float GetTotalStats(){
			return speed + armor + damage + strength + dexterity + intelligence + vitality;
		}
		public bool IsGreaterThan(Stats other){
			int countGreatStats = 0;
			bool isLessThan = false;
			if (speed >= other.speed)
				countGreatStats++;
			else isLessThan = true;
			if (armor >= other.armor)
				countGreatStats++;
			else isLessThan = true;
			if (damage >= other.damage)
				countGreatStats++;
			else isLessThan = true;
			if (strength >= other.strength)
				countGreatStats++;
			else isLessThan = true;
			if (dexterity >= other.dexterity)
				countGreatStats++;
			else isLessThan = true;
			if (intelligence >= other.intelligence)
				countGreatStats++;
			else isLessThan = true;
			if (vitality >= other.vitality)
				countGreatStats++;
			else isLessThan = true;
			if (isLessThan)
				return false;
			/**
			 * speed > other.speed &&
				armor > other.armor && 
				damage > other.damage &&
				strength > other.strength &&
				dexterity > other.dexterity &&
				intelligence > other.intelligence &&
				vitality > other.vitality;
			 * */
			return countGreatStats > 0;

		}
	}
	

}