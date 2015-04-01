using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic {
	public class GameMapGenerator
	{
		public GameMap gMap;
		bool[,] roomsIsAdded;

		List<Vector2> roomsAvailabe;

		Vector2[] directionsFour = new Vector2[]{
			new Vector2(0,1),new Vector2(1,0),new Vector2(0,-1),new Vector2(-1,0)
		};
		bool helperIsPositionValid(Vector2 pos){
			return pos.x >=0 && pos.x < gMap.width && pos.y >=0 && pos.y < gMap.height;
		}
		public GameMapGenerator(){ 
		
		}
		public GameMap GenerateRooms(int width, int height, int count){
	

			if (width % 2 == 0) width++;
			if (height % 2 == 0)height++;
			gMap = new GameMap (width, height);
			roomsIsAdded = new bool[width, height];
			roomsAvailabe = new List<Vector2> ();
			Mark (width / 2, height / 2);


			gMap.roomAt = Iterate ();
			for (int i = 0; i< count-1; i++) {
				Iterate();
			}
			bool[,] isAdded = new bool[width, height];
			UpdateRooms (ref isAdded, gMap.roomAt);
			return gMap;
		}
		public void UpdateRooms(ref bool[,] isAdded, DataRoom room){
			isAdded [(int)room.id.x, (int)room.id.y] = true;
			for (int i = 0; i < 4; i++) {
				var posNew = room.id + directionsFour[i];
				if(!helperIsPositionValid(posNew)) continue;
				var roomNew = gMap.rooms[(int)posNew.x,(int)posNew.y];
				if(roomNew == null) continue;

				room.doors[i] = new GameLogic.Entity.Door();
				if(isAdded[(int)posNew.x, (int)posNew.y]) continue;
				else UpdateRooms(ref isAdded, roomNew);
			}

		}
		void Mark(Vector2 pos){Mark ((int)pos.x, (int)pos.y);}
		void Mark(int x, int y){
			if (roomsIsAdded [x, y]) return;
			roomsIsAdded [x, y] = true;
			roomsAvailabe.Add (new Vector2 (x, y));
		}
		void MarkAround(Vector2 pos){
			for (int i = 0; i <4; i++) {
				var posNew =pos + directionsFour[i];
				if(!helperIsPositionValid(posNew))
					continue;
				Mark(posNew);
			}
		}
		DataRoom Iterate(){
			int index = Random.Range (0, roomsAvailabe.Count);
			Vector2 pos = roomsAvailabe [index];
			roomsAvailabe.RemoveAt (index);

			if (isPositionGood (pos)) {
				var r = (gMap.rooms[(int)pos.x,(int)pos.y] = new DataRoom());
				r.id = pos;
				MarkAround(pos);
				//Debug.Log("NEW ROOM AT " + pos.x + " " + pos.y);
				return r;
			}
			return null;

		}
		bool isPositionGood(Vector2 pos){
			int countEmptySpaces = 0;
			for (int i = 0; i <4; i++) {
				var posNew =pos + directionsFour[i];
				if(!helperIsPositionValid(posNew)){
					countEmptySpaces++;
					continue;
				}
				if(gMap.rooms[(int)posNew.x,(int)posNew.y]==null) 
					countEmptySpaces++;
			}
			return countEmptySpaces >= 3;
		}
		void OccupyThisRoom(int x, int y){
			if (gMap.rooms [x, y] != null)
				return;
			roomsIsAdded [x, y] = true;
			gMap.rooms [x, y] = new DataRoom ();
			for (int i = 0; i < 4; i++) {
				gMap.rooms [gMap.width / 2 +(int)directionsFour[i].x, gMap.height / 2] = new DataRoom ();
			}
		}

		// Use this for initialization
		void Start ()
		{
		
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
	}
}
