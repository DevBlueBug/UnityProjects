using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
public class EditorJsonWrapper
{
	public static Dictionary<EditorPiece.KType,string> dicTypeString;
	public static Dictionary<string,EditorPiece.KType> dicStringType;
	static EditorJsonWrapper(){
		dicTypeString = new Dictionary<EditorPiece.KType, string> (){
			{EditorPiece.KType.World,"A"},
			{EditorPiece.KType.Block_Hard,"B"},
			{EditorPiece.KType.Block_Soft,"C"},
			{EditorPiece.KType.Decoration,"D"},
			{EditorPiece.KType.Door,"E"},
			{EditorPiece.KType.Edge,"F"},
			{EditorPiece.KType.EnemyFlying,"G"},
			{EditorPiece.KType.EnemyFoot,"H"},
			{EditorPiece.KType.EnemyTower,"I"},
			{EditorPiece.KType.EnemyTurret,"J"},
			{EditorPiece.KType.Ground,"K"},
			{EditorPiece.KType.Item,"L"},
			{EditorPiece.KType.Player,"M"},
			{EditorPiece.KType.TrapFoot,"N"},
			{EditorPiece.KType.TrapGravity,"O"},
			{EditorPiece.KType.TrapTower,"P"}

		};
		dicStringType = new Dictionary<string, EditorPiece.KType> ();
		foreach (var pair in  dicTypeString) {
			dicStringType.Add(pair.Value,pair.Key);
		}
	}
	public static JSONNode helperToNode(EditorPiece piece, int x, int y){
		if (piece == null) return null;
		var node = JSON.Parse ("{}");
		node["X"].AsInt = x;
		node["Y"].AsInt = y;
		node["Type"] = dicTypeString[piece.meType];
		return node;
	}
	public static void helperUnwrap(ref JSONArray  array ,ref List<EditorPiece_Data> list){
		//return null;
		for (int i = 0; i < array.Count; i++) {
			var arWorld = array[i];
			var dataPieceWorld = new EditorPiece_Data(
				dicStringType[ arWorld["Type"]],arWorld["X"].AsInt,arWorld["Y"].AsInt);
			list.Add(dataPieceWorld);
		}
	}
	public static bool ToBoardData(string s,out EditorBoard_Data data){
		data = new EditorBoard_Data ();
		if (s == null) return false;
		try{
			var node = JSON.Parse (s);

			for (int i = 0; i < 4; i++) {
				data.doors[i] = node["door"][i].AsBool;
			}
			var arrWorld = node ["piecesWorld"].AsArray;
			var arrUnits = node ["piecesUnits"].AsArray;
			helperUnwrap (ref arrWorld, ref data.piecesWorld);
			helperUnwrap (ref arrUnits, ref data.piecesUnits);


			return true;
		}
		catch{
			return false;
		}
	}
	public static JSONNode ToJson(EditorPiece_Data data){
		var node = JSON.Parse ("{}");
		node["X"].AsInt = data.X;
		node["Y"].AsInt = data.Y;
		node["Type"] = dicTypeString[data.type];
		return node;
	}
	public static JSONNode ToJson(EditorBoard_Data data){
		var node = JSON.Parse ("{}");
		JSONArray 
			listPiecesWorld = new JSONArray (),
			listPiecesUnits = new JSONArray ();
		node ["door"] [-1].AsBool = data.doors [0];
		node ["door"] [-1].AsBool = data.doors [1];
		node ["door"] [-1].AsBool = data.doors [2];
		node ["door"] [-1].AsBool = data.doors [3];
		
		foreach (var pieceWorld in data.piecesWorld) {
			listPiecesWorld.Add(ToJson(pieceWorld));	
		}
		foreach (var pieceUnit in data.piecesUnits) {
			listPiecesUnits.Add(ToJson(pieceUnit));
		}
		node ["piecesWorld"] = listPiecesWorld;
		node ["piecesUnits"] = listPiecesUnits;
		//Debug.Log ("ToJson EditorBoard_Data " + node.ToString ());
		return node;
	}
	public static string ToJson(EditorBoard board){
		var node = JSON.Parse ("{}");
		JSONArray 
			listPiecesWorld = new JSONArray (),
			listPiecesUnits = new JSONArray ();
		node ["door"] [-1].AsBool = board.entitiesWorld [7] [8].meType == EditorPiece.KType.Door;
		node ["door"] [-1].AsBool = board.entitiesWorld [14] [4].meType == EditorPiece.KType.Door;
		node ["door"] [-1].AsBool = board.entitiesWorld [7] [0].meType == EditorPiece.KType.Door;
		node ["door"] [-1].AsBool = board.entitiesWorld [0] [4].meType == EditorPiece.KType.Door;

		for (int i = 1; i < board.width-1; i++)
		for (int j = 1; j < board.height-1; j++) {
			var nodeWorld = helperToNode(board.entitiesWorld[i][j],i,j);
			if(nodeWorld !=null) 
				listPiecesWorld[-1] = nodeWorld;
			
			var nodeUnit = helperToNode(board.entitiesUnits[i][j],i,j);
			if(nodeUnit !=null) 
				listPiecesUnits[-1] = nodeUnit;
		}
		node ["piecesWorld"] = listPiecesWorld;
		node ["piecesUnits"] = listPiecesUnits;
		return node.ToString ();
	}
}

