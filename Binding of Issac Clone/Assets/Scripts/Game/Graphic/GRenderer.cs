using UnityEngine;
using System.Collections.Generic;

using Utility;
using Game.Entity;
using Game.Graphic.Effect;


namespace Game.Graphic{
	
	public class GRenderer : MonoBehaviour
	{
		public bool isDebug;
		public Game.Entity.GEntity myEntity;
		public Animator animator;
		public List<Rigidbody> bodies;
		public List<MeshRenderer> meshes;
		public List<GRendererEffect> effects;
		public bool isDeletedInstantly = true;

		int countFrame;

		void Awake(){
			myEntity.E_HpChange += Hdr_HpChange;
			if (isDeletedInstantly) {
				myEntity.E_Killed += delegate {
					GameObject.Destroy (this.gameObject);
				};
			} else {
				myEntity.E_Killed += Hdr_Killed;
			}
		}
		void Hdr_HpChange(GEntity entity, float hpChange){
			if (hpChange >= 0)
				return;
			animator.SetTrigger ("trgAttacked");
			//animator.SetBool("isAttacked",true);

		}
		bool isMove;
		void Update(){
			if (animator == null) return;
		//	if(isDebug && !myEntity.isMoved)Debug.Log ("GRENDERER " + gameObject.name + " "+ myEntity.isMoved);
			animator.SetBool ("isMoving", myEntity.isMoved	);
			
			if (isMove && !myEntity.isMoved) {
				isMove = false;
				animator.speed = 1;
			}
			if (myEntity.isMoved) {
				isMove = true;
				animator.speed = .8f + myEntity.velo*.2f;
			}

		}
		void Hdr_Killed(GEntity Entity){
			foreach (var body in bodies) {
				body.transform.parent = this.transform.parent;
				body.isKinematic = false;
				var dir =( 
				          //body.transform.position - this.transform.position 

				          new Vector3(Random.Range(-1.0f,1.0f),0,Random.Range(-1.0f,1.0f) )
				          ).normalized;
				//body.AddExplosionForce(.5f* (1/Time.deltaTime),
				 //                      this.transform.position ,1);//(Quaternion.Euler(dir*1.0f * (1/Time.deltaTime)) );
				//body.AddForce(dir*.5f * (1/Time.deltaTime));
				Debug.Log(myEntity.body.velocity);
				body.AddForce(myEntity.body.velocity * (1/Time.deltaTime));
				if(myEntity.body.velocity.sqrMagnitude < .1f){
					body.AddForce(myEntity.forceAdded);
				}
			}
			GameObject.Destroy (this.gameObject);
		}
	}

}
