using UnityEngine;
using System.Collections;

public class AnimationPlay : MonoBehaviour
{
	public AnimationClip clip;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.X) ){
			var animator = GetComponent<Animator>();
			AnimatorOverrideController overrideController = new AnimatorOverrideController();
			overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;


			//var c = (AnimatorOverrideController)animator.runtimeAnimatorController;

			overrideController["walking"]  = clip;

			animator.runtimeAnimatorController = overrideController;

		}
	
	}
}

