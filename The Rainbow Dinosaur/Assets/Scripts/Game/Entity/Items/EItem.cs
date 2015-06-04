using UnityEngine;
using System.Collections.Generic;
using NItem;
public class EItem : EntityMove
{
	public enum ItemId {
		Error =-1,  
		Heart=0, 
		Money=1,MoneyBig=2,
		Bomb=3, BombBig=4,
		SwordSimple=5, GunSimple = 6};
	public enum ItemPosition {None=0, Head, Body, Hand,Foot,Weapon};
	public delegate void D_PlayerAcquired(EItem item, Entity player);
	public D_PlayerAcquired E_PlayerAcquired = delegate {};

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
		//E_PlayerAcquired (this, target);
		//Triggered (target);
		if (inventory != null) Transfer (target);
		Kill ();
		return -1;
	}
	static List<Item.KType> itemsRequireNoTransfer = new List<Item.KType>(){
		Item.KType.Nothing, Item.KType.Weapon
	};
	bool helperIsRequireTransfer(Item.KType type){
		foreach (var itemType in itemsRequireNoTransfer) {
			if(type == itemType) return false;
		}
		return true;
	}


	void Transfer(Entity entity){
		foreach (var i in inventory.items) {
			if(helperIsRequireTransfer(i.Key) ){
				entity.inventory.Add(i.Value);
				continue;
			}
			else{
				SpecialTransfer(entity, i.Value);
			}
		}
	}
	void SpecialTransfer(Entity entity, Item item){
		if(item.meType == Item.KType.Weapon){
			entity.weapon= ((NItem.NWeapon.Weapon)item)
				.SetTargets(entity.weapon.targets);
			//fetch the new weapon to the entity. 
		}
	}


}

