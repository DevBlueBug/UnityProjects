using UnityEngine;
using System.Collections.Generic;
using NBehaviour;
using NBehaviour.NCondition;

public class EnemyBoss_00 : Enemy
{
	public int hpThreshold;
	public float 
		bulletSpeed, 
		fireRate;
	public override void Awake ()
	{
		base.Awake ();
		var follow = new BhvFollowPlayer (this);
		var hpChanged = new HpChanged_Threshold (this, hpThreshold);
		var followThenShoot = new BhvFollowPlayer (this);
		followThenShoot.others.Add (new BhvFireAtPlayer (this, fireRate));

		follow.condition = hpChanged;
		follow.bhvSuccess = followThenShoot;


		bhvs.Add (new BhvFireAtPlayer (this, fireRate));
		weapon = new NWeapon.GunBasic ().SetTargets(KType.Player,KType.World ).SetSpeed(bulletSpeed);


	}
	
}

