using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
	public Entity player;
	public PlayerController controller;
	List<Room> roomsAdded = new List<Room>();
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	public void KUpdate ()
	{
		if (player.isAlive) controller.KUpdate ();
	
	}

	public void E_NewRoom(Room room, int direction){

		Vector3 pos = (direction == -1) ? 
			new Vector3 ((int) room.width / 2, (int) (room.height / 2), 0) :
				room.GetDoorPosition (direction);
		//Debug.Log (direction + " " +  pos);
		for (int i = 0; i < roomsAdded.Count; i++) {
			roomsAdded[i].RemoveEntity(player);
		}
		room.AddEntity (player, (int)pos.x, (int)pos.y, false);
		roomsAdded = new List<Room> (){room};

	}
}

