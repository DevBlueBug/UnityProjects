using UnityEngine;
using System.Collections;

public class AniEntity : MonoBehaviour
{
	public EntityMove entity;
	public Animator animator;
	Vector3[] posBefore = new Vector3[2]{Vector3.zero,Vector3.zero};
	int posBeforeCount = 0;

	int aniState = 0; //0 up 1 right 2 down 3 left

	void Awake(){

	}
	void Start(){
	}
	void Update(){
		UpdateAnimation ();



	}
	void UpdateAnimation(){
		var magnitude =  
			Mathf.Min(1, entity.velocity.sqrMagnitude*100000) * 1.0f + 
			(entity.transform.localPosition - posBefore[(posBeforeCount+1) & 1]).magnitude	 * 15	;
		//Debug.Log (entity.velocity + " " + posBefore[(posBeforeCount+1) & 1] + " " + magnitude	);
		posBefore [++posBeforeCount & 1] = entity.transform.localPosition;

		if (magnitude <= .1f) {
			SetAniState (-1);
			return;
		}
		animator.speed = magnitude;
		if (Mathf.Abs (entity.velocity.x) > Mathf.Abs (entity.velocity.y)) {
			if (entity.velocity.x > 0)
				SetAniState (1);
			else if (entity.velocity.x < 0) 
				SetAniState (3);
		} else {
			if (entity.velocity.y > 0)
				SetAniState (0);
			else if (entity.velocity.y < 0) 
				SetAniState (2);
		}
	}
	void SetAniState(int stateNew){

		if(aniState == stateNew) return;
		aniState = stateNew;
		if (aniState == -1) {
			//idl animation
			animator.SetTrigger("idl");
		} else if (aniState == 0 || aniState == 2) {
			//walk up down
			animator.SetTrigger("walk");
		} else if (aniState == 1) {
			//walk right
			animator.SetTrigger("walkRight");
		} else if (aniState == 3) {
			//walk left
			animator.SetTrigger("walkLeft");
		}
	}

}

