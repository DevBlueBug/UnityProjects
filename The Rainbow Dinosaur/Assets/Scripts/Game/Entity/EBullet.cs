using UnityEngine;
using System.Collections.Generic;

public class EBullet : EntityMove
{
	int isWillBeDead = -1;
	Vector3 finalPosition;
	public int hpChange;
	public float forceApplied;
	Vector3 originPoint;
	public override void Awake ()
	{
		E_TriggerTarget += H_TriigerTarget;

		base.Awake ();
	}
	public override void Start ()
	{
		base.Start ();
		originPoint = this.transform.position;
	}
	public override void KUpdate (Room room)
	{
		base.KUpdate (room);
		if (isWillBeDead == -1)
			return;
		if (isWillBeDead == 2)
			Kill ();
		else
			isWillBeDead++;
		//this.transform.position = finalPosition;
	}
	public int H_TriigerTarget(Entity me, Entity other, Collider2D collider){
		isWillBeDead = 1;

		var pos = me.transform.position;
		var velo = new Vector3(body.velocity.x,body.velocity.y,0);
		pos -= this.velocity.normalized *.8f;

		var rayPoint = Physics2D.Raycast (pos, velo);
		if (rayPoint.collider != collider) return 1;
		this.transform.position = rayPoint.point;


		other.HpChange ( HpChangeType.Bullet, hpChange);
		try{
			(other as EntityMove).AddForceVelocity(
				this.body.velocity
				.normalized * forceApplied);  
		}
		catch{
		}

		this.body.velocity = Vector3.zero;
		this.body.isKinematic = true;
		SetVelocity (Vector3.zero);

		return -1;

	}

}

