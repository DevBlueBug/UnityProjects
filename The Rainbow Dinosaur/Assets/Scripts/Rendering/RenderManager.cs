using UnityEngine;
using System.Collections;

public class RenderManager : MonoBehaviour
{
	public delegate void D_BulletEffect(RenderBullet bullet);
	public static D_BulletEffect E_BulletEffect = delegate {};
	public RenderRoomConstant P_RoomConstant;
	
	public PPE_Engine engineChromatic;
	public Rorschach engineRorschach;
	public GameBrain game;
	public PlayerManager playerManager;

	public int 
		layerWall,
		layerShadow,
		orderWall,
		orderWallShadow;

	//RenderWalls renderWalls;
	RenderRoomConstant roomConstant;
	void Awake(){
		RenderRoom.wallLayer = layerWall;
		RenderRoom.wallOrder = orderWall;
		RenderRoom.wallShadowLayer = layerShadow;
		RenderRoom.wallShadowOrder = orderWallShadow;
		//roomConstant = Instantiate (P_RoomConstant);
		//roomConstant.transform.parent = this.transform;
		//roomConstant.transform.parent = this.transform;


		NItem.NWeapon.Weapon.E_Backswing += H_Backswing;
		RenderManager.E_BulletEffect = H_BulletEffect;
		game.E_GameStarted_NewRoom += H_NewRoom;

	}
	void H_NewRoom(Room room,int n){
		Debug.Log ("RENDER MANAGER :" + room);
		engineRorschach.E_NewSeed (room.posX + room.posY * 100);
	}
	void H_BulletEffect(RenderBullet bullet){
		float angle = 
			Mathf.Atan2 (bullet.bullet.direction.y, bullet.bullet.direction.x);
		var dir = new Vector3 (Mathf.Cos(angle),Mathf.Sin(angle),0 );
		return;
		PPE_Engine.E_Effect00 (bullet.transform.position);	

		PPE_Engine.E_NewForce (new PPE_Force(
			bullet.transform.position.x +bullet.bullet.direction.x*.01f,
			bullet.transform.position.y +bullet.bullet.direction.y *.01f,
			-.1f*bullet.bullet.forceApplied,
			3));

	}
	void H_Backswing(Vector3 position, Vector3 direction, float force){
		force *= .5f;
		ChromaticEffect.E_NewForce (new PPE_Force (position.x - direction.x*.2f,position.y - direction.y*.2f,force,.5f));

	}
	public void KUpdate(){
		engineChromatic.KUpdate ();
		engineRorschach.KUpdate ();
	}


}

