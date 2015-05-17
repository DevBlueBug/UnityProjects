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
			{KeyCode.W, new Vector3(0,1,0)},
			{KeyCode.D, new Vector3(1,0,0)},
			{KeyCode.S, new Vector3(0,-1,0)},
			{KeyCode.A, new Vector3(-1,0,0) }
		};
		
		static Dictionary<KeyCode,int> inputKeyboardAttack = new Dictionary<KeyCode, int>()
		{
			{KeyCode.I, 0},
			{KeyCode.L, 1},
			{KeyCode.K, 2},
			{KeyCode.J, 3 }
		};
		public CharacterController chaController;
		public GEntity myEntity;
		D_InputUpdate E_InputUpdate = delegate{};

		public Game.Entity.Task.GTaskMove taskMove = 
			new Game.Entity.Task.GTaskMove(new Vector3());

		void Awake(){
			bool isDesktop = true;
			if (isDesktop)
				E_InputUpdate = this.UpdateKeyboard;

		}

		// Use this for initialization
		void Start ()
		{
		
		}
		public void SetEntity(GEntity entity){
			myEntity = entity;
		}
		public Vector3 myVelocity;
		// Update is called once per frame
		public virtual void KUpdate ()
		{
			myVelocity = myEntity.body.velocity;
			E_InputUpdate ();
			UpdateKeyBoard_Movement ();
		
		}
		public virtual void KFixedUpdate(){

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


			
			if (direction.sqrMagnitude == 0) return;

			direction.Normalize ();
			
			var chaController = myEntity.GetComponent<CharacterController> ();
			myEntity.Move(direction * myEntity.velo);
			return;
			//myEntity.body.AddForce (direction*myEntity.velo );
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
