using UnityEngine;
using System.Collections;

namespace Game.Entity{
	
	public class GHitbox : GEntity
	{
		
		public GameObject 
			HitboxWorld,
			HitboxPlayer,
			HitboxEnemy;
		bool isHitTarget;
		
		
		public bool IsHitWorld {
			get {
				return HitboxWorld.gameObject.activeSelf;
			}
			set {
				HitboxWorld.gameObject.SetActive( value );
			}
		}
		public bool IsHitPlayer {
			get {
				return HitboxPlayer.gameObject.activeSelf;
			}
			set {
				HitboxPlayer.gameObject.SetActive( value );
			}
		}
		public bool IsHitEnemy {
			get {
				return HitboxEnemy.gameObject.activeSelf;
			}
			set {
				HitboxEnemy.gameObject.SetActive( value );
			}
		}

		// Use this for initialization
		override public void Start ()
		{
			base.Start ();
		}
		/**
		public virtual void OnCollisionEnter(Collision c){
			hp--;
			//but do not call for explosion
		}
		public virtual void OnTriggerEnter(Collider collider){
			hp--;
			isHitTarget = true;
			Debug.Log ("OnTriigerEnter");
			//call for an explosion
		}
		**/
	}

}
