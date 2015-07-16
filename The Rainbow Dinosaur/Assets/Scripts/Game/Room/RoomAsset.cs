using UnityEngine;
using Data;
using System.Collections.Generic;

public class RoomAsset : MonoBehaviour
{
	public enum TypeDoor {Normal, Treasure,Boos, Secret };



	public Entity 
		EError,
		EEmpty,
		EEdge, EGround,
		EDoor,
		EBlockHard,
		EBlockSoft,
		EEnemyFoot,
		EEnemyFlying;
	public Enemy
		Fly,
		FlyAttack,
		FlyAvoid,
		FlyWanderer,
		FlyDancer;
	public Entity
		Boss00;
	public EDoor
		DoorNormal,
		DoorTreasure,
		DoorSecret,
		DoorBoss;
	Dictionary<TypeDoor, EDoor> dicDoors; 

	public Entity Get(Data.Piece.KId id){
		Entity entity;
		switch (id) {
		default: entity = EError; break;
		case Piece.KId.Empty:entity = EEmpty; break;
		case Piece.KId.Edge: entity = EEdge; break;
		case Piece.KId.Ground: entity = EGround; break;
		case Piece.KId.Door: entity = EDoor; break;
		case Piece.KId.EnemyFoot: entity = EEnemyFoot; break;
		case Piece.KId.Block_Hard: entity = EBlockHard; break;
		case Piece.KId.Block_Soft: entity = EBlockSoft; break;
		case Piece.KId.EnemyFlying: entity = EEnemyFlying; break;
		}
		entity.id = id;
		//Debug.Log (type + " " + dic);
		return Instantiate(entity);	
	}
	public Entity GetBoss(int level){
		return Instantiate( Boss00 );
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
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

