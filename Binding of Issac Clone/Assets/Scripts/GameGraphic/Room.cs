using UnityEngine;
using System.Collections;
using GameLogic;

public class Room : RendererObject
{
	public DataRoom myRoom;
	public RendererObject RBoundry;
	public RendererFloor RFloor;
	public RendererDoor RDoor;

	RendererFloor myFloor;
	RendererObject[,] mapObjects;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	void AddObject(RendererObject PREFAB ,int x, int y, int dirFacing ){
		if (mapObjects [x, y] != null)
			mapObjects[x,y].Destroy ();
		var obj = Instantiate (PREFAB);
		mapObjects [x, y] = obj;

		obj.transform.parent = this.transform;
		obj.Position = new Vector3 (x, 0, y);
		obj.transform.localRotation = Quaternion.Euler (0, 180 +90* dirFacing, 0 );

	}
	public void Display(DataRoom room){
		this.myRoom = room;
		mapObjects = new RendererObject[room.width, room.height];
		//RendererDoor[] doors = new RendererDoor[]{Instantiate (rDoor),Instantiate (rDoor),Instantiate (rDoor),Instantiate (rDoor)};
		//Debug.Log (room);
		myFloor = Instantiate (RFloor);
		myFloor.transform.parent = this.transform;
		myFloor.Display (room.width, room.height);
		AddBoundries (room.width, room.height);
		AddDoors (room.width, room.height);
	}
	void AddBoundries(int w, int h){
		for (int i = 0; i < w; i++) {
			AddObject (RBoundry, i, h-1,0);
			AddObject (RBoundry, i, 0,0);
		}
		for (int i = 1; i < h; i++) {
			AddObject (RBoundry, 0, i,0);
			AddObject (RBoundry, w-1, i,0);
		}
	}
	void AddDoors(int w, int h){
		if (myRoom.doors [0] != null)
			AddObject (RDoor, w / 2, h - 1 , 2);
		if (myRoom.doors [1] != null)
			AddObject (RDoor, w -1, h /2, 3);
		if (myRoom.doors [2] != null)
			AddObject (RDoor, w /2, 0,0 );
		if (myRoom.doors [3] != null)
			AddObject (RDoor, 0, h /2,1 );
	}
}

