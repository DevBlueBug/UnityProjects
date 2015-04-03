using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Entity;

namespace Game{
	
	public class GRoomGenerator
	{

		public GRoom Generate(GTheme theme, Data.DRoom data){
			var room = new GameObject ("Room " + data.X + " " +data.Y).AddComponent<GRoom> ();
			room.Init (data.X,data.Y, data.width, data.height);
			room.width = data.width;
			room.height = data.height;
			room.myFloor = HelperInstantiate(theme.Room.Floor,room.transform)
					.SetPosition(new Vector3 (data.width * .5f-.5f , 0, data.height *.5f-.5f))
					.SetScale(new Vector3 (data.width, 1, data.height));
			AddBoundries (room, theme.Room.Wall_Boundry, data.width, data.height);
			AddDoors (room, theme.Room.Door, data.doors, data.width, data.height);
			foreach (var dataUnit in data.entities) {
				room.AddSimple (HelperToEntity(theme.Entities,dataUnit), dataUnit.x, dataUnit.y, dataUnit.dirLooking);
			}
			return room;
		}
		GEntity HelperInstantiate(GEntity entity, Transform parent){
			var e = GameObject. Instantiate (entity);
			e.transform.parent = parent;
			return e;
		}
		
		public void AddBoundries(GRoom room, GEntity prefab, int w, int h){
			for (int i = 0; i < w; i++) {
				room.AddMap (HelperInstantiate(prefab, room.transform), i, h-1,0);
				room.AddMap (HelperInstantiate(prefab, room.transform), i, 0,0);
			}
			for (int i = 1; i < h; i++) {
				room.AddMap (HelperInstantiate(prefab, room.transform), 0, i,0);
				room.AddMap (HelperInstantiate(prefab, room.transform), w-1, i,0);
			}
		}
		public void AddDoors(GRoom room, GEntity door, bool[] isDoored, int w, int h){
			for (int i = 0; i < 4; i++) {
				if (isDoored [i]){
					switch(i){
						case 0: 
							room.AddDoor(HelperInstantiate(door,room.transform),w/2,h-1,2,i);break;
						case 1: 
							room.AddDoor(HelperInstantiate(door,room.transform),w-1,h/2,3,i);break;
						case 2: 
							room.AddDoor(HelperInstantiate(door,room.transform),w/2,0,0,i);break;
						case 3: 
							room.AddDoor(HelperInstantiate(door,room.transform),0,h/2,1,i);break;
					}
				}
			}
		}
		GEntity HelperToEntity(GTheme.GThemeEntities entities, Data.DEntity data){
			var prefab = entities.Simple [Random.Range (0, entities.Simple.Count)];
			return GameObject.Instantiate(prefab);
		}
	}

}
