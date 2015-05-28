using UnityEngine;
using System.Collections.Generic;

public class EBullet : EntityMove
{
	public int hpChange;
	public float forceApplied;
	public override void Awake ()
	{
		E_TriggerTarget += H_TriigerTarget;

		base.Awake ();
	}
	public int H_TriigerTarget(Entity me, Entity other){
		other.HpChange ( HpChangeType.Bullet, hpChange);
		try{
			(other as EntityMove).AddForceVelocity(
				(other.transform.localPosition - me.transform.localPosition)
				.normalized * forceApplied);  
		}
		catch{
		}
		this.Kill ();
		return -1;

	}

}

