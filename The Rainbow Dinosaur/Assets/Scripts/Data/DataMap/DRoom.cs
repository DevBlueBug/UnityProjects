using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Data {

	public class DRoom
	{
		public enum KType {Normal,Start,Treasure,Boss,Secret }

		public KType meType = KType.Normal;
		public Vector2 id;
		public int X{ get { return(int)id.x; } }
		public int Y{ get { return(int)id.y; } }
		public int width,height;
		public bool[] doors = new bool[]{false,false,false,false};
		public DDoor[] doorss= new DDoor[4];
		public float distance;
		

		public DRoom(Vector2 id){
			width = 15;
			height = 9;
			this.id = id;
		}


	}

}