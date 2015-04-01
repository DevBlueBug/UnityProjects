using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Data;

namespace Game{
	
	public class GameMaster : MonoBehaviour
	{
		public int[][] dirClockwise = new int[][]{
			new int[]{0,1},new int[]{1,0},new int[]{0,-1},new int[]{-1,0}
		};
		//Settings 
		//different designs of rooms for "theme"
		public List<GTheme> Themes;
		public Entity.GEntity playerCharacter; 
		public PlayerController playerController;

		GMap myMap;
		GRoom myRoom;

		void Start ()
		{
			playerController.myEntity = playerCharacter;
			LoadNewLevel ();
		}
		
		// Update is called once per frame
		void Update ()
		{
			KUpdate ();
		}
		
		void Event_PlayerEnterDoor(int n){
			//Debug.Log ("EVENT_PLAYER_ENTER_DOOR " + n);
			var dir = dirClockwise [n];
			int newRoomX = myMap.roomAt_X + dir [0],
				newRoomY = myMap.roomAt_Y + dir [1];
			LoadRoom (newRoomX, newRoomY);
		}


		void LoadNewLevel(){
			var dataMap = new Game.Data.DMapGenerator ()
				.GenerateMap (10, 10, 30);
			var theme = Themes [Random.Range (0, Themes.Count)];

			myMap = new GMap ().Init (dataMap, theme.room,theme.entityPack );
			LoadRoom (myMap.width / 2, myMap.height / 2);
			//LinkRoom( myMap.DEBUG_ACTIVE_RANDOM_ROOM ());
			//myMap.roomActive.AddPlayer (playerCharacter, -1);
		}
		void LoadRoom(int x, int y){
			var room = myMap.ActiveRoom (x, y);
			LinkRoom (room);
			room.AddPlayer (playerCharacter,-1);
		}
		void LinkRoom(GRoom room){
			room.Event_EnterDoor = Event_PlayerEnterDoor;
		}

		void KUpdate(){
			playerCharacter.KUpdate ();
			playerController.KUpdate ();
			//myRoom.KUpdate ();
		}

	}
	

}