using UnityEngine;
using System.Collections;

public class GRepulsiveForce : MonoBehaviour
{
	public float radius,force;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	void OnTriggerStay(Collider c){
		Repulsive (c);
		//Debug.Log ("Hi ColliderEnter");
	}
	void OnTriggerEnter(Collider c){
		Repulsive (c);
	}
	void Repulsive(Collider c){
		var distance = c.transform.position - this.transform.position;
		var mag = distance.magnitude;
		var ratio = (radius-mag) / radius;
		//Debug.Log ("pushing "   +distance.normalized + " "+(ratio*ratio*force));
		c.attachedRigidbody.AddForce ((distance.normalized )*ratio*ratio*force);
	}
}

