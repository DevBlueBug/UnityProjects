using UnityEngine;
using System.Collections;

public class _Test00 : MonoBehaviour
{
	public Room room;
	public RoomAsset asset;

	// Use this for initialization
	void Start ()
	{
		var data = new Data.DRoom(new Vector2(1,1));
		data.doors [0] = true;
		data.doors [1] = true;
		data.doors [2] = true;
		data.doors [3] = true;
		room.Reset (asset,data);
		Debug.Log ("REFERS");
		room.Refresh ();
	
	}	
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

