using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Game.Entity.Delegates;
using Game.Entity.Task;
using Game.Entity.Behavior;

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
		public List<GBehavior> myBehaviors = new List<GBehavior> ();
		public List<GTask> myTasks = new List<GTask>();

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
		public GEntity SetPosition(Vector3 value){
			this.position = value;
			return this;
		}
		public GEntity SetScale(Vector3 value){
			this.scale = value;
			return this;
		}
		public GEntity SetRotation(Vector3 value){
			this.rotation = value;
			return this;
		}
		
		public void AddTask(GTask task){
			this.myTasks.Add (task);
		}
		public void AddForce(Vector3 force){
			forceToAdd += force;
		}

		void FixedUpdate () {
			FixedUpdate_VelocityForce ();
		}
		public virtual void KUpdate (GRoom room)
		{
			{
				if (myBehaviors.Count == 0)
					return;
				myBehaviors [0].KUpdate (this, room);
				if (!myBehaviors [0].isAlive)
					myBehaviors.RemoveAt (0);
			}
			{
				if (myTasks.Count == 0)
					return;
				myTasks [0].KUpdate (this, room);
				if (!myTasks [0].isAlive)
					myTasks.RemoveAt (0);
			}
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