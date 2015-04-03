using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Game.Data {

	public class DRoom
	{
		public Vector2 id;
		public int X{ get { return(int)id.x; } }
		public int Y{ get { return(int)id.y; } }
		public int width,height;
		public bool[] doors = new bool[]{false,false,false,false};
		
		public List<DEntity> entities = new List<DEntity>();

		public DRoom(){
			width = 15;
			height = 9;
		}
	}

}