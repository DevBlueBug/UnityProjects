using System;
using System.Collections.Generic;
namespace Data
{
	public class DMap
	{
		public int width, height;
		DRoom[,] rooms;
		public List<DRoom> roomsAvailable;
		public DRoom 
			roomInit,
			roomFar;
		
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
		
		int RC_UpdateCostCount = 0;
		void RC_UpdateCost(int x, int y,ref float distanceMax){
			if (RC_UpdateCostCount++ > 100) return;
			if (this[x, y].distance > distanceMax){
				UnityEngine.Debug.Log("RoOM FAR " + x + " " + y);
				distanceMax = this[x,y].distance;
				roomFar = this[x,y];
			}
			for (int i = 0; i < 4; i++) {
				int 
					xNew = x + (int)Utility.EasyUnity.dirFour3[i].x,
					yNew = y+ (int)Utility.EasyUnity.dirFour3[i].y;
				if(!helperIsPositionValid(xNew,yNew) || this[xNew,yNew] == null) continue;
				this[xNew,yNew].distance = this[x,y].distance +1;
				RC_UpdateCost(xNew,yNew,ref distanceMax); 
			}
		}
		bool helperIsPositionValid(int posx, int posy){
			return posx >= 0 && posx < width && 
				posy >= 0 && posy < height;
		}
	}
}

