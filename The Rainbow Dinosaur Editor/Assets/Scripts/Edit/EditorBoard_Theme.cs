using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorBoard_Theme : MonoBehaviour
{
	public EditorPiece
		entEmpty,
		entEdge,
		entGround,
		entDoor,
		
		entBlockSoft,
		entBlockHard,
		
		entDecoration,
		entItem,
		
		entTrapFoot,
		entTrapTower,
		entTrapGravity,
		
		entEnemyFoot,
		entEnemyFlying,
		entEnemyTower,
		entEnemyTurret;
	// Use this for initialization
	Dictionary<Data.Piece.KId,EditorPiece> pieces;
	void Awake ()
	{
		pieces = new Dictionary<Data.Piece.KId, EditorPiece>(){
			{Data.Piece.KId.Empty,entEmpty},
			{Data.Piece.KId.Edge,entEdge},
			{Data.Piece.KId.Door,entDoor},
			{Data.Piece.KId.Ground,entGround},
			{Data.Piece.KId.Block_Soft,entBlockSoft},
			{Data.Piece.KId.Block_Hard,entBlockHard},
			{Data.Piece.KId.Decoration,entDecoration},
			{Data.Piece.KId.Item,entItem},
			{Data.Piece.KId.TrapFoot,entTrapFoot},
			{Data.Piece.KId.TrapTower,entTrapTower},
			{Data.Piece.KId.TrapGravity,entTrapGravity},
			{Data.Piece.KId.EnemyFoot,entEnemyFoot},
			{Data.Piece.KId.EnemyFlying,entEnemyFlying},
			{Data.Piece.KId.EnemyTower,entEnemyTower},
			{Data.Piece.KId.EnemyTurret,entEnemyTurret},
			
		};

	
	}
	public EditorPiece Get(Data.Piece.KId type){
		pieces [type].meType = type;
		return pieces [type];
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

