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

			LoadNewLevel ();
		}
		
		// Update is called once per frame
		void Update ()
		{
			KUpdate ();
		}
		
		void Event_PlayerEnterDoor(int n){
			var dir = dirClockwise [n];
			int x = myMap.roomActive.X + dir [0],
			y = myMap.roomActive.Y + dir [1];
			LoadRoom (myMap.Load (x, y));
		}


		void LoadNewLevel(){
			var dataMap = new Game.Data.DMapGenerator ()
				.GenerateMap (10, 10, 30);
			var theme = Themes [Random.Range (0, Themes.Count)];
			myMap = GMap.Generate (theme, dataMap);
			//myMap.roomActive.AddPlayer (playerEntity,-1);
			LoadRoom (myMap.roomActive);
		}
		void LoadRoom(GRoom room){
			room.Event_EnterDoor = Event_PlayerEnterDoor;
			player.EnterRoom (room);

		}
		void KUpdate(){
			player.KUpdate (myMap.roomActive);
			myMap.KUpdate ();
			//myRoom.KUpdate ();
		}

	}
	

}