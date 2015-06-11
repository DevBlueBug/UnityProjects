using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	
	Dictionary<KeyCode,Vector3> dirInputVelocity = new Dictionary<KeyCode, Vector3>(){
		{KeyCode.W, Vector3.up},
		{KeyCode.D, Vector3.right},
		{KeyCode.S, Vector3.down},
		{KeyCode.A, Vector3.left}
	};
	
	Dictionary<KeyCode,Vector3> dirInputAttack = new Dictionary<KeyCode, Vector3>(){
		{KeyCode.I, Vector3.up},
		{KeyCode.L, Vector3.right},
		{KeyCode.K, Vector3.down},
		{KeyCode.J, Vector3.left}
	};
	// Update is called once per frame
	public void KUpdate (PlayerManager manager, EntityMove entityPlayer, Room room)
	{
		bool isChanged = false;
		Vector3 direction = Vector3.zero;
		foreach (var v in dirInputVelocity) {
			if(Input.GetKey(v.Key)){
				isChanged = true;
				direction+= v.Value;
				//Debug.Log(v.Key);
			}
		}
		foreach (var v in dirInputAttack) {
			if(Input.GetKey(v.Key)){
				manager.E_Attack(entityPlayer,room, v.Value);
			}
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			manager.E_Bomb(entityPlayer,room);
		}
		
		if(isChanged) manager.E_Move(direction.normalized );
	
	}
}

