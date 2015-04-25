using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Entity;
using Data;

namespace Game{
	
	public class GRoomGenerator
	{

		public GRoom Generate(GTheme theme, Data.DRoom data){
			var room = new GameObject ("Room " + data.X + " " +data.Y).AddComponent<GRoom> ();
			room.Init (data.X,data.Y, data.width, data.height);
			room.width = data.width;
			room.height = data.height;
			room.myFloor =HelperInstantiate (theme.Room.Floor);
			room.myFloor.transform.parent = room.transform;
			room.myFloor.SetPosition(new Vector3 (data.width * .5f-.5f , 0, data.height *.5f-.5f))
						.SetScale(new Vector3 (data.width, 1, data.height));
			AddBoundries (room, theme.Room.Wall_Boundry, data.width, data.height);
			AddDoors (room, theme.Room.Door, data.doors, data.width, data.height);
			foreach (var dataUnit in data.entities) {

				if(dataUnit.myType == DEntity.MyType.Enemy){
					room.AddSimple (HelperToEntity(theme.Entities,dataUnit), dataUnit.x, dataUnit.y, dataUnit.dirLooking);
				}
				else 
					room.AddMap(HelperInstantiate(theme.Room.Wall_Boundry), dataUnit.x,dataUnit.y,0);
			}

			return room;
		}
		GEntity HelperInstantiate(GEntity entity){
			var e = GameObject. Instantiate (entity);
			return e;
		}
		
		public void AddBoundries(GRoom room, GEntity prefab, int w, int h){
			for (int i = 0; i < w; i++) {
				room.AddMap (HelperInstantiate(prefab), i, h-1,0);
				room.AddMap (HelperInstantiate(prefab), i, 0,0);
			}
			for (int i = 1; i < h; i++) {
				room.AddMap (HelperInstantiate(prefab), 0, i,0);
				room.AddMap (HelperInstantiate(prefab), w-1, i,0);
			}
		}
		public void AddDoors(GRoom room, GEntItem door, bool[] isDoored, int w, int h){
			for (int i = 0; i < 4; i++) {
				if (isDoored [i]){
					switch(i){
						case 0: 
							room.AddDoor(GameObject.Instantiate(door),w/2,h-1,2,i);break;
						case 1: 
						room.AddDoor(GameObject.Instantiate(door),w-1,h/2,3,i);break;
						case 2: 
						room.AddDoor(GameObject.Instantiate(door),w/2,0,0,i);break;
						case 3: 
						room.AddDoor(GameObject.Instantiate(door),0,h/2,1,i);break;
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
