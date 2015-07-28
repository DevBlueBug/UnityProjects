using UnityEngine;
using System.Collections;
using NBehaviour;
using NBehaviour.NCondition;

public class EnemyFlyAvoid : Enemy
{

	public override void Awake ()
	{
		base.Awake ();
		//this.velo *= 1.75f;
		var bhvFollow = new BhvFollowPlayerFlyWave(this,360,.5f);
		var bhvRunInCircle = new BhvRunInCircle (this, .3f, 1);
		bhvRunInCircle.condition = new CdtCdtHpChanged (this);
		bhvRunInCircle.bhvSuccess = bhvFollow;
		bhvRunInCircle.bhvFail = bhvFollow;
		bhvs.Add (bhvRunInCircle);
		bhvs.Add( new BhvAvoidBulletsForce(this,3.0f,50));
	}
}

