using UnityEngine;
using Data;
using System.Collections.Generic;

public class RoomAsset : MonoBehaviour
{
	static Piece.KId[] IdsEnvironment = new Piece.KId[]{
		Piece.KId.Empty,Piece.KId.Edge,Piece.KId.Decoration,Piece.KId.Ground,
		Piece.KId.Block_Hard,Piece.KId.Block_Soft
	};
	static Piece.KId[] IdsEntities = new Piece.KId[]{
		Piece.KId.EnemyFlying,Piece.KId.EnemyFoot
	};

	public enum TypeDoor {Normal, Treasure,Boos, Secret };
	public RoomAsset_Entities assetEntities;
	public RoomAsset_Environment assetEnvironment;
	/**
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
		**/

	/**
	public Entity Get(Data.Piece.KId id){
		Entity entity;
		switch (id) {
		default: entity = EError; break;
		case Piece.KId.Empty:entity = EEmpty; break;
		case Piece.KId.Edge: entity = EEdge; break;
		case Piece.KId.Ground: entity = EGround; break;
		case Piece.KId.Door: entity = EDoor; break;
		case Piece.KId.Block_Hard: entity = EBlockHard; break;
		case Piece.KId.Block_Soft: entity = EBlockSoft; break;
		case Piece.KId.EnemyFoot: entity = EEnemyFoot; break;
		case Piece.KId.EnemyFlying: entity = EEnemyFlying; break;
		}
		entity.id = id;
		//Debug.Log (type + " " + dic);
		return Instantiate(entity);	
	}
	**/
	public Entity Get(Piece.KId id){
		foreach (var check in IdsEntities) {
			if(id == check) return assetEntities.Get(id);
		}
		foreach (var check in IdsEnvironment) {
			if(id == check) return assetEnvironment.Get(id);
		}
		throw new System.Exception ("GetNew Exception " + id);
	}
	public Entity GetBoss(int level){
		return assetEntities.GetBoss( level );
	}

	public EDoor GetDoor(DDoor.Types type){
		return assetEnvironment.GetDoor (type);
		/**
		Dictionary<DDoor.Types, EDoor> dic = new Dictionary<DDoor.Types, EDoor> (){
			{DDoor.Types.Normal,DoorNormal},
			{DDoor.Types.Treausre,DoorTreasure},
			{DDoor.Types.Secret,DoorSecret},
			{DDoor.Types.Boss,DoorBoss},
		};
		var obj = Instantiate (dic [type]);
		obj.type = type;
		return obj;
		**/

	}
	/**
	**/
	Entity GetEnemyFoot(){
		return null;
	}
	Entity GetFly(){
		return null;
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

