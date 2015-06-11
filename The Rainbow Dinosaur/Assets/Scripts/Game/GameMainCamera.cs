using UnityEngine;
using System.Collections;

public class GameMainCamera : MonoBehaviour
{
	
	Vector3 center = new Vector3 (7,4,0);
	// Use this for initialization
	void Awake ()
	{
		PlayerManager.E_PlayerPositionChanged += PlayerPositionChanged;
	}
	void PlayerPositionChanged(Vector3 pos){
		Vector3 dis = pos - center;
		var position = center + dis * .1f;
		this.transform.localPosition = new Vector3 (position.x,position.y,this.transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

