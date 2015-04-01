using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player: MonoBehaviour {
	[System.Serializable]
	public class KeyboardInputType
	{
		public KeyCode key;
		public Vector2 dir; 
	}
	public GameLogic.Entity.Unit.UnitBase myUnit;
	public List<KeyboardInputType> keysMove;
	public List<KeyboardInputType> keysAttack;

	//temporary holders 
	Vector2 attackDir;
	Vector2 moveDir;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		UpdateMove ();
		UpdateAttack ();


	
	}
	void UpdateMove(){
		moveDir = new Vector2 ();
		foreach (var d in keysMove)
			if (Input.GetKey (d.key))
				moveDir += d.dir;
		//myUnit.MoveDir (new Vector3 (moveDir.x, 0, moveDir.y));
		//myUnit.AddForce (new Vector3(moveDir.x,0,moveDir.y) * 30.0f);
	}
	void UpdateAttack(){
		int inc = 1;
		foreach (var d in keysAttack) {
			if (Input.GetKeyDown (d.key)) {
				myUnit.transform.rotation = Quaternion.Euler (new Vector3(0, 90,0) * inc);
				//myUnit.AttackDir(new Vector3 (d.dir.x, 0, d.dir.y));
				return;
			}
			inc++;
		}

	}
}
