//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
public class RoomConverter
{
	public delegate void D_NewRoom(Room Room);

	public RoomConverter (){
	}
	//return the initial room
	public Room ToRooms(out Room[,] rooms,
	                    Room p_room,RoomAsset p_roomAsset, 
	                    Data.Organizer organizer, D_NewRoom E_NewRoom,

	                    Data.DMap map){
		rooms = new Room[map.width, map.height];
		foreach (var r in map.roomsAvailable) {
			//bool resetWithData = (r.X != map.roomInit.X || r.Y != map.roomInit.Y);
			AddRoom(ref rooms, organizer,p_roomAsset,
			        UnityEngine.GameObject.Instantiate(p_room),
			        r.meType, r.X,r.Y, r.doors);
			E_NewRoom(rooms[r.X,r.Y]);
		}
		return rooms [map.roomInit.X, map.roomInit.Y];
	}
	void AddRoom(ref Room[,] rooms, Data.Organizer organizer, RoomAsset roomAsset,
	             Room room,
	             Data.DRoom.KType type, int x, int y, bool[] doors){
		rooms [x, y] = room;
		room.type = type;
		room.SetPosition(x,y);
		if (room.type == Data.DRoom.KType.Normal) {
			var dataBoard = organizer.GetBoard(doors[0],doors[1],doors[2],doors[3]);
			room.Reset(roomAsset, dataBoard);
		} else if(room.type == Data.DRoom.KType.Start) {
			room.Reset(roomAsset,new Data.Board(doors[0],doors[1],doors[2],doors[3]));
			room.AddEntity(roomAsset.GetBoss(0),room.width/2,room.height/2,false);
		}
		else if(room.type== Data.DRoom.KType.Boss){
			room.Reset(roomAsset,new Data.Board(doors[0],doors[1],doors[2],doors[3]));

		}


		//move this to brain
		/**
		room.gameObject.SetActive(false);
		room.E_NextRoom += delegate (int n) {
			Debug.Log("NEW ROOM");
			var pos = Utility.EasyUnity.dirFour3[n];
			E_GameStarted_NewRoom(rooms[room.posX+(int)pos.x,room.posY+(int)pos.y],(n+2)%4);
		};
		**/

	}
	/**
	public IEnumerator H_EnterDoor(Room roomNew, int direction){
		yield return new WaitForEndOfFrame();
		H_NewRoom (roomNew, direction);
	}
	public void H_NewRoom(Room room, int direction){
		Debug.Log (room);
		this.room.gameObject.SetActive (false);
		room.On ();
		this.room = room;
		pManager.E_NewRoom (room, direction);
		room.gameObject.SetActive (true);
		//E_GameStarted_NewRoom (room, direction);
	}
	**/

}
