using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	public float velocity;
	public EntityMove entity;

	Dictionary<KeyCode,Vector3> dirInputVelocity = new Dictionary<KeyCode, Vector3>(){
		{KeyCode.W, Vector3.up},
		{KeyCode.D, Vector3.right},
		{KeyCode.S, Vector3.down},
		{KeyCode.A, Vector3.left}
	};
	// Update is called once per frame
	public void KUpdate ()
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
		
		if(isChanged) entity.Move(direction.normalized * velocity );
	
	}
}

