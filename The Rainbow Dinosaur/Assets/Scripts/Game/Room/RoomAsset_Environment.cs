using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Data;

public class RoomAsset_Environment : MonoBehaviour
{

	public enum TypeDoor {Normal, Treasure,Boos, Secret };


	public Entity 
		Error,
		Empty,
		Edge, Ground,
		BlockSoft,
		BlockHard;
	public EDoor
		DoorNormal,
		DoorTreasure,
		DoorSecret,
		DoorBoss;


	public Entity Get(Data.Piece.KId id){
		Entity entity;
		switch (id) {
		default: 
			entity = Error;
			Debug.Log("Get ERROR"  + id);
			break;
		case Piece.KId.Empty:entity = Empty; break;
		case Piece.KId.Edge: entity = Edge; break;
		case Piece.KId.Ground: entity = Ground; break;
		case Piece.KId.Block_Hard: entity = BlockHard; break;
		case Piece.KId.Block_Soft: entity =BlockSoft; break;
		}
		entity.id = id;
		return Instantiate(entity);	
	}
	public EDoor GetDoor(DDoor.Types type){

		Dictionary<DDoor.Types, EDoor> dic = new Dictionary<DDoor.Types, EDoor> (){
			{DDoor.Types.Normal,DoorNormal},
			{DDoor.Types.Treausre,DoorTreasure},
			{DDoor.Types.Secret,DoorSecret},
			{DDoor.Types.Boss,DoorBoss},
		};
		
		var obj = Instantiate (dic [type]);
		obj.type = type;
		return obj;
	}
}

