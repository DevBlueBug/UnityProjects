using UnityEngine;
using System.Collections;
using NBehaviour;
using NBehaviour.NCondition;

public class EnemyFlyDancer : Enemy
{

	public override void Awake ()
	{
		base.Awake ();
		var bhvFollow = new BhvFollowPlayerFly(this);
		var bhvRunInCircle = new BhvRunInCircle (this, .3f, 1);
		bhvRunInCircle.condition = new HpChanged (this);
		bhvRunInCircle.bhvSuccess = bhvFollow;
		bhvRunInCircle.bhvFail = bhvFollow;
		bhvs.Add (bhvRunInCircle);
	}
}

