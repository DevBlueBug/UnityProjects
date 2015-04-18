using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace  Game.Data {
	public class DMapGen{
		static Vector2[] directionsFour = new Vector2[]{
			new Vector2(0,1),new Vector2(1,0),new Vector2(0,-1),new Vector2(-1,0)
		};

		bool[,]			roomsIsAdded;
		List<Vector2>	roomsAdded;
		List<DRoom>		roomsCreated;

		public DMapGen(){
		}
		bool helperIsPositionValid(DMap kMap, Vector2 pos){
			return pos.x >=0 && pos.x < kMap.width && pos.y >=0 && pos.y < kMap.height;
		}

		
		DRoom Iterate(DMap kMap, List<DRoom> roomsCreated, List<Vector2> roomsAdded ){
			int index = Random.Range (0, roomsAdded.Count);
			Vector2 pos = roomsAdded [index];
			roomsAdded.RemoveAt (index);
			
			if (isPositionGood (kMap,pos)) {
				var r = (kMap[(int)pos.x,(int)pos.y] = new DRoom());
				roomsCreated.Add(kMap[(int)pos.x,(int)pos.y]);
				r.id = pos;
				MarkAround(kMap,pos);
				//Debug.Log("NEW ROOM AT " + pos.x + " " + pos.y);
				return r;
			}
			return null;	
		}
		public void UpdateRooms(DMap kMap, ref bool[,] isAdded, DRoom room){
			isAdded [(int)room.id.x, (int)room.id.y] = true;
			for (int i = 0; i < 4; i++) {
				var posNew = room.id + directionsFour[i];
				if(!helperIsPositionValid(kMap,posNew)) continue;
				var roomNew = kMap[(int)posNew.x,(int)posNew.y];
				if(roomNew == null) continue;
				
				room.doors[i] = true;
				if(isAdded[(int)posNew.x, (int)posNew.y]) continue;
				else UpdateRooms(kMap, ref isAdded, roomNew);
			}
			
		}
		void Mark(Vector2 pos){Mark ((int)pos.x, (int)pos.y);}
		void Mark(int x, int y){
			if (roomsIsAdded [x, y]) return;
			roomsIsAdded [x, y] = true;
			roomsAdded.Add (new Vector2 (x, y));
		}
		void MarkAround(DMap kMap, Vector2 pos){
			for (int i = 0; i <4; i++) {
				var posNew =pos + directionsFour[i];
				if(!helperIsPositionValid(kMap,posNew))
					continue;
				Mark(posNew);
			}
		}
		
		bool isPositionGood(DMap kMap,Vector2 pos){
			int countEmptySpaces = 0;
			for (int i = 0; i <4; i++) {
				var posNew =pos + directionsFour[i];
				if(!helperIsPositionValid(kMap,posNew)){
					countEmptySpaces++;
					continue;
				}
				if(kMap[(int)posNew.x,(int)posNew.y]==null) 
					countEmptySpaces++;
			}
			return countEmptySpaces >= 3;
		}
	}
	public class DMapGenerator
	{
		static Vector2[] directionsFour = new Vector2[]{
			new Vector2(0,1),new Vector2(1,0),new Vector2(0,-1),new Vector2(-1,0)
		};

		DMap kMap;
		bool[,]			roomsIsAdded;
		List<Vector2>	roomsAdded;
		List<DRoom>		roomsCreated;

		bool helperIsPositionValid(Vector2 pos){
			return pos.x >=0 && pos.x < kMap.width && pos.y >=0 && pos.y < kMap.height;
		}

		public DMapGenerator(){ }
		public DMap GenerateMap(int width, int height, int count){
			if (width % 2 == 0) width++;
			if (height % 2 == 0)height++;
			kMap = new DMap (width, height);
			roomsIsAdded = new bool[width, height];
			roomsAdded = new List<Vector2> ();
			roomsCreated = new List<DRoom> ();
			Mark (width / 2, height / 2);
			
			
			var roomInit = Iterate (roomsCreated,roomsAdded);
			for (int i = 0; i< count-1; i++) {
				Iterate(roomsCreated,roomsAdded);
			}
			bool[,] isAdded = new bool[width, height];
			UpdateRooms (ref isAdded, roomInit);
			new DMapDecorator ().Init (roomsCreated);
			return kMap;
		}
		
		DRoom Iterate(List<DRoom> roomsCreated, List<Vector2> roomsAdded ){
			int index = Random.Range (0, roomsAdded.Count);
			Vector2 pos = roomsAdded [index];
			roomsAdded.RemoveAt (index);
			
			if (isPositionGood (pos)) {
				var r = (kMap[(int)pos.x,(int)pos.y] = new DRoom());
				roomsCreated.Add(kMap[(int)pos.x,(int)pos.y]);
				r.id = pos;
				MarkAround(pos);
				//Debug.Log("NEW ROOM AT " + pos.x + " " + pos.y);
				return r;
			}
			return null;	
		}
		public void UpdateRooms(ref bool[,] isAdded, DRoom room){
			isAdded [(int)room.id.x, (int)room.id.y] = true;
			for (int i = 0; i < 4; i++) {
				var posNew = room.id + directionsFour[i];
				if(!helperIsPositionValid(posNew)) continue;
				var roomNew = kMap[(int)posNew.x,(int)posNew.y];
				if(roomNew == null) continue;
				
				room.doors[i] = true;
				if(isAdded[(int)posNew.x, (int)posNew.y]) continue;
				else UpdateRooms(ref isAdded, roomNew);
			}
			
		}
		void Mark(Vector2 pos){Mark ((int)pos.x, (int)pos.y);}
		void Mark(int x, int y){
			if (roomsIsAdded [x, y]) return;
			roomsIsAdded [x, y] = true;
			roomsAdded.Add (new Vector2 (x, y));
		}
		void MarkAround(Vector2 pos){
			for (int i = 0; i <4; i++) {
				var posNew =pos + directionsFour[i];
				if(!helperIsPositionValid(posNew))
					continue;
				Mark(posNew);
			}
		}
		
		bool isPositionGood(Vector2 pos){
			int countEmptySpaces = 0;
			for (int i = 0; i <4; i++) {
				var posNew =pos + directionsFour[i];
				if(!helperIsPositionValid(posNew)){
					countEmptySpaces++;
					continue;
				}
				if(kMap[(int)posNew.x,(int)posNew.y]==null) 
					countEmptySpaces++;
			}
			return countEmptySpaces >= 3;
		}
	}
}
