using UnityEngine;
using System.Collections;

public class GameBrain : MonoBehaviour
{
	public Room P_Room;
	public RoomAsset P_RoomAsset;

	internal float difficulty = 0;

	internal Data.DMap map;
	internal Room room;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void Init(){
		if (room != null) Destroy (room.gameObject);
		room = Instantiate (P_Room);
		//room.Reset (P_RoomAsset);
		var data = DataLoader.Load (0);
		room.Reset (P_RoomAsset,data);
	}

}
