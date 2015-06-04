using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace  Data {

	public class DMapGenerator
	{
		DMap GetMap(int width, int height){
			DMap map = new DMap(width,height);
			map[width/2,height/2]= new DRoom(new Vector2(width/2,height/2));
			map.roomInit = map[width/2,height/2];
			return map;
		}
		public DMap GenerateMap(int width, int height, int count){
			var map = GetMap (width, height);
			List<DMapPopulator> branches = GetPopulators (map);
			IterateBranches (map,branches, count, (int)(count * .3 ) );

			map.roomInit.meType = DRoom.KType.Start;
			branches [0].roomFar.meType = DRoom.KType.Boss;


			for (int i = 1; i < branches.Count; i++) {
				for(int j = 0; j < branches[i].roomsDeadEnd.Count;j++){
					var room = branches[i].roomsDeadEnd[j];
					room.meType = (Random.Range(0,2)==0)?
						DRoom.KType.Treasure:
							DRoom.KType.Secret;
				}
			}
			IterateDoorUpdates (map, map.roomInit);

			return map;
		}
		
		List<DMapPopulator> GetPopulators(DMap map){
			List<Vector2> fourDirections = new List<Vector2>(){
				new Vector2(map.width/2,map.height/2+1),
				new Vector2(map.width/2+1,map.height/2),
				new Vector2(map.width/2,map.height/2-1),
				new Vector2(map.width/2-1,map.height/2)
			};
			List<DMapPopulator> branches = new List<DMapPopulator> ();
			int branchNum = Random.Range (2, 5);
			for (int i = 0; i < branchNum; i++) {
				int directionNum = Random.Range(0,100)%fourDirections.Count;
				var directionChosen = fourDirections[directionNum];
				fourDirections.RemoveAt(directionNum);
				branches.Add(new DMapPopulator(map, directionChosen ));
			}
			return branches;
			
		}

		void IterateBranches(DMap map, List<DMapPopulator> branches, int roomCount, int roomCountOthers){
			
			for (int i = 0; i < roomCountOthers; i++) {
				for(int j = 0 ; j < branches.Count;j++)
					branches[j].Iterate(map);
			}
			for (int i = 0; i < roomCount - roomCountOthers; i++) {
				branches[0].Iterate(map);
			}
			for (int i = 0; i< branches.Count; i++) {
				branches[i].IterateEnd(map);
			}
		}
		public Dictionary<DRoom.KType,DDoor.Types> dicRoomDoorTypes = new Dictionary<DRoom.KType, DDoor.Types>(){
			{DRoom.KType.Normal, DDoor.Types.Normal},
			{DRoom.KType.Treasure, DDoor.Types.Treausre},
			{DRoom.KType.Secret, DDoor.Types.Secret},
			{DRoom.KType.Boss, DDoor.Types.Boss},
			
			{DRoom.KType.Start, DDoor.Types.Normal}
		};
		void IterateDoorUpdates(DMap map, DRoom room){
			for (int i = 0; i< 4; i++) {
				if(!room.doors[i] || room.doorss[i] != null) continue;
				int 
					x = room.X + (int)Utility.EasyUnity.dirFour3[i].x,
					y= room.Y + (int)Utility.EasyUnity.dirFour3[i].y;
				room.doorss[i] = new DDoor();
				room.doorss[i].type = dicRoomDoorTypes[map[x,y].meType];
				IterateDoorUpdates(map,map[x,y]);
			}
		}
	}
}
