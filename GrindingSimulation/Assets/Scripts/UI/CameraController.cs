using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
	
	public GameObject cam;
	Dictionary<KeyCode, Vector2> dirCam = new Dictionary<KeyCode, Vector2>(){
		{KeyCode.W, Vector3.up},
		{KeyCode.S, Vector3.down},
		{KeyCode.A, Vector3.left},
		{KeyCode.D, Vector3.right}
	};
	void Update ()
	{
		foreach (var d in dirCam) {
			if(Input.GetKey(d.Key) ){
				cam.transform.localPosition += new Vector3(d.Value.x,d.Value.y,0) * 10.0f * Time.deltaTime ;
			}
		}
		
	}
}

