using UnityEngine;
using System.Collections;

public class Ani_Player : MonoBehaviour {
	public static float veloMin = .1f;

	public Rigidbody2D body;
	public Animator ani,aniEye;
	bool 
		isMoving = false;
	int dirFacing = 0; // 0 left 1 right

	float timeElapsed = 0;
	string[] eyeDirection = new string[]{"up","right","down","left","idl"};
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var isMovingNow = body.velocity.sqrMagnitude > .3f;
		var dirFacingNew = 0;
		if (body.velocity.x > veloMin) {
			dirFacingNew = 1;
		} else if (body.velocity.x < -veloMin) {
			dirFacingNew = -1;
		}
		if (dirFacing != dirFacingNew) {
			dirFacing = dirFacingNew;
			Debug.Log(dirFacing);
			if(dirFacing == -1){
				this.transform.localRotation = Quaternion.Euler(0,0,0);
			}
			else if (dirFacing == 1) {
				this.transform.localRotation = Quaternion.Euler(0,180,0);
			}
		}

		if (isMoving != isMovingNow) {
			isMoving = isMovingNow;
			ani.SetBool("isMoving",isMovingNow);
		}
	
		
		if (Input.GetKeyDown (KeyCode.A)) {

			ani.SetTrigger("onAttack");
		}
		timeElapsed += Time.deltaTime;
		if (timeElapsed > 3.5f) {
			timeElapsed = 0;
			if(Random.Range(0,3)==0){
				aniEye.SetTrigger(eyeDirection[Random.Range(0,4)]);
			}
			else{
				aniEye.SetTrigger("idl");
			}
		}
	
	}
}
