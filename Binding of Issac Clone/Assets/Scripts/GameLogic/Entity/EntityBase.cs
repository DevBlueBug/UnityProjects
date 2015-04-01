using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GameLogic.Entity {
	
	public class EntityBase : MonoBehaviour {
		public Rigidbody body;
		public float 
			hp,
			velo,
			veloMax;
		public List<EntityEventHandler> EntityEventHandlers; 
		Vector3 forceToAdd;
	

		public Vector3 position {
			get { return transform.localPosition;}
			set {transform.localPosition = value;}
		}
		public void AddForce(Vector3 force){
			forceToAdd += force;
		}

		public virtual void Awake(){
			foreach (var e in EntityEventHandlers)
				e.Register (this);
		}

		public virtual void KUpdate(){
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
	}
	
}