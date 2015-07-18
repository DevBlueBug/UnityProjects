using UnityEngine;
using System.Collections.Generic;

public class EBullet : Entity
{
	public delegate void D_Moved(EBullet me);
	public delegate void D_Hit(Entity hitted, Vector3 reflective);
	public D_Hit E_Hit = delegate {};
	public D_Moved E_Moved = delegate {};

	int isWillBeDead = -1;
	Vector3 finalPosition;
	public float hpChange;
	public float forceApplied;
	public Vector3 direction;
	public float velocity;
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
	bool UpdateBullet(float amount){

		var rayCast = Physics2D.Raycast (this.transform.position, direction,amount);
		
		if (rayCast.collider == null) {
			this.transform.localPosition += direction * amount;
			return true;
		}
		try{
			var entity = rayCast.collider.GetComponent<PointerEntity>().entity;

			//rayCast.normal
		
			//if(rayCast.distance > amount*.1f) this.transform.transform.position = rayCast.point;
			//else 

			if(helperIsMyTarget(entity.meType)){
				var relfectiveVar = Vector3.Reflect( rayCast.point - 
				                                    (rayCast.point - new Vector2(direction.x,direction.y)) ,  rayCast.normal);
				//Debug.Log("reflective " + relfectiveVar.normalized);
				E_Hit(entity, relfectiveVar);
				Hit(entity);
				Kill();
				return false;
			}
			else{
				this.transform.transform.position += direction*amount;
			}
		}
		catch{
		}
		return true;


	}
	public override void KUpdate (Room room)
	{
		base.KUpdate (room);
		int countEffect = 0;
		float count = (velocity*Time.deltaTime) /.1f;
		for (int i = 0; i< count && i < 900; i++) {

			if(!UpdateBullet(.1f)) break;
			if(countEffect++ % 3 == 0){
				countEffect = 1;
				E_Moved(this);
			}
			if(i >= 900)Debug.Log("NO");
		}


		if (isWillBeDead == -1)
			return;
		if (isWillBeDead == 2)
			Kill ();
		else
			isWillBeDead++;
		//this.transform.position = finalPosition;
	}
	void Hit(Entity other){
		other.HpChange ( HpChangeType.Bullet, hpChange);
		try{
			(other as EntityMove).AddForceVelocity( direction * forceApplied);  
		}
		catch{
		}

	}
	public int H_TriigerTarget(Entity me, Entity other, Collider2D collider){

		return 1;
		/**
		var pos = me.transform.position;
		var velo = new Vector3(body.velocity.x,body.velocity.y,0);
		var veloNormalized = velo.normalized;

		RaycastHit2D rayPoint;
		int count = 0;
		do {
			pos -= veloNormalized * .5f;
			rayPoint = Physics2D.Raycast (pos, velo);
		} while(count++ <10 && rayPoint.collider != collider);

		if (rayPoint.collider != null) {
			this.transform.position = pos;
			isWillBeDead = 1;
		} else return 1;





		this.body.velocity = Vector3.zero;
		this.body.isKinematic = true;
		SetVelocity (Vector3.zero);

		return -1;
		**/

	}

}

