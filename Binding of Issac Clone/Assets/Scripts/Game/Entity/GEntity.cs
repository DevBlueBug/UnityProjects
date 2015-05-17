using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Game.Entity.Delegates;
using Game.Entity.Task;
using Game.Entity.Behavior;

namespace Game.Entity{
	public class GEntity : MonoBehaviour
	{
		public D_FixedUpdate E_FixedUpdate = delegate {};
		public D_Attack E_Attack = delegate {};
		public D_Birth E_Birth = delegate {};
		public D_Kill E_Killed = delegate{};
		public D_HpChange E_HpChange = delegate {	};
		public D_ForceApplied E_ForceApplied = delegate {	};

		public D_OnCollisionEnter E_OnCollisionEnter = delegate{};
		public D_OnTriggerEnter E_OnTriggerEnter = delegate{};
		public D_OnTriggerStay E_OnTriigerStay = delegate {};

		public static int IdCount = 0;

		//components 
		public Rigidbody2D body;
		//public CharacterController myController;
		//public Game.Graphic.GRenderer myRenderer;


		//Status
		public int id; // will be assinged auto
		public bool isDebug = false;
		public float 
			hp,
			velo;
		public bool 
			isAlive = true,
			isHpChange = true,
			isForced = true;

		public GTask taskCurrent = null;
		public List<GBehavior> myBehaviors = new List<GBehavior> ();
		public List<GTask> myTasks = new List<GTask>();

		internal bool 
			isMoved = false,
			isMoveChanged = false;
		internal Vector3 
			moveAmount = Vector3.zero,
			moveAmountOld,
			forceAdded;


		//saved
		Vector3 rot = new Vector3();
		
		public virtual void Awake(){
			HelperIterateInit (myBehaviors);
			HelperIterateInit (myTasks);
			id = IdCount++;

		}
		
		public virtual void Start(){
		}

		public virtual void KFixedUpdate (GRoom room) {
		}
		void UpdateMove(){
			if (isMoveChanged) {
				isMoved = true;
				isMoveChanged = false;
				moveAmountOld = moveAmount;
				//this.body.AddForce(new Vector2(moveAmount.x,moveAmount.y) );
				this.body.velocity = new Vector2(moveAmount.x,moveAmount.y);
				//this.myController.Move (moveAmount * Time.deltaTime);
				moveAmount = Vector3.zero;
			} else {
				if(isMoved){
					this.body.velocity = new Vector2(0,0);
					//this.myController.Move(Vector3.zero);
					moveAmountOld = Vector3.zero;
					isMoved = false;
				}
			}
			//if(isMove&& myController != null)this.myController.Move (body.velocity * Time.deltaTime);

		}
		public virtual void KUpdate (GRoom room)
		{
			//if(isDebug )Debug.Log (gameObject.name +  " " + body.velocity);

			//if(isDebug)Debug.Log("M V
			GBehavior.UpdateTheRest (myBehaviors, this, room);
			UpdateTasks (room);
			UpdateMove ();
			//if(isDebug )Debug.Log (gameObject.name +  " " + myController.velocity);



			//UpdateTasks (room);
			//this.rotation = rot;
			//this.position = new Vector3 (this.position.x,0,this.position.z);
			if (!isAlive) return;			
			if (hp <= 0) {
				Kill();
				return;
			}
		}
		public void UpdateTasks(GRoom room){
			if (taskCurrent == null || !taskCurrent.isAlive) {
				if(myTasks.Count ==0) return;
				taskCurrent = myTasks[0];
				taskCurrent.Init(this);
				myTasks.RemoveAt(0);
				UpdateTasks(room);
				return;
			}
			taskCurrent.KUpdate (this, room);
			if (!taskCurrent.IsAlive) {
				taskCurrent.Kill(this);
			}

		}

		//helpers
		public void HelperIterateInit<T> (List<T> components)where T:IEntityComponent{

			if (components.Count == 0) return;
			components [0].Init ( this);
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
		public void AddBehavior(GBehavior bhv){
			myBehaviors.Add (bhv);
			bhv.transform.parent = this.transform;
			bhv.transform.position = this.transform.position;
			bhv.Init (this);
		}
		public bool AddForce(Vector3 force){
			if (!isForced) return false;
			body.AddForce (force);
			E_ForceApplied (this,force);
			forceAdded = force;
			return true;
		}
		public bool AddHp(float hpChange){
			if (!isHpChange) return false;
			this.hp += hpChange;
			E_HpChange (this,hpChange);
			return true;
		}

		//FunctionCalls
		public void Move(Vector3 velocity){
			isMoveChanged = true;
			moveAmount += velocity;
		}
		public virtual void Attack(int n){
			E_Attack (this,n);

		}
		public virtual void Kill(){
			this.isAlive = false;
			E_Killed (this);
		}
		public virtual void OnCollisionEnter2D(Collision2D c){
			this.E_OnCollisionEnter (this, c);
		}
		public virtual void OnTriggerEnter2D(Collider2D c){
			Debug.Log ("TRIIGER ENTER 2D");
			this.E_OnTriggerEnter (this, c);
		}
		public virtual void OnTriggerStay2D(Collider2D c){
			this.E_OnTriigerStay (this, c);
		}

	}
}