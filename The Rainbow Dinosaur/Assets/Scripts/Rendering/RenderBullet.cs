using UnityEngine;
using System.Collections;

public class RenderBullet : RenderEntity
{
	public EffectBulletBounce P_Bounce;
	public float LengthMax;
	public EBullet bullet;
	public GameObject Model;

	Vector3 position;
	public override void Awake ()
	{
		base.Awake ();
		
		bullet.E_Kill += H_Kill;
		bullet.E_Hit += H_Hit;
		bullet.E_Moved += delegate (EBullet b){
			RenderManager.E_BulletEffect (this);

		};
	}
	public override void AwakeAddChromaticObject ()
	{
		chroObject = renderSprite.gameObject.AddComponent<ChromaticBullet>()
			.Init(renderSprite,isChromaticSelfUpdate,this);
	}

	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("RNEDER BULLET START");
		position = this.transform.position;
		this.transform.localRotation = Quaternion.Euler (0,0,
		  Mathf.Atan2( bullet.direction.y,bullet.direction.x) * (180/3.14f));
	
	}
	void H_Hit(Entity hittedEntity, Vector3 reflective){
		ChromaticEffect.E_NewForce (new ChromaticForce (this.transform.position.x,this.transform.position.y,
		                                                bullet.forceApplied*.1f,1,1)) ;
		int count = Random.Range (2, 3);
		var angle = Mathf.Atan2 (reflective.y, reflective.x) * (180.0f/3.14f) ;
		{
			var bounce = Instantiate(P_Bounce);
			//bounce.transform.parent = this.transform.parent;
			bounce.transform.position = this.transform.position;
			bounce.angle = angle;
			//Debug.Log("ADDING ANGLE " + (this.transform.rotation.z + " " +180) );
			float ratio = Random.Range(1,2.0f);
			bounce.speed = 7.0f* ratio;
			bounce.length = .5f ;
			bounce.travelDistance = 1.0f* ratio;
		}

		for (int i = 0; i < count; i++) {
			var bounce = Instantiate(P_Bounce);
			//bounce.transform.parent = this.transform.parent;
			bounce.transform.position = this.transform.position;
			bounce.angle = angle + Random.Range(-45,45);
			//Debug.Log("ADDING ANGLE " + (this.transform.rotation.z + " " +180) );
			float ratio = Random.Range(1,2.0f);
			bounce.speed = 5.3f* ratio;
			bounce.length = .5f ;
			bounce.travelDistance = .9f* ratio;
			
			
		}
	}
	
	void H_Kill(Entity e){

		
	}
	// Update is called once per frame
	public override void Update ()
	{
		var dis = this.transform.position - position;
		//RenderManager.E_BulletEffect (this);

		Model.transform.localScale = new Vector3 ( Mathf.Min(LengthMax, dis.magnitude), Model.transform.localScale.y,Model.transform.localScale.z);//*Time.deltaTime;
	}
}

