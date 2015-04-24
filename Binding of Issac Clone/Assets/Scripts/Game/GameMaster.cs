using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Data;
using UI.MiniMap;

namespace Game{
	
	public class GameMaster : MonoBehaviour
	{
		public int[][] dirClockwise = new int[][]{
			new int[]{0,1},new int[]{1,0},new int[]{0,-1},new int[]{-1,0}
		};
		public MinMap mapMini;
		//Settings 
		//different designs of rooms for "theme"
		public List<GTheme> Themes;

		GMap mapGame;
		public GPlayer player;


		void Start ()
		{
			player.E_NewPlayer ();
			LoadNewLevel (out mapGame, 10,10,30);
			Link (mapMini, mapGame);

		}
		void Link(UI.MiniMap.MinMap mapMini, GMap mapGame){
			mapMini.width = mapGame.width;
			mapMini.height = mapGame.height;
			mapMini.Reset ();
			//Dictionary<MinMapRoom.KType,MinMapRoom.KType> dicType;
			//Dictionary<MinMapRoom.KState,MinMapRoom.KState> dicState;

			for(int i = 0;i < mapGame.width;i++)for(int j = 0 ; j < mapGame.height;j++){
				var roomGame =mapGame[i,j];
				if(roomGame==null)continue;
				mapMini[i,j].SetRoom(true);
				//mapMini[i,j].SetState
			}



		}

		
		// Update is called once per frame
		void Update ()
		{
			KUpdate ();
		}
		void FixedUpdate(){
			KFixedUpdate ();
		}
		void EVENT_LOAD_NEW_ROOM(GRoom room, int playerDirection){
			player.E_NewRoom(room,playerDirection);
			mapMini.MoveCameraTo(room.X, room.Y);
			LinkRoom (mapGame.roomActive);
		}
		void HDR_PlayerEnteredRoom(int n){
			var dir = dirClockwise [n];
			int x = mapGame.roomActive.X + dir [0],
			y = mapGame.roomActive.Y + dir [1];
			EVENT_LOAD_NEW_ROOM (mapGame.Load (x, y), n);
		}


		void LoadNewLevel(out GMap mapGame, int w, int h, int roomNum){
			var dataMap = new Game.Data.DMapGenerator ()
				.GenerateMap (w,h, roomNum);
			var theme = Themes [Random.Range (0, Themes.Count)];
			mapGame = GMap.Generate (theme, dataMap);
			EVENT_LOAD_NEW_ROOM (mapGame.roomActive, -1);
			//myMap.roomActive.AddPlayer (playerEntity,-1);


		}
		void LinkRoom(GRoom room){
			room.Event_EnterDoor = HDR_PlayerEnteredRoom;

		}
		void KFixedUpdate(){
			player.KFixedUpdate (mapGame.roomActive);
			mapGame.KFixedUpdate ();
		}
		void KUpdate(){
			player.KUpdate (mapGame.roomActive);
			mapGame.KUpdate ();
			//myRoom.KUpdate ();
		}

	}
	

}