using UnityEngine;
using System.Collections;
using NBehaviour;
using NBehaviour.NCondition;
public class EnemyFlyAttack : Enemy
{
	public override void Awake ()
	{
		base.Awake ();
		this.weapon = new NItem.NWeapon.GunBasic ()
			.SetTargets (Entity.KType.Player, Entity.KType.World);
		var bhvRunInCircle = new BhvRunInCircle (this, .3f, 1);
		var bhvFollow = new BhvFollowPlayerFly(this);
		bhvRunInCircle.condition = new CdtCdtHpChanged (this)
			.SetOr (new CdtTimeElapsed(this, 2.0f));
		bhvRunInCircle.bhvSuccess = bhvFollow;
		bhvRunInCircle.bhvFail = bhvFollow;
		bhvFollow.condition = new CdtTimeElapsed (this, 3.0f);
		bhvFollow.bhvSuccess = new BhvFireAtPlayer (this, 0.0f);
		bhvFollow.bhvSuccess.condition = new CdtTimeElapsed (this, -1);
		bhvFollow.bhvSuccess.bhvSuccess = bhvFollow;
		bhvs.Add (bhvRunInCircle);
	}
}

