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
		public D_Attack E_Attack = delegate {};
		public Rigidbody body;
		public Vector3 rot = new Vector3();
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
			task.Init (this);
			this.myTasks.Add (task);
		}
		public void AddForce(Vector3 force){
			forceToAdd += force;
		}
		void Start(){
			HelperIterateInit (myBehaviors);
			HelperIterateInit (myTasks);

		}
		void FixedUpdate () {
			FixedUpdate_VelocityForce ();
		}

		public void HelperIterateInit<T> (List<T> components) 
		where T:IEntityComponent{
			if (components.Count == 0) return;
			components [0].Init ( this);
		}
		public void HelperIterate<T> (List<T> components,GRoom room) 
		where T:IEntityComponent{
			if (components.Count == 0) return;
			components [0].KUpdate (this, room);
			if (!components [0].IsAlive)
				components.RemoveAt (0);
		}
		public virtual void KUpdate (GRoom room)
		{
			this.rotation = rot;
			HelperIterate (myBehaviors,room);
			HelperIterate (myTasks,room);
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
		public virtual void Attack(Vector3 direction){
			E_Attack (direction);

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