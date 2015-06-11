using UnityEngine;
using System.Collections;

public class EffectBulletBounce : MonoBehaviour
{
	Vector3 position;
	public Rigidbody2D body;
	public float 
		angle,speed,length,travelDistance;

	// Use this for initialization
	void Start ()
	{
		this.transform.localScale = new Vector3 (length,this.transform.localScale.y,this.transform.localScale.z);
		this.transform.localRotation = Quaternion.Euler (0, 0, angle);
		//Debug.Log ("rotation  " + angle);
		position = this.transform.position;
		float r = angle * (3.14f/180);
		body.velocity = new Vector2 (Mathf.Cos(r) ,Mathf.Sin(r))*speed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//float r = angle * (3.14f/180);
		//body.velocity = new Vector2 (Mathf.Cos(r) ,Mathf.Sin(r))*speed;

		var dis = this.transform.position - position;
		float ratio = (dis.magnitude / travelDistance),
		ratioSin = Mathf.Sin(ratio*3.14f);
		this.transform.localScale = new Vector3 (ratioSin* length,this.transform.localScale.y,this.transform.localScale.z);

		//Mathf.Sin ( * 3.14f);

		//Debug.Log (ratio + " " + ratioSin);
		if (ratio >= 1) {
			//Debug.Log("DESTROY");
			Destroy(this.gameObject);
		}
	
	}
}

