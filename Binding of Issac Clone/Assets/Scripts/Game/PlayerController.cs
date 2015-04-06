using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Game.Entity;

namespace Game{

	public class PlayerController : MonoBehaviour
	{
		public delegate void D_InputUpdate();
		public class KeyboardInputType
		{
			public KeyCode key;
			public Vector2 dir; 
		}
		static Dictionary<KeyCode,Vector3> inputKeyboardMove = new Dictionary<KeyCode, Vector3>()
		{
			{KeyCode.W, new Vector3(0,0,1)},
			{KeyCode.D, new Vector3(1,0,0)},
			{KeyCode.S, new Vector3(0,0,-1)},
			{KeyCode.A, new Vector3(-1,0,0) }
		};
		
		static Dictionary<KeyCode,int> inputKeyboardAttack = new Dictionary<KeyCode, int>()
		{
			{KeyCode.I, 0},
			{KeyCode.L, 1},
			{KeyCode.K, 2},
			{KeyCode.J, 3 }
		};

		public GEntity myEntity;
		D_InputUpdate E_InputUpdate = delegate{};

		void Awake(){
			bool isDesktop = true;
			if (isDesktop)
				E_InputUpdate = this.UpdateKeyboard;

		}

		// Use this for initialization
		void Start ()
		{
		
		}
		public Vector3 myVelocity;
		// Update is called once per frame
		public virtual void KUpdate ()
		{
			myVelocity = myEntity.body.velocity;
			E_InputUpdate ();
		
		}
		public virtual void KFixedUpdate(){
			UpdateKeyBoard_Movement ();
		}
		
		void UpdateKeyboard(){
			UpdateKeyBoard_Attack ();
		}
		void UpdateKeyBoard_Movement(){
			//move
			Vector3 direction = Vector2.zero;
			foreach (var k in inputKeyboardMove) {
				if(Input.GetKey(k.Key) ) direction += k.Value;
			}
			if (direction.sqrMagnitude == 0)
				return;
			direction.Normalize ();
			myEntity.body.AddForce (direction*myEntity.velo );
			//myEntity.body.AddForce (speedNeeded * myEntity.body.mass);
			//myEntity.AddForce (myEntity.velo * direction);
		}
		
		void UpdateKeyBoard_Attack(){
			foreach (var k in inputKeyboardAttack) {
				if(Input.GetKeyDown(k.Key) ) {
					myEntity.Attack(k.Value);
					return;
				}
			}
		}

	}
}
