using UnityEngine;
using System.Collections.Generic;

public class RenderEntityDirected : RenderEntityMove	
{
	public enum StateDirection{Idl, Front,Back,Left,Right };
	static Dictionary<StateDirection, string> dicAniStateTrigger = new Dictionary<StateDirection, string>(){
		{StateDirection.Idl, "idl"},
		{StateDirection.Front, "walkFront"},
		{StateDirection.Right, "walkRight"},
		{StateDirection.Back, "walkBack"},
		{StateDirection.Left, "walkLeft"},
	};
	StateDirection stateAni;
	Vector3[] posBefore = new Vector3[2]{Vector3.zero,Vector3.zero};
	int posBeforeCount = 0 ;
	float aniSpeed = 0;

	float GetEntityMagnitude(){
		//Debug.Log("MAGNITDUE " + posBefore[(posBeforeCount+1) & 1] + " TO " + entity.transform.localPosition ) ;
		//return 0 if veloicty was 0 because it is not actually moving
		bool isMovingItsOwn = Mathf.Min (1, GetEntity ().velocity.sqrMagnitude * 100000) == 1;
		return	(isMovingItsOwn)? 
				Mathf.Max (.001f, (entity.transform.localPosition - posBefore [(posBeforeCount + 1) & 1]).magnitude) :
				0;
	}
	void UpdatePosOld(){
		posBeforeCount++;
		posBefore [posBeforeCount & 1] = entity.transform.localPosition;
	}
	
	void UpdateAnimation(){
		var magnitude = GetEntityMagnitude ();
		aniSpeed = Mathf.Max(1, magnitude*20);
		SetAniSpeed (aniSpeed);
		UpdatePosOld ();

		
		if (magnitude <= .0005f) {
			SetAniState (StateDirection.Idl);
			return;
		}
		var velocity = GetEntity ().velocity;
		if (Mathf.Abs (velocity.x) > Mathf.Abs (velocity.y)) {
			if (velocity.x > 0)
				SetAniState (StateDirection.Right);
			else if (velocity.x < 0) 
				SetAniState (StateDirection.Left);
		} else {
			if (velocity.y > 0)
				SetAniState (StateDirection.Back);
			else if (velocity.y < 0) 
				SetAniState (StateDirection.Front);
		}
	}
	void SetAniState(StateDirection stateNew){
		if(stateAni == stateNew) return;
		stateAni = stateNew;

		SetTrigger (dicAniStateTrigger [stateAni]);
	}
	public override void Update ()
	{
		base.Update ();
		UpdateAnimation ();	
	}
}

