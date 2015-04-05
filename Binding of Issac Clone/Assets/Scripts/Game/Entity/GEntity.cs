using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Game.Entity.Delegates;
using Game.Entity.Task;
using Game.Entity.Behavior;

namespace Game.Entity{
	public class GEntity : MonoBehaviour
	{
		public D_Attack E_Attack = delegate {};
		public D_Birth E_Birth = delegate {};
		public D_Kill E_Killed = delegate{};
		public D_OnCollisionEnter E_OnCollisionEnter = delegate{};
		public D_OnSwitchEnter E_OnTriggerEnter = delegate{};

		public static int IdCount = 0;

		public enum MyType{World,Player,Enemy};
		//identity
		public int id; // will be assinged auto
		public MyType myType;
		public Rigidbody body;
		public Collider myCollider;
		public Collider myColliderSwitch;

		//Status
		public bool isAlive = true;
		public float 
			hp,
			velo,veloMax;
		Vector3 forceToAdd;
		public List<GBehavior> myBehaviors = new List<GBehavior> ();
		public List<GTask> myTasks = new List<GTask>();

		//saved
		Vector3 rot = new Vector3();

		
		public virtual void Awake(){
			HelperIterateInit (myBehaviors);
			HelperIterateInit (myTasks);
			id = IdCount++;
			
		}
		
		public virtual void Start(){
		}

		void FixedUpdate () {
			FixedUpdate_VelocityForce ();
		}
		
		public virtual void KUpdate (GRoom room)
		{
			if (!isAlive) return;


			this.rotation = rot;
			HelperIterate (myBehaviors,room);
			HelperIterate (myTasks,room);

			if (hp <= 0) {
				Kill();
				return;
			}
		
		}

		//helpers
		public void HelperIterateInit<T> (List<T> components)where T:IEntityComponent{

			if (components.Count == 0) return;
			components [0].Init ( this);
		}
		public void HelperIterate<T> (List<T> components,GRoom room) where T:IEntityComponent{
		
			if (components.Count == 0) return;
			components [0].KUpdate (this, room);
			if (!components [0].IsAlive)
				components.RemoveAt (0);
		}
		//getSet
		public Vector3 position{
			get{ return this.transform.localPosition;}
			set{ this.transform.localPosition = value;}
		}
		public Vector3 scale{
			get{ return this.transform.localScale;}
			set{ this.transform.localScale = value;}
		}
		public Vector3 rotation{
			get{ return rot;}
			set{this.rot = value; 
				this.transform.localRotation = Quaternion.Euler(rot);}
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
		public void AddBehavior(GBehavior bhv){
			myBehaviors.Add (bhv);
			bhv.transform.parent = this.transform;
			bhv.transform.position = this.transform.position;
			bhv.Init (this);
		}


		//FunctionCalls
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
		public virtual void Attack(int n){
			E_Attack (this,n);

		}
		public virtual void Kill(){
			this.isAlive = false;
			E_Killed (this);
			GameObject.Destroy (this.gameObject);
		}
		public virtual void OnCollisionEnter(Collision c){
			this.E_OnCollisionEnter (this, c);
		}
		public virtual void OnTriggerEnter(Collider c){
			this.E_OnTriggerEnter (this, c);
		}

	}
}