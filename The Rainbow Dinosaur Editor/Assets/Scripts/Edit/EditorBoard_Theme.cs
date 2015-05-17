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
	Dictionary<Data.Piece.KType,EditorPiece> pieces;
	void Awake ()
	{
		pieces = new Dictionary<Data.Piece.KType, EditorPiece>(){
			{Data.Piece.KType.Empty,entEmpty},
			{Data.Piece.KType.Edge,entEdge},
			{Data.Piece.KType.Door,entDoor},
			{Data.Piece.KType.Ground,entGround},
			{Data.Piece.KType.Block_Soft,entBlockSoft},
			{Data.Piece.KType.Block_Hard,entBlockHard},
			{Data.Piece.KType.Decoration,entDecoration},
			{Data.Piece.KType.Item,entItem},
			{Data.Piece.KType.TrapFoot,entTrapFoot},
			{Data.Piece.KType.TrapTower,entTrapTower},
			{Data.Piece.KType.TrapGravity,entTrapGravity},
			{Data.Piece.KType.EnemyFoot,entEnemyFoot},
			{Data.Piece.KType.EnemyFlying,entEnemyFlying},
			{Data.Piece.KType.EnemyTower,entEnemyTower},
			{Data.Piece.KType.EnemyTurret,entEnemyTurret},
			
		};

	
	}
	public EditorPiece Get(Data.Piece.KType type){
		pieces [type].meType = type;
		return pieces [type];
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

