using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
	public delegate void D_PlayerPositin (Vector3 pos);
	public static D_PlayerPositin E_PlayerPositionChanged = delegate {};
	public static Vector3 PlayerPosInt = Vector3.zero;
	public static Vector3 PlayerPosFloat = Vector3.zero;
	public Player P_Player;

	public RenderManager renderManager;
	public PlayerController controller;
	public Player player;

	bool 
		isAttacked = false;
	Room room;
	List<Room> roomsAdded = new List<Room>();

	Utility.EasyTimer 
		timerAttack = new Utility.EasyTimer (0, .1f),
		timerAttackedInvincibility = new Utility.EasyTimer(0,2.0f);


	void Awake(){
		this.controller = new GameObject ("Controller").AddComponent<PlayerController> ();
		controller.transform.parent = this.transform;
		//EItem.E_PlayerAcquired += H_ItemNew;

	}
	// Update is called once per frame
	public void KUpdate ()
	{
		if (isAttacked) {
			if(timerAttackedInvincibility.Tick(Time.deltaTime)){
				isAttacked = false;
				player.SetInvinsibility(true);
			}

		}

		controller.KUpdate (this, player.entity,room);
		UpdatePlayerPosition ();
	
	}
	void UpdatePlayerPosition(){
		PlayerPosFloat = new Vector3(player.transform.localPosition.x,player.transform.localPosition.y,0);
		PlayerPosInt =new Vector3(Mathf.Round( player.transform.localPosition.x),
		                       Mathf.Round( player.transform.localPosition.y)
		                       ,0);
		E_PlayerPositionChanged (PlayerPosFloat);
	}

	
	public void E_GetNewPlayer(){
		player = Instantiate (P_Player);
		player.entity.E_Attacked += H_Attacked;
		player.entity.weapon = new NItem.NWeapon.GunBasic ()
			.SetTargets (Entity.KType.Enemy, Entity.KType.World);
		player.entity.GetInventory().Add (player.entity, NItem.Item.Get(NItem.Item.KId.Equip_Body_Player_Default));
		player.entity.GetInventory().Add (player.entity, NItem.Item.Get(NItem.Item.KId.Equip_Head_Player_Default));
	}
	public void E_NewRoom(Room room, int direction){
		this.room = room;
		Vector3 pos,
		center = new Vector3 ((int)room.width / 2, (int)(room.height / 2), 0);
		if (direction == -1) {
			pos = center;
		} else {
			pos = room.GetDoorPosition (direction);
			var to = (center - pos).normalized;
			to.Scale( new Vector3(1,1,1));
			pos += to;
		}
		//Debug.Log (direction + " " +  pos);
		for (int i = 0; i < roomsAdded.Count; i++) {
			roomsAdded[i].RemoveEntity(player.entity,false);
		}
		room.AddEntity (player.entity, (int)pos.x, (int)pos.y, false);
		roomsAdded = new List<Room> (){room};
		UpdatePlayerPosition ();
	}
	public void E_Move(Vector3 dir){
		//Debug.Log (	 player.information.GetVelocity ());
		player.entity.Move (dir	);
	}
	public void E_Attack(Entity entitiPlayer,Room room, Vector3 dir){
	
		entitiPlayer.Attack (room, dir);

		/**

		var bullet = Instantiate (P_Bullet);
		room.AddEntity (bullet,
		                player.transform.localPosition.x,
		                player.transform.localPosition.y,false);
		bullet.SetVelocity (dir * player.information.GetVelocity());
		**/
	}
	
	public void E_Bomb(Entity entity, Room room){
		//get bomb/
		//then give the item to bulletManager 
	}

	void H_Attacked(Entity entity, float damage){
		isAttacked = true;
		player.SetInvinsibility (false);
		//Debug.Log ("PLAYER ATTACKED " + entity.hp);

	}

}

