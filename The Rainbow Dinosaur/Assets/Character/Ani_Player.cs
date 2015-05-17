using UnityEngine;
using System.Collections;

public class Ani_Player : MonoBehaviour {

	public Animator ani,aniEye;
	bool isMoving = false;
	float timeElapsed = 0;
	string[] eyeDirection = new string[]{"up","right","down","left","idl"};
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			isMoving = !isMoving;
			ani.SetBool("isMoving",isMoving);
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
