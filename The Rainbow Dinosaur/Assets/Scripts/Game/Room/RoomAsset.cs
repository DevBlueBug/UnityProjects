using UnityEngine;
using Data;
using System.Collections.Generic;

public class RoomAsset : MonoBehaviour
{
	public Entity 
		EEmpty,
		EEdge,EGround,
		EDoor;
	Dictionary<Piece.KType, Entity> dic;
	void Awake(){
		dic = new Dictionary<Piece.KType, Entity> (){
			{Piece.KType.Empty, EEmpty},
			{Piece.KType.Edge, EEdge},
			{Piece.KType.Ground, EGround},
			{Piece.KType.Door, EDoor}
		};
	}
	public Entity Get(Data.Piece.KType type){
		//Debug.Log (type + " " + dic);
		return Instantiate(dic[type]);
		
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

