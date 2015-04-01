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
		static Dictionary<KeyCode,Vector3> inputKeyboard = new Dictionary<KeyCode, Vector3>()
		{
			{KeyCode.W, new Vector3(0,0,1)},
			{KeyCode.D, new Vector3(1,0,0)},
			{KeyCode.S, new Vector3(0,0,-1)},
			{KeyCode.A, new Vector3(-1,0,0) }
		};

		public GEntity myEntity;
		D_InputUpdate E_InputUpdate = delegate{};

		void Awake(){
			bool isDesktop = true;
			if (isDesktop)
				E_InputUpdate = this.UpdateKeyboard;
			else
				;
		}

		// Use this for initialization
		void Start ()
		{
		
		}
		// Update is called once per frame
		public virtual void KUpdate ()
		{
			E_InputUpdate ();
		
		}
		
		void UpdateKeyboard(){
			UpdateKeyBoard_Movement ();
		}
		void UpdateKeyBoard_Movement(){
			//move
			Vector3 direction = Vector2.zero;
			foreach (var k in inputKeyboard) {
				if(Input.GetKey(k.Key) ) direction += k.Value;
			}
			if (direction.sqrMagnitude == 0)
				return;
			direction.Normalize ();
			myEntity.AddForce (myEntity.velo * direction);
		}

	}
}
