using UnityEngine;
public class PlayerInformation : MonoBehaviour{
	public float velocity;
	public PlayerInformation Reset(EntityMove player){
		velocity = player.velo;
		return this;
	}
	public float GetVelocity(){
		return velocity;
	}

}