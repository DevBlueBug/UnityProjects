using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorBoard_Theme : MonoBehaviour
{
	public EditorPiece
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
	Dictionary<EditorPiece.KType,EditorPiece> pieces;
	void Awake ()
	{
		pieces = new Dictionary<EditorPiece.KType, EditorPiece>(){
			{EditorPiece.KType.Edge,entEdge},
			{EditorPiece.KType.Door,entDoor},
			{EditorPiece.KType.Ground,entGround},
			{EditorPiece.KType.Block_Soft,entBlockSoft},
			{EditorPiece.KType.Block_Hard,entBlockHard},
			{EditorPiece.KType.Decoration,entDecoration},
			{EditorPiece.KType.Item,entItem},
			{EditorPiece.KType.TrapFoot,entTrapFoot},
			{EditorPiece.KType.TrapTower,entTrapTower},
			{EditorPiece.KType.TrapGravity,entTrapGravity},
			{EditorPiece.KType.EnemyFoot,entEnemyFoot},
			{EditorPiece.KType.EnemyFlying,entEnemyFlying},
			{EditorPiece.KType.EnemyTower,entEnemyTower},
			{EditorPiece.KType.EnemyTurret,entEnemyTurret},
			
		};

	
	}
	public EditorPiece Get(EditorPiece.KType type){
		return pieces [type];
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

