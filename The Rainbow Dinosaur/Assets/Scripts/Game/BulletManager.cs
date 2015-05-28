using UnityEngine;
using System.Collections.Generic;
public class BulletManager : MonoBehaviour{
	public enum KTypes
	{ 
		Normal
	}
	public delegate EBullet D_RequestBullet(KTypes type);
	public static D_RequestBullet E_RequestBullet;

	public EBullet 
		B_Normal;

	void Awake(){
		BulletManager.E_RequestBullet = H_RequestBullet;
	}
	EBullet H_RequestBullet(KTypes type){
		return Instantiate(B_Normal);
	}
	public void Link(Room room){

	}
	public EBullet RequestBullet(
		KTypes type, int damage, float range,float speed, Vector3 direction,
		List<Entity.KType> targets){

		return null;
	}
}