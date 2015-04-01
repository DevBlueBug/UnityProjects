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
		//prefabs
		public GEntity 
			Entity_Floor,
			Entity_Boundry,
			Entity_Door;


		public Vector2 Index;
		public Delegates.D_EnteredDoor Event_EnterDoor = delegate(int w){Debug.Log("HEY " + w);};

		int width,height;

		GEntity myFloor;
		List<GEntity> myEntities;
		GEntity[,] mapEntities; 
	

		public GRoom Init(Data.DRoom r){
			this.width = r.width;
			this.height = r.height;
			myEntities = new List<GEntity> ();
			mapEntities = new GEntity[r.width, r.height];
			myFloor = Instantiate (Entity_Floor);
			myFloor.transform.parent = this.transform;
			myFloor.scale = new Vector3 (r.width, 1, r.height);
			myFloor.position = new Vector3 (r.width * .5f-.5f , 0, r.height *.5f-.5f);
			AddBoundries (Entity_Boundry, r.width, r.height);
			AddDoors (r.doors, Entity_Door, r.width, r.height);
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
		public void AddDoors(bool[] isDoored, GEntity entity, int w, int h){
			Vector3[] positions = new Vector3[]{
				new Vector3(w / 2, h - 1, 2),new Vector3(w -1, h /2, 3),
				new Vector3(w /2, 0,0 ),new Vector3( 0, h /2,1 )
			};
			for (int i = 0; i < 4; i++) {
				if(isDoored[i]){
					int n = i;
					AddMap(entity,(int)positions[i].x,(int)positions[i].y,(int)positions[i].z)
					.E_TriggerEnter += delegate {
						Event_EnterDoor(n);
					};
				}

			}
		}
		public GEntity AddMap(GEntity entity, int x , int y, int dirLooking){
			if (mapEntities [x, y] != null) {
				mapEntities [x, y].Kill();
			}
			mapEntities [x, y] = Instantiate (entity);
			mapEntities [x, y].transform.parent = this.transform;
			mapEntities [x, y].position = new Vector3 (x, 0, y);
			mapEntities [x, y].rotation = new Vector3 (0, 180 + 90 * dirLooking,0);
			return mapEntities [x, y];
		}
		
		public GEntity AddSimple(GEntity entity, int x , int y, int dirLooking){
			var e = Instantiate (entity);
			e.transform.parent = this.transform;
			e.position = new Vector3 (x, 0, y);
			e.rotation = new Vector3 (0, 180 + 90 * dirLooking,0);
			myEntities.Add (e);
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
		// Use this for initialization
		void Start ()
		{
		
		}
		public void KUpdate ()
		{
			for(int i =0 ; i < myEntities.Count;i++){
				myEntities[i].KUpdate();
			}
		
		}
	}
}