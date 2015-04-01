using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameLogic.Entity;
using GameLogic.Entity.Unit;

namespace GameLogic {

	public class DataRoom
	{
		public Vector2 id;
		public int width,height;
		//Doors up right down left. Clockwise.
		public Door[] doors = new Door[]{null,null,null,null};
		List<EntityBase> units; //to be updated

		public DataRoom(){
			width = 15;
			height = 9;
		}


	}

}