using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Game.Entity;

namespace Game{
	public class GRoom : MonoBehaviour
	{
		public enum KType {Normal,Treasure,Boss,Secret }
		public enum KState {None, HasGold,HasHeart,HasBomb }
		static Dictionary<int,Vector3> DicPlayerStartingPosition = new Dictionary<int, Vector3>(){
			{-1, new Vector3(.5f,.5f,0)},
			{0, new Vector3(.5f,1,0)},
			{1, new Vector3(1, .5f,0)},
			{2, new Vector3(.5f, 0, 0)},
			{3, new Vector3(0, .5f,0)}
		};
		
		public Delegates.D_EnteredDoor Event_EnterDoor = delegate(int w){Debug.Log("HEY " + w);};

		public KType myType;
		public KState myState;
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
		public void KFixedUpdate(){
			for(int i =0 ; i < myEntities.Count;i++){
				myEntities[i].KFixedUpdate(this);
			}
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
		public void AddDoor(GEntItem entity, int x, int y, int dirLooking, int dirHeaded){
			AddMap (entity, x, y, dirLooking);
			entity.hitbox.onDoMe += delegate{Event_EnterDoor (dirHeaded);};
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
			e.position = new Vector3 (x, y, 0);
			e.rotation = new Vector3 (0, 180 + 90 * dirLooking,0);
			myEntities.Add (e);
			LinkEntity (e);
			return e;
		}

		public void AddPlayer(GEntity playerEntity, int direction){
			playerEntity.transform.parent = this.transform;
			foreach (var k in DicPlayerStartingPosition)
				if (direction == k.Key) {
				
				var respawnPoint = new Vector3 (
					(int)(width * k.Value.x), 
					(int)(height * k.Value.y),
					0);// * k.Value;

				var ToCenter = (new Vector3((int)(width*.5f),(int)(height*.5f),0 ) - respawnPoint).normalized;
				playerEntity.position = respawnPoint + ToCenter*1.3f;
				Debug.Log("PLAYER POSITON "  + (respawnPoint + ToCenter*1.3f));

				}
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