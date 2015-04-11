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

		GMap myMap;
		public GPlayer player;

		void Start ()
		{
			player.E_NewPlayer ();
			LoadNewLevel ();
			player.E_NewRoom(myMap.roomActive,-1);
		}
		
		// Update is called once per frame
		void Update ()
		{
			KUpdate ();
		}
		void FixedUpdate(){
			KFixedUpdate ();
		}
		
		void Event_PlayerEnterDoor(int n){
			var dir = dirClockwise [n];
			int x = myMap.roomActive.X + dir [0],
			y = myMap.roomActive.Y + dir [1];
			LinkRoom (myMap.Load (x, y));
			player.E_NewRoom (myMap.roomActive,n);
		}


		void LoadNewLevel(){
			var dataMap = new Game.Data.DMapGenerator ()
				.GenerateMap (10, 10, 30);
			var theme = Themes [Random.Range (0, Themes.Count)];
			myMap = GMap.Generate (theme, dataMap);
			//myMap.roomActive.AddPlayer (playerEntity,-1);
			LinkRoom (myMap.roomActive);
		}
		void LinkRoom(GRoom room){
			room.Event_EnterDoor = Event_PlayerEnterDoor;

		}
		void KFixedUpdate(){
			player.KFixedUpdate (myMap.roomActive);
			myMap.KFixedUpdate ();
		}
		void KUpdate(){
			player.KUpdate (myMap.roomActive);
			myMap.KUpdate ();
			//myRoom.KUpdate ();
		}

	}
	

}