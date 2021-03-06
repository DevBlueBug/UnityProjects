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

		int width,height;
		float distanceMax;
		bool[,]			roomsIsSeed;
		int[,]			roomsCost;
		List<Vector2>	roomsSeed;
		internal List<DRoom>		roomsCreated;
		internal DRoom[,]			roomsCreatedIndex;
		internal List<DRoom>		roomsDeadEnd;

		Vector2 roomInitIndex;
		public DRoom roomFar;
	
		public DMapPopulator(DMap map, Vector2 seed){
			this.width = map.width;
			this.height = map.height;

			//if (width % 2 == 0) width++;
			//if (height % 2 == 0)height++;

			this.roomInitIndex = seed;

			distanceMax = 0;
			roomsIsSeed = new bool[width, height];
			roomsCost = new int[width, height];
			roomsSeed = new List<Vector2> ();
			roomsCreated = new List<DRoom> ();
			roomsCreatedIndex = new DRoom[width,height];
			CreateRoom(map,seed);
			AddSeedAround (map,seed);
			
			
		}
		bool helperIsPositionValid(Vector2 pos){
			return pos.x >=0 && pos.x < width && pos.y >=0 && pos.y < height;
		}
		
		bool helperIsPositionValid(int posx, int posy){
			return posx >=0 && posx < width && posy >=0 && posy < height;
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
		public void IterateEnd(DMap map){
			roomsDeadEnd = new List<DRoom> ();
			bool[,] isChcked = new bool[width, height];
			IFindDeadEnds (map , ref isChcked,(int)roomInitIndex.x,(int)roomInitIndex.y);

		}
		bool IFindDeadEnds(DMap map,ref bool[,] isChecked, int x, int y){
			if (isChecked [x, y]) return false;
			isChecked [x, y] = true;

			bool isDeadEnd = true;
			for (int i = 0; i< 4; i++) {
				int xNew = x + (int)Utility.EasyUnity.dirFour3[i].x,
					yNew = y + (int)Utility.EasyUnity.dirFour3[i].y;
				if(!helperIsPositionValid(xNew,yNew) || isChecked[xNew,yNew]) continue;
				if(roomsCreatedIndex[xNew,yNew] != null){
					//had an adjacent room,
					isDeadEnd = false;
					IFindDeadEnds(map,ref isChecked, xNew,yNew);
				}
			}
			if (isDeadEnd) {
				roomsDeadEnd.Add(map[x,y]);
			}
			return isDeadEnd;

		}
		void CreateRoom(DMap map, Vector2 position){
			var room = new DRoom (position);
			roomsCreatedIndex [(int)position.x, (int)position.y] = room;

			if(roomsCost[(int)position.x,(int)position.y] > distanceMax){
				distanceMax = roomsCost[(int)position.x,(int)position.y];
				roomFar = room;
			}


			map [(int)position.x, (int)position.y] = room;
			roomsCreated.Add(map[(int)position.x,(int)position.y]);
			
			for (int i = 0; i < 4; i++) {
				var posNew = position + directionsFour[i];
				if(!helperIsPositionValid(posNew)) continue;
				var roomNew = map[(int)posNew.x,(int)posNew.y];
				if(roomNew == null) continue;
				room.doors[i] = true;
				roomNew.doors[(i+2)%4] =true;

				break;
			}
			
		}
		void AddSeed(Vector2 pos){AddSeed ((int)pos.x, (int)pos.y);}
		void AddSeed(int x, int y){
			if (roomsIsSeed [x, y]) return;
			roomsIsSeed [x, y] = true;
			roomsSeed.Add (new Vector2 (x, y));
		}
		void AddSeedAround(DMap kMap, Vector2 pos){
			for (int i = 0; i <4; i++) {
				var posNew =pos + directionsFour[i];
				if(!helperIsPositionValid(posNew))
					continue;
				roomsCost[(int)posNew.x,(int)posNew.y] = roomsCost[(int)pos.x,(int)pos.y] +1;
				AddSeed(posNew);
			}
		}
		
		bool IsSeedLegit(DMap kMap,Vector2 pos){
			int countEmptySpaces = 0;
			if (kMap [(int)pos.x, (int)pos.y] != null) 
				return false;
			for (int i = 0; i <4; i++) {
				var posNew = pos + directionsFour[i];
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

