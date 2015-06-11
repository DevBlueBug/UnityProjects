using UnityEngine;
using System.Collections;

public class RenderManager : MonoBehaviour
{
	public PlayerManager playerManager;
	void Awake(){
		NItem.NWeapon.Weapon.E_Backswing += H_Backswing;
	}
	void H_Backswing(Vector3 position, Vector3 direction, float force){
		force *= .1f;
		ChromaticEffect.E_NewForce (new ChromaticForce (position.x - direction.x*.2f,position.y - direction.y*.2f,force,.5f));

	}


}

