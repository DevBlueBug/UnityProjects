//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
namespace Data
{
	
	public class DMapPopulator{
		static Vector2[] directionsFour = new Vector2[]{
			new Vector2(0,1),new Vector2(1,0),new Vector2(0,-1),new Vector2(-1,0)
		};
		
		float distanceMax;
		bool[,]			roomsIsSeed;
		List<Vector2>	roomsSeed;
		internal List<DRoom>		roomsCreated;

		
		public DMapPopulator(int width, int height, Vector2 seed){
			if (width % 2 == 0) width++;
			if (height % 2 == 0)height++;

			distanceMax = 0;
			roomsIsSeed = new bool[width, height];
			roomsSeed = new List<Vector2> ();
			roomsCreated = new List<DRoom> ();
			AddSeed (seed);
			
			
		}
		bool helperIsPositionValid(DMap kMap, Vector2 pos){
			return pos.x >=0 && pos.x < kMap.width && pos.y >=0 && pos.y < kMap.height;
		}
		
		
		public bool Iterate(DMap kMap){
			if (roomsSeed.Count == 0) {
				Debug.Log("ERROR : NO MORE SEED LEFT");
				return false;
			}
			int index = Random.Range (0, roomsSeed.Count);
			Vector2 pos = roomsSeed [index];
			roomsSeed.RemoveAt (index);
			
			if (IsSeedLegit (kMap, pos)) {
				CreateRoom (kMap, pos);
				AddSeedAround (kMap, pos);
				return true;
			}
			return Iterate (kMap);
		}
		void CreateRoom(DMap map, Vector2 position){
			var room = (map[(int)position.x,(int)position.y] = new DRoom(position));
			roomsCreated.Add(map[(int)position.x,(int)position.y]);
			
			for (int i = 0; i < 4; i++) {
				var posNew = position + directionsFour[i];
				if(!helperIsPositionValid(map, posNew)) continue;
				var roomNew = map[(int)posNew.x,(int)posNew.y];
				if(roomNew == null) continue;
				
				room.doors[i] = true;
				roomNew.doors[(i+2)%4] =true;
				roomNew.distance = room.distance +1.0f;
				if(roomNew.distance > distanceMax)
					distanceMax = roomNew.distance;
				break;
			}
			
		}
		void AddSeed(Vector2 pos){AddSeed ((int)pos.x, (int)pos.y);}
		void AddSeed(int x, int y){
			if (roomsIsSeed [x, y]) return;
			roomsIsSeed [x, y] = true;
			roomsSeed.Add (new Vector2 (x, y));
		}
		void AddSeedAround(DMap kMap, Vector2 position){
			for (int i = 0; i <4; i++) {
				var posNew =position + directionsFour[i];
				if(!helperIsPositionValid(kMap,posNew))
					continue;
				AddSeed(posNew);
			}
		}
		
		bool IsSeedLegit(DMap kMap,Vector2 pos){
			int countEmptySpaces = 0;
			if (kMap [(int)pos.x, (int)pos.y] != null) 
				return false;
			for (int i = 0; i <4; i++) {
				var posNew = pos + directionsFour[i];
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
}
