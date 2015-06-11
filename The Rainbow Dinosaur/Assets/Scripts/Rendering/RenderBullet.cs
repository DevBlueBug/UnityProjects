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
	}
	// Use this for initialization
	void Start ()
	{
		position = this.transform.position;
		this.transform.localRotation = Quaternion.Euler (0,0,
		  Mathf.Atan2( bullet.direction.y,bullet.direction.x) * (180/3.14f));
	
	}
	
	void H_Kill(Entity e){
		ChromaticEffect.E_NewForce (new ChromaticForce (this.transform.position.x,this.transform.position.y,
		                                                bullet.forceApplied*.1f,1,1)) ;
		int count = Random.Range (1, 3);
		for (int i = 0; i < count; i++) {
			var bounce = Instantiate(P_Bounce);
			//bounce.transform.parent = this.transform.parent;
			bounce.transform.position = this.transform.position;
			bounce.angle = this.transform.rotation.eulerAngles.z+180 + Random.Range(-90,90);
			//Debug.Log("ADDING ANGLE " + (this.transform.rotation.z + " " +180) );
			float ratio = Random.Range(1,2.0f);
			bounce.speed = 5.0f* ratio;
			bounce.length = .5f ;
			bounce.travelDistance = 1.0f* ratio;


		}

		
	}
	int count =0;
	// Update is called once per frame
	void Update ()
	{
		var dis = this.transform.position - position;
		//if (count++ > distanceCount) {
		//	position = this.transform.position;
		///	count =0;
		//}

		Model.transform.localScale = new Vector3 ( Mathf.Min(LengthMax, dis.magnitude), Model.transform.localScale.y,Model.transform.localScale.z);//*Time.deltaTime;
	}
}
