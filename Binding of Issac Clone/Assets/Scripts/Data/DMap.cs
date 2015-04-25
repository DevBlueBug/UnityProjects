using System;
using System.Collections.Generic;
namespace Data
{
	public class DMap
	{
		public int width, height;
		public DRoom roomInit;
		DRoom[,] rooms;
		public List<DRoom> roomsAvailable;
		
		public DMap (int w, int h)
		{
			this.width = w;
			this.height = h;
			rooms = new DRoom[width,height];
			roomsAvailable = new List<DRoom> ();
		}
		public DRoom this[int x, int y]{
			get{
				return rooms[x,y];
			}
			set{
				if(rooms[x,y] != null){
					roomsAvailable.Remove(rooms[x,y]);
				}
				roomsAvailable.Add(value);
				rooms[x,y] = value;
			}
		}
	}
}

