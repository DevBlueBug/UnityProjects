using UnityEngine;
using System.Collections;

public class GameBrain : MonoBehaviour
{
	public Room P_Room;
	public RoomAsset P_RoomAsset;
	RoomAsset roomAsset;

	public PlayerManager pManager;

	internal float difficulty = 0;

	internal Data.DMap map;
	internal Room room;


	// Use this for initialization
	void Start ()
	{
		roomAsset = Instantiate (P_RoomAsset);
		roomAsset.transform.parent = this.transform;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		room.KUpdate ();
		pManager.KUpdate ();
	
	}
	
	public void Init(){
		if (room != null) Destroy (room.gameObject);
		room = Instantiate (P_Room);
		//room.Reset (P_RoomAsset);
		var data = DataLoader.Load (0);
		room.Reset (roomAsset,data);
		E_NewRoom (room);
	}
	public void E_NewRoom(Room room){
		pManager.E_NewRoom (room, -1);
	}

}

