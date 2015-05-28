using UnityEngine;
using System.Collections.Generic;
using NS_Behaviour.NCondition;

public class EnemyFly : Enemy
{
	//public float
	//	idlCircleRadius,
	//	idlCircleRoundsPerSeconds;
	//float idlVelo;
	//int stateBehavior = 0;
	/**
	 * 0 : idling, run around in circle
	 * 1 : attacked, 
	 **/
	//NS_Behaviour.BhvFollowPlayerStraight bhvFollow;
	//int hpInit;
	//float angle = 0;
	public override void Awake ()
	{
		base.Awake ();
		var bhvFollow = new NS_Behaviour.BhvFollowPlayerStraight(this);
		var bhvRunInCircle = new NS_Behaviour.BhvRunInCircle (this, .3f, 1);
		bhvRunInCircle.condition = new HpChanged (this);
		bhvRunInCircle.bhvSuccess = bhvFollow;
		bhvRunInCircle.bhvFail = bhvFollow;
		bhvs.Add (bhvRunInCircle);
	}
	/**
	public void UpdateIdl(){
		if (this.hp != hpInit) {
			stateBehavior = 1;
			return;
		}
		angle += 6.28f *idlCircleRoundsPerSeconds * Time.deltaTime * Random.Range(.3f,1.2f);
		SetVelocity (new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0) * idlVelo);
	}
	void UpdateFollow(){
		if (this.hp != hpInit) {
			stateBehavior = 1;
			return;
		}
		angle += 6.28f *idlCircleRoundsPerSeconds * Time.deltaTime * Random.Range(.3f,1.2f);
		var direction = (PlayerManager.PlayerPosFloat - this.transform.localPosition).normalized;
		SetVelocity (new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0) * idlVelo
		             + direction *this.velo );
	}
	**/
}

