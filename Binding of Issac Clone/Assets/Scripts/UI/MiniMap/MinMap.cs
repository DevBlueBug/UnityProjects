using UnityEngine;
using System.Collections.Generic;

namespace UI.MiniMap{

	public class MinMap : MonoBehaviour
	{
		public MinMapRoom P_Room;
		public Camera camMap;
		public int width,height;
		MinMapRoom[,] rooms;
		// Use this for initialization
		void Start ()
		{
		}
		public void Reset(){
			rooms = new MinMapRoom[width, height];
			return;
			for (int i = 0; i < width; i++)
			for (int j = 0; j < height; j++) {
				rooms[i,j] = Utility.EasyInstantiate
					.InstantiateScale(P_Room.gameObject,this.transform)
						.GetComponent<MinMapRoom>();
				rooms[i,j].transform.localPosition = new Vector3(i,j,0);
				rooms[i,j].SetRoom(false);
				//rooms[i,j].gameObject.SetActive(false);
			}
		}
		void Update ()
		{
			var dicKeys = new Dictionary<KeyCode, Vector3>(){
				{KeyCode.I, new Vector3(0,1,0)},
				{KeyCode.K, new Vector3(0,-1,0)},
				{KeyCode.J, new Vector3(-1,0,0)},
				{KeyCode.L, new Vector3(1,0,0)}
			};
			foreach (var k in dicKeys) {
				if(Input.GetKeyDown(k.Key)){
					MoveCamera((int)k.Value.x,(int)k.Value.y);
				}
			}
		}
		public MinMapRoom this[int x, int y]{
			get{ 
				if(rooms[x,y]==null)AddNewRoom(x,y);
				return rooms [x, y];
			}
		}
		public MinMapRoom AddNewRoom(int x, int y){
			if (rooms [x, y] != null) {
				Destroy(rooms[x,y]);
			}
			rooms[x,y] = Utility.EasyInstantiate.InstantiateScale(P_Room.gameObject,this.transform)
				.GetComponent<MinMapRoom>();
			rooms [x, y].transform.localPosition = new Vector3 (x, y, 0);
			return rooms [x, y];
		}
		void MoveCameraTo(int x, int y){
			rooms [(int)camMap.transform.localPosition.x,
			      (int)camMap.transform.localPosition.y].SetRoom (false);
			camMap.transform.localPosition = new Vector3 (x, y, -1);
			rooms [x, y].gameObject.SetActive (true);
			rooms [x,y].SetRoom (true);

		}
		void MoveCamera(int x, int y){
			MoveCameraTo ((int)camMap.transform.localPosition.x+x,
			              (int)camMap.transform.localPosition.y+y);
		}
		// Update is called once per frame

	}

}
