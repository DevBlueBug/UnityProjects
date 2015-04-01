using UnityEngine;
using System.Collections;

using Game.Entity.Delegates;

namespace Game.Entity{
	public class GEntity : MonoBehaviour
	{
		public D_TriggerEnter E_TriggerEnter = delegate{};
		public D_TriggerExit E_TriggerExit = delegate{};
		public Rigidbody body;
		public float 
			hp,
			velo,veloMax;
		Vector3 forceToAdd;

		public Vector3 position{
			get{ return this.transform.localPosition;}
			set{ this.transform.localPosition = value;}
		}
		public Vector3 scale{
			get{ return this.transform.localScale;}
			set{ this.transform.localScale = value;}
		}
		public Vector3 rotation{
			get{ return this.transform.localRotation.eulerAngles;}
			set{ this.transform.localRotation = Quaternion.Euler(value);}
		}
		// Use this for initialization
		void Start ()
		{
			
		}
		
		// Update is called once per frame
		public virtual void KUpdate ()
		{
			
		}
		public void AddForce(Vector3 force){
			forceToAdd += force;
		}

		void FixedUpdate () {
			FixedUpdate_VelocityForce ();
		}
		
		void FixedUpdate_VelocityForce(){
			if (body.velocity.sqrMagnitude + forceToAdd.sqrMagnitude == 0)
				return;
			body.velocity+=  forceToAdd / body.mass * Time.fixedTime;
			if (body.velocity.sqrMagnitude > veloMax*veloMax) {
				body.velocity = body.velocity.normalized * veloMax;
				//Debug.Log(myUnit.body.velocity);
			}
			forceToAdd = new Vector3 ();
		}
		public virtual void Kill(){
			GameObject.Destroy (this.gameObject);
		}
		void OnTriggerEnter(Collider c){
			E_TriggerEnter (c);
		}
		void OnTriggerExit(Collider c){
			E_TriggerExit (c);
		}

	}
}