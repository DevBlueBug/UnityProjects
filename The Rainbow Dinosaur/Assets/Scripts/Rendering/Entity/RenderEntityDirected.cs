using UnityEngine;
using System.Collections.Generic;

public class RenderEntityDirected : RenderEntityMove	
{
	public enum StateDirection{Idl, Up,Down,Left,Right };
	static Dictionary<StateDirection, string> dicAniStateTrigger = new Dictionary<StateDirection, string>(){
		{StateDirection.Idl, "idl"},
		{StateDirection.Up, "walk"},
		{StateDirection.Right, "walkRight"},
		{StateDirection.Down, "walk"},
		{StateDirection.Left, "walkLeft"},
	};
	public EntityMove entity;
	StateDirection stateAni;
	Vector3[] posBefore = new Vector3[2]{Vector3.zero,Vector3.zero};
	int posBeforeCount = 0 ;
	float aniSpeed = 0;

	float GetEntityMagnitude(){
		//return 0 if veloicty was 0 because it is not actually moving
		return  Mathf.Min(1, entity.velocity.sqrMagnitude*100000)  *
				(entity.transform.localPosition - posBefore[(posBeforeCount+1) & 1]).magnitude;
	}
	void UpdatePosOld(){
		posBeforeCount++;
		posBefore [posBeforeCount & 1] = entity.transform.localPosition;
	}
	
	void UpdateAnimation(){
		var magnitude = GetEntityMagnitude ();
		aniSpeed = Mathf.Max(1, magnitude);
		UpdatePosOld ();

		
		if (magnitude <= .1f) {
			SetAniState (StateDirection.Idl);
			return;
		}
		if (Mathf.Abs (entity.velocity.x) > Mathf.Abs (entity.velocity.y)) {
			if (entity.velocity.x > 0)
				SetAniState (StateDirection.Right);
			else if (entity.velocity.x < 0) 
				SetAniState (StateDirection.Left);
		} else {
			if (entity.velocity.y > 0)
				SetAniState (StateDirection.Up);
			else if (entity.velocity.y < 0) 
				SetAniState (StateDirection.Down);
		}
	}
	void SetAniState(StateDirection stateNew){
		if(stateAni == stateNew) return;
		stateAni = stateNew;
		SetTrigger (dicAniStateTrigger [stateAni]);

	}
}

