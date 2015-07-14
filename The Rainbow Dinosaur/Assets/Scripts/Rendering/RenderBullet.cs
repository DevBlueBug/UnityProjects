using UnityEngine;
using System.Collections;

public class RenderBullet : RenderEntity
{
	public EffectBulletBounce P_Bounce;
	public float LengthMax;
	public EBullet bullet;
	public GameObject Model;
	public float travelDistance = 5.0f;

	Vector3 posInit, posOld;
	public override void Awake ()
	{
		base.Awake ();
		
		bullet.E_Kill += H_Kill;
		bullet.E_Hit += H_Hit;
		bullet.E_Moved += delegate (EBullet b){
			RenderManager.E_BulletEffect (this);

		};
	}

	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("RNEDER BULLET START");
		posInit = this.transform.position;
		posOld = this.transform.position;
		//this.transform.localRotation = Quaternion.Euler (0,0,
		//  Mathf.Atan2( bullet.direction.y,bullet.direction.x) * (180/3.14f));
	
	}
	void H_Hit(Entity hittedEntity, Vector3 reflective){
		ChromaticEffect.E_NewForce (new PPE_Force (this.transform.position.x,this.transform.position.y,
		                                                bullet.forceApplied*.1f,1,1)) ;
		int count = Random.Range (2, 3);
		var angle = Mathf.Atan2 (reflective.y, reflective.x) * (180.0f/3.14f);
		{
			var bounce = Instantiate(P_Bounce);
			float ratio = Random.Range(1,2.0f);
			bounce.Init(angle,7.0f* ratio,.5f,1.0f* ratio);
			bounce.transform.position = this.transform.position;
		}

		for (int i = 0; i < 3; i++) {
			var bounce = Instantiate(P_Bounce);
			bounce.transform.position = this.transform.position;
			float ratio = Random.Range(1,2.0f);
			bounce.Init( angle + Random.Range(-45,45),5.3f*ratio,.5f,.9f*ratio);
		}
	}
	
	void H_Kill(Entity e){

		
	}
	//Vector3 posBefore;
	// Update is called once per frame
	public override void Update ()
	{
		var disInit = this.transform.position - posInit;
		if (Mathf.Sqrt(disInit.x*disInit.x + disInit.y*disInit.y) > travelDistance) {
			//Debug.Log(posInit);
			//this.bullet.Kill();
			//return;
		}
	}
}

