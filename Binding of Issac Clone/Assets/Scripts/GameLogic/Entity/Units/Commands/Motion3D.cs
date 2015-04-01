using UnityEngine;
using System.Collections;

namespace GameLogic.Entity.Unit.Action{

	public class Motion3D : MotionBase
	{

		//temporary
		Vector3 forceToAdd;
		// Use this for initialization
		// Update is called once per frame
		void FixedUpdate () {
			myUnit.body.velocity+=  forceToAdd / myUnit.body.mass * Time.fixedTime;
			if (myUnit.body.velocity.sqrMagnitude > myUnit.veloMax*myUnit.veloMax) {
				myUnit.body.velocity = myUnit.body.velocity.normalized * myUnit.veloMax;
				//Debug.Log(myUnit.body.velocity);
			}
			forceToAdd = new Vector3 ();
		}
		public void AddForce(Vector3 force){
			forceToAdd += force;
		}
		
		public override void Dir (Vector3 dir)
		{
			AddForce (dir * myUnit.velo);
		}

		void Start ()
		{
		
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
	}
}
