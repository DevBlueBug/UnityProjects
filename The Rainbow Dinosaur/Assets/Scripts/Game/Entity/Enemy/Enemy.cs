using UnityEngine;
using System.Collections;

public class Enemy : EntityMove
{
	public int damageOnContact = -1;
	public override void Awake ()
	{
		base.Awake ();
		this.E_TriggerTarget += H_Damage;
	}
	public int H_Damage(Entity me, Entity other, Collider2D collider){
		//Debug.Log ("DAMAGE!");
		other.HpChange (HpChangeType.Contact, damageOnContact);
		return 1;
	}

}

