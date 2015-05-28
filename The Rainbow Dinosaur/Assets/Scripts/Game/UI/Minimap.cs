using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour
{
	public MinimapIcon P_MinimapIcon;
	public Sprite 
		SPR_RoomActive,
		SPR_RoomSleep,
		SPR_Init,
		SPR_Treasure,
		SPR_Boss;

	public Camera camera;
	public GameBrain brain;

	MinimapIcon[,] icons = null;
	// Use this for initialization
	void Awake ()
	{

		brain.E_NewMap += H_NewMap;
		brain.E_GameStarted_NewRoom += H_NewRoom;
	}
	void H_NewRoom(Room room, int direction){
		camera.transform.localPosition = 
			new Vector3 (room.posX,room.posY,-1);
		for(int i = 0 ; i < icons.GetLength(0);i++)
		for(int j = 0 ; j < icons.GetLength(1);j++){
			var icon =icons[i,j];
			if(icon == null)continue;
			icon.sprite00.sprite = SPR_RoomSleep;
		}
		icons [room.posX, room.posY].sprite00.sprite = SPR_RoomActive;

		//icons [room.posX, room.posY].sprite00.sprite = SPR_RoomActive;
	}
	void H_NewMap(Data.DMap map){
		if (icons != null) {
			for(int i = 0 ; i < icons.GetLength(0);i++)
				for(int j = 0 ; j < icons.GetLength(1);j++){
				if(icons[i,j] != null) 
					Destroy(icons[i,j].gameObject);
			}
		}
		icons = new MinimapIcon[map.width, map.height];
		foreach (var room in map.roomsAvailable) {
			var icon = Instantiate(P_MinimapIcon);
			icons[room.X,room.Y] = icon;
			icon.transform.parent = this.transform;
			icon.transform.localScale = Vector3.one;
			icon.transform.localPosition = new Vector3(room.X,room.Y,0);
			switch(room.meType){
			case Data.DRoom.KType.Start:icon.sprite01.sprite = 		SPR_Init;break;
			case Data.DRoom.KType.Boss:	icon.sprite01.sprite =		SPR_Boss;break;
			case Data.DRoom.KType.Treasure:icon.sprite01.sprite =	SPR_Treasure;break;
			}
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

