using UnityEngine;
using System.Collections.Generic;

public class EItem : EntityMove
{
	public enum ItemId {
		Error =-1,  
		Heart=0, 
		Money=1,MoneyBig=2,
		Bomb=3, BombBig=4,
		SwordSimple=5, GunSimple = 6};
	public enum ItemPosition {None=0, Head, Body, Hand,Foot,Weapon};
	public delegate void D_PlayerAcquired(EItem item);
	public static D_PlayerAcquired E_PlayerAcquired = delegate {};

	public EItem P_Original;
	
	public ItemId		itemId;
	public ItemPosition	itemPosion;
	public int value;

	public override void Awake ()
	{
		base.Awake ();
		this.E_TriggerTarget += H_TriggerTarget;
	}
	int H_TriggerTarget(Entity me, Entity target, Collider2D collider){
		E_PlayerAcquired (this);
		Kill ();
		return -1;
	}


}

