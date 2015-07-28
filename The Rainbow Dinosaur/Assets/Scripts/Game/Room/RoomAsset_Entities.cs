using UnityEngine;
using System.Collections.Generic;
using Data;

public class RoomAsset_Entities : MonoBehaviour
{
	public List<Entity> 
		EnemyFoot,
		EnemyFlying;
	//Fly FAttack,FAvoid,FWanderer,FDancer
	public List<Entity> Bosses;

	public Entity Get(Piece.KId id){
		Entity e = GetPrefab(id);
		if(e== null) throw new System.ArgumentException ();
		else return Instantiate(e);
	}
	Entity GetPrefab(Piece.KId id){
		switch (id) {
		case Piece.KId.EnemyFlying:
			return EnemyFlying[Random.Range(0,EnemyFlying.Count) ];
		case Piece.KId.EnemyFoot:
			return EnemyFoot[Random.Range(0,EnemyFoot.Count) ];
		}
		return null;
	}
	public Entity GetBoss(int level){
		return Instantiate(Bosses[Random.Range(0,Bosses.Count)]);
	}
}

