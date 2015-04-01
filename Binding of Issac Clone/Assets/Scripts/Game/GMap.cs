using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Game.Entity;

namespace Game{
	public class GMap
	{
		public int 
			width,height,
			roomAt_X, roomAt_Y;
		GRoom[,] map;
		public GRoom roomActive;
		public GRoom this[int x, int y]{
			get{return map[x,y];}
			set{ map [x, y] = value;}
		}
		
		public GMap Init(Data.DMap mapData, GRoom Prefab_Room, GEntityPack EntityPackage){
			this.width = mapData.width;
			this.height = mapData.height;
			map = new GRoom[width,height];
			for (int i = 0; i < width; i++)
				for (int j = 0; j < height; j++) {
				if(mapData[i,j]==null)continue;
				var dataRoom = mapData[i,j];
				var gameRoom = AddRoom(dataRoom,i,j,Prefab_Room);
				AddEntiteis(gameRoom,EntityPackage,dataRoom.entities);
			
	
			}
			return this;
		}
		GRoom AddRoom(Data.DRoom dataRoom,int x, int y, GRoom room){
			var roomNew = GameObject.Instantiate (room);
			map[x,y] = roomNew.Init (dataRoom);
			map [x, y].gameObject.SetActive (false);
			return roomNew;
		}
		void AddEntiteis(GRoom room, GEntityPack pack, 
		                 List<Data.DEntity> entities ){ 
			foreach (var e in entities) {
				if(e.myType == Game.Data.DEntity.MyType.World)
					room.AddMap (pack.UnWrap (e), e.x, e.y, e.dirLooking);
				else if(e.myType == Game.Data.DEntity.MyType.Enemy)
					room.AddSimple (pack.UnWrap (e), e.x, e.y, e.dirLooking);
			}
				


		}
		public GRoom ActiveRoom(int x, int y){
			if (roomActive != null)
				roomActive.gameObject.SetActive (false);
			roomAt_X = x; roomAt_Y = y;
			roomActive = map [x, y];
			roomActive.gameObject.SetActive (true);
			return roomActive;
		}
		public GRoom DEBUG_ACTIVE_RANDOM_ROOM(){
			for(int i =0 ; i < width;i++)for(int j = 0 ; j < height;j++){
				if(map[i,j]  !=null){

					return ActiveRoom(i,j);
				}
			}
			return null;
		}
	}
}

