using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

namespace Data{
	/**
	public class DRoomLayout
	{
		public static Dictionary<TileType,string> dicTileStr;
		public static Dictionary<string,TileType> dicStrTile;
		static DRoomLayout(){
			dicTileStr = new Dictionary<TileType, string> (){
				{DRoomLayout.TileType.Ground,"a"},
				{DRoomLayout.TileType.Air,"b"},
				{DRoomLayout.TileType.Wall_Hard,"c"},
				{DRoomLayout.TileType.Wall_Soft,"d"},
				{DRoomLayout.TileType.Wall_Solid,"e"},
				{DRoomLayout.TileType.Trap,"f"},
				{DRoomLayout.TileType.Enemy,"g"},
				{DRoomLayout.TileType.Item,"h"}
			};
			dicStrTile = new Dictionary<string, TileType> ();
			foreach (var d in dicTileStr)
				dicStrTile.Add (d.Value,d.Key);

		}
		public static explicit operator DRoomLayout(string s){

			var json = SimpleJSON.JSON.Parse (s);
			var layout = new DRoomLayout (json ["width"].AsInt, json ["height"].AsInt);
			for (int i = 0; i < layout.width; i++)
			for (int j = 0; j < layout.height; j++) {
				//Debug.Log(i + " "  + j + " " +DRoomLayout.dicStrTile[ json["tiles"][i][j]]);
				layout.tiles[i,j] =  DRoomLayout.dicStrTile[json["tiles"][i][j]];
			}
			layout.doors [0] = json ["door"] [0].AsBool;
			layout.doors [1] = json ["door"] [1].AsBool;
			layout.doors [2] = json ["door"] [2].AsBool;
			layout.doors [3] = json ["door"] [3].AsBool;
			return layout;
		}
		public override string ToString ()
		{
			var node = JSON.Parse ("{}");
			node ["width"].AsInt = this.width;
			node ["height"].AsInt = this.height;
			var room = new JSONArray ();
			for (int i = 0; i < width; i++) {
				var row = new JSONArray ();
				for(int j = 0 ; j < height;j++){
					row[-1] = dicTileStr[ tiles[i,j]];
				}
				room[-1] = row;
			}
			node ["tiles"] = room;
			node ["door"] [-1].AsBool = doors [0];
			node ["door"] [-1].AsBool = doors [1];
			node ["door"] [-1].AsBool = doors [2];
			node ["door"] [-1].AsBool = doors [3];
		



			return node.ToString ();
		}
		public enum TileType {
			Ground,
			Air,
			Wall_Hard,
			Wall_Soft,
			Wall_Solid,
			Trap,
			Enemy,
			Item
		};
		public int width,height;
		public TileType[,] tiles;
		public bool[] doors;

		public DRoomLayout(int w, int h){
			width = w;
			height = h;
			doors = new bool[]{true,true,true,true};
			tiles = new TileType[w, h];

		}
	}
	public class DRoomLayout_Sort :IComparer<DRoomLayout>{
		public static int[] doorScore = new int[]{100000, 10000, 1000, 100};
		public int Compare(DRoomLayout a, DRoomLayout b){
			return GetValue(a) - GetValue(b);
		}
		int GetValue(DRoomLayout layout){
			int score = 0;
			for (int i = 0; i< 4; i++)
				if (layout.doors [i])
					score += doorScore [i];
			for (int i = 0; i < layout.width; i++)for(int j =  0 ; j < layout.height;j++){
				var tile = layout.tiles[i,j];
				if(tile == DRoomLayout.TileType.Enemy) score--;
			}
			return -score;
		}

	}
**/
}
