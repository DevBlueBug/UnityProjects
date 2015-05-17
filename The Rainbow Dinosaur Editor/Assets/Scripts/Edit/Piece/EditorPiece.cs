using UnityEngine;
using System.Collections;

public class EditorPiece : MonoBehaviour
{
	public delegate void D_Refreshed(EditorPiece me,EditorBoard board);
	public D_Refreshed E_Refreshed = delegate {};
	//public enum KType{ Edge, Ground, Door, Block ,Decoration, Enemy, Trap, Player};


	public SpriteRenderer meRenderer;
	//public KType meType;
	public Data.Piece.KType meType;
	public int hp;
	public bool 
		isAttacked_Bullet,
		isAttacked_Bomb;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	public void Refresh(EditorBoard world){
		E_Refreshed (this,world);
	}
}

