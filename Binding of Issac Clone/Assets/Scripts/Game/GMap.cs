using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Game.Entity;

namespace Game{
	public class GMap
	{
		public int 
			width,height;
		GRoom[,] map;
		public GRoom roomActive;
		public GRoom this[int x, int y]{
			get{return map[x,y];}
			set{ map [x, y] = value;}
		}
		public static GMap Generate(GTheme theme, Data.DMap data){
			GMap map = new GMap ();
			map.Init (data.width, data.height);
			for (int i = 0; i < data.width; i++)
			for (int j = 0; j < data.height; j++) {
				if(data[i,j]==null)continue;
				var GRoom = new GRoomGenerator().Generate(theme ,data[i,j]);
				map.RegisteRoom(GRoom);
			}
			return map;
		}

		public GMap Init(int w, int h){
			this.width = w;
			this.height = h;
			map = new GRoom[w, h];
			return this;
		}

		public GRoom Load(int x, int y){
			if (roomActive != null)
				roomActive.gameObject.SetActive (false);
			roomActive = map [x, y];
			//Debug.Log (roomActive);
			roomActive.gameObject.SetActive (true);
			return roomActive;
		}

		public void RegisteRoom(GRoom room){
			if (roomActive != null)
				roomActive.gameObject.SetActive (false);
			map [room.X, room.Y] = room;
			Load (room.X, room.Y);
		}
		
		public void KUpdate ()
		{
			roomActive.KUpdate ();
			
		}

		public GRoom DEBUG_ACTIVE_RANDOM_ROOM(){
			for(int i =0 ; i < width;i++)for(int j = 0 ; j < height;j++){
				if(map[i,j]  !=null){

					return Load(i,j);
				}
			}
			return null;
		}
	}
}

