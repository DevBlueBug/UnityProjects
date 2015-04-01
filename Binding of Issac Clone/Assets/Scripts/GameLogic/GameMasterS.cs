using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameLogic.Entity;
using GameLogic.Delegates;

namespace GameLogic {
	public class GameMasterS : MonoBehaviour
	{
		//public event D_NewRoom E_NewRoom;


		public D_NewRoom E_NewRoom = delegate{};
		
		public EntityBase playerUnit;
		public PlayerControllerwww playerController;


		GameMap myMap;
		DataRoom roomAt;
		//bool isUpdated = true;
		
		//public RenderMaster renderMaster; 
		// Use this for initialization
		void Start ()
		{
			playerController.myEntity = playerUnit;
			NewLevel ();
		}
		void NewLevel(){
			myMap = new GameMapGenerator().GenerateRooms(10, 10, 30);
			LoadNewRoom (myMap.roomAt);
		}
		void LoadNewRoom(DataRoom room){
			E_NewRoom( myMap.roomAt);
			playerUnit.position = new Vector3 (room.width/2,0, room.height/2);
		}
		
		// Update is called once per frame
		void Update ()
		{
			Update_DebugMap ();
		}
		
		Dictionary<KeyCode,Vector2> dicArrows = new Dictionary<KeyCode, Vector2> (){
			{KeyCode.DownArrow,new Vector2(0,-1)},
			{KeyCode.UpArrow, new Vector2(0,1)},
			{KeyCode.LeftArrow, new Vector2(-1,0)},
			{KeyCode.RightArrow, new Vector2(1,0)},
		};
		void Update_DebugMap(){
			foreach (var d in dicArrows) {
				if(Input.GetKeyDown(d.Key) ){
					if(myMap.Move(d.Value )) LoadNewRoom(myMap.roomAt) ;
				}
			}
		}
	}
}
	
	

