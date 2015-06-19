using UnityEngine;
using System.Collections;

public class RenderManager : MonoBehaviour
{
	public delegate void D_BulletEffect(RenderBullet bullet);
	public static D_BulletEffect E_BulletEffect = delegate {};
	public ChromaticEngine engineChromatic;
	public GameBrain game;
	public PlayerManager playerManager;

	RenderWalls renderWalls;
	void Awake(){
		renderWalls = new GameObject ("RenderWalls")
			.AddComponent<RenderWalls> ();

		renderWalls.transform.parent = this.transform;

		NItem.NWeapon.Weapon.E_Backswing += H_Backswing;
		RenderManager.E_BulletEffect = H_BulletEffect;

	}
	void H_BulletEffect(RenderBullet bullet){
		float angle = 
			Mathf.Atan2 (bullet.bullet.direction.y, bullet.bullet.direction.x);
		var dir = new Vector3 (Mathf.Cos(angle),Mathf.Sin(angle),0 );
		ChromaticEngine.E_Effect00 (bullet.transform.position);	

		ChromaticEngine.E_NewForce (new ChromaticForce(
			bullet.transform.position.x +bullet.bullet.direction.x*.01f,
			bullet.transform.position.y+bullet.bullet.direction.y *.01f,
			.1f*bullet.bullet.forceApplied,
			3));

	}
	void H_Backswing(Vector3 position, Vector3 direction, float force){
		force *= .1f;
		ChromaticEffect.E_NewForce (new ChromaticForce (position.x - direction.x*.2f,position.y - direction.y*.2f,force,.5f));

	}
	public void KUpdate(){
		engineChromatic.KUpdate ();
	}


}

