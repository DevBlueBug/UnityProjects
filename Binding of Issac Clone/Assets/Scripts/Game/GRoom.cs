using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Game.Entity;

namespace Game{
	public class GRoom : MonoBehaviour
	{
		static Dictionary<int,Vector3> DicPlayerStartingPosition = new Dictionary<int, Vector3>(){
			{-1, new Vector3(.5f,0,.5f)},
			{0, new Vector3(.5f,0,1)},
			{1, new Vector3(1, 0,.5f)},
			{2, new Vector3(.5f, 0, 0)},
			{3, new Vector3(0, 0,.5f)}
		};
		
		public Delegates.D_EnteredDoor Event_EnterDoor = delegate(int w){Debug.Log("HEY " + w);};
		
		public int width,height;
		public Vector2 Index;
		public AStar.KMap mapAstar;


		public GEntity myFloor;
		public List<GEntity> myEntities;
		public GEntity[,] mapEntities; 
		
		public int X {get{return (int)Index.x;}}
		public int Y {get{return (int)Index.y;}}
		


		public GRoom Init(int x, int y, int w, int h){
			Index = new Vector2 (x, y);
			SetSize (w,h);
			myEntities = new List<GEntity> ();
			return this;
		}
		
		// Use this for initialization
		void Start ()
		{
			
		}
		public void KUpdate ()
		{
			for(int i =0 ; i < myEntities.Count;i++){
				myEntities[i].KUpdate(this);
			}
			
		}
		public GRoom SetSize(int width,int height){
			this.width = width;
			this.height = height;
			mapEntities = new GEntity[width, height];
			mapAstar = new AStar.KMap (width, height);
			return this;
			
		}

		public void AddBoundries(GEntity E_Boundry, int w, int h){
			for (int i = 0; i < w; i++) {
				AddMap (E_Boundry, i, h-1,0);
				AddMap (E_Boundry, i, 0,0);
			}
			for (int i = 1; i < h; i++) {
				AddMap (E_Boundry, 0, i,0);
				AddMap (E_Boundry, w-1, i,0);
			}
		}
		public void AddDoor(GEntity entity, int x, int y, int dirLooking, int dirHeaded){
			AddMap (entity, x, y, dirLooking);
			entity.E_OnTriggerEnter += delegate{Event_EnterDoor (dirHeaded);};
		}
		public GEntity AddMap(GEntity entity, int x , int y, int dirLooking){
			if (mapEntities [x, y] != null) {
				mapEntities [x, y].Kill();
			}

			mapAstar [x, y].isAlive = false;
			mapEntities [x, y] = entity;
			//LINK
			entity.E_Killed += Hdr_Kill_RemoveFromMap;
			AddSimple (entity,x,y,dirLooking);
			return entity;
		}
		
		public GEntity AddSimple(GEntity e, int x , int y, int dirLooking){
			e.transform.parent = this.transform;
			e.position = new Vector3 (x, 0, y);
			e.rotation = new Vector3 (0, 180 + 90 * dirLooking,0);
			myEntities.Add (e);
			LinkEntity (e);
			return e;
		}

		public void AddPlayer(GEntity playerEntity, int direction){
			playerEntity.transform.parent = this.transform;
			foreach (var k in DicPlayerStartingPosition)
				if (direction == k.Key)
					playerEntity.position = new Vector3 (
						(int)(width * k.Value.x), 0, 
						(int)(height * k.Value.z));// * k.Value;
		}
		public void LinkEntity(GEntity e){
			e.E_Killed += Hdr_Kill_RemoveFromList;
			e.E_Birth += Hdr_Birth;
		}
		public void UnLinkEntity(GEntity e){
			e.E_Killed -= Hdr_Kill_RemoveFromList;
			e.E_Birth -= Hdr_Birth;
		}
		//Hdrs
		void Hdr_Birth(GEntity me, GEntity entity){
			entity.transform.parent = this.transform;
			myEntities.Add (entity);
			LinkEntity (entity);
		}
		void Hdr_Kill_RemoveFromMap(GEntity entity){
			mapEntities [(int)entity.position.x, (int)entity.position.z] = null;
			mapAstar.Reset ((int)entity.position.x, (int)entity.position.z);

		}
		void Hdr_Kill_RemoveFromList(GEntity entity){
			myEntities.Remove (entity);
		}
	}
}