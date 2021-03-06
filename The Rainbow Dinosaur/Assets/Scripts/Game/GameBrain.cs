using UnityEngine;
using System.Collections;

public class GameBrain : MonoBehaviour
{
	public delegate void D_NewMap(Data.DMap map);
	public delegate void D_GameStarted_NewRoom(Room room, int n);
	public delegate void D_RoomLoaded (Room room);
	
	public D_NewMap E_NewMap = delegate {	};
	public D_GameStarted_NewRoom E_GameStarted_NewRoom = delegate { };

	public EItemManager P_ItemManager;
	public Room P_Room;
	public RoomAsset P_RoomAsset;

	public ChromaticEffect effect;
	public PlayerManager pManager;
	public RenderManager rManager;

	internal float difficulty = 0;

	internal Data.DMap map;
	internal Data.Organizer organizer;
	internal Room[,] rooms;
	internal Room room;


	void Awake(){
		Room.P_ItemManager = P_ItemManager;
		var bulletManager = new GameObject ("BulletManager").AddComponent<BulletManager>();
		bulletManager.transform.parent = this.transform;
		organizer = new Data.Organizer ();
		E_GameStarted_NewRoom += H_NewRoomInit;
	}
	// Use this for initialization
	void Start ()
	{
		//var e = new  Data.Organizer ().GetId (true, true, false, true);

	}
	
	// Update is called once per frame
	void Update ()
	{
		room.KUpdate ();
		pManager.KUpdate ();
		if (Input.GetKeyDown (KeyCode.E)) {
			Debug.Log("ITEM SPAWN");
			pManager.player.entity.GetInventory().Add(pManager.player.entity,NItem.Item.Get(NItem.Item.KId.Equip_Head_Kaonash));
			//room.AddEntity(P_ItemManager.GetRandom(),
			 //              Random.Range(1,13),Random.Range(1,7),false);
		}
		//Debug.Log (pManager.player.entity.GetInventory ().items.Count);
		rManager.KUpdate ();
	}
	void OnGUI(){
	}
	
	public void Init(){
		pManager.E_GetNewPlayer ();
		map = new Data.DMapGenerator ().GenerateMap (10, 10, 5);
		E_NewMap (map);


		if (rooms != null) {
			for(int i = 0 ; i < map.width;i++)for(int j = 0 ; j< map.height;j++){
				if(rooms[i,j] != null) rooms[i,j].Kill();
			}
		}
		rooms = new Room[map.width, map.height];
		//room = new RoomConverter ().ToRooms (out rooms, P_Room, P_RoomAsset, organizer, H_NewRoomCreated, map);
		//ToRooms (map);
		E_GameStarted_NewRoom (
				new RoomConverter ().ToRooms (out rooms, P_Room, P_RoomAsset, organizer, H_NewRoomCreated, map),-1);

	}
	public void H_NewRoomCreated(Room room){
		room.Off ();
		room.E_NextRoom += delegate (int n) {
			Debug.Log("NEW ROOM");
			var pos = Utility.EasyUnity.dirFour3[n];
			E_GameStarted_NewRoom(rooms[room.posX+(int)pos.x,room.posY+(int)pos.y],(n+2)%4);
		};
		//room.E_GetItemDrop += P_ItemManager.GetDrop;
		//room.E_GetItemStationary += P_ItemManager.GetStationary;

	}
	public IEnumerator H_EnterDoor(Room roomNew, int direction){
		yield return new WaitForEndOfFrame();
		H_NewRoomInit (roomNew, direction);
	}
	public void H_NewRoomInit(Room roomInit, int direction){
		//Debug.Log (roomInit);
		roomInit.On ();
		pManager.E_NewRoom (roomInit, direction);
		if(this.room != null) this.room.Off ();
		this.room = roomInit;
	}

}

