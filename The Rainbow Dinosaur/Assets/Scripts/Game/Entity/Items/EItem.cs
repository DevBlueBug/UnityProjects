using UnityEngine;
using System.Collections.Generic;
using NItem;
public class EItem : EntityMove
{

	public delegate void D_PlayerAcquired(EItem item, Entity player);
	public D_PlayerAcquired E_PlayerAcquired = delegate {};

	public EItem P_Original;
	
	public Item.KId			idItem;
	public Item.KTypeEquip	typeEquip;
	public int value;
	bool isStablized = false;


	public override void Awake ()
	{
		base.Awake ();
		this.E_TriggerTarget += H_TriggerTarget;
	}
	
	public override void KUpdate (Room room)
	{
		base.KUpdate (room);
		if (!isStablized) {
			isStablized = IsStablized();
		}
	}
	bool IsStablized(){
		return (this.transform.position - PlayerManager.PlayerPosFloat).magnitude > 1.0f;
	}
	int H_TriggerTarget(Entity me, Entity target, Collider2D collider){
		//E_PlayerAcquired (this, target);
		//Triggered (target);
		if (!isStablized) return 0;
		if (inventory != null) Transfer (target);
		Kill ();
		return -1;
	}
	static List<Item.KId> itemsRequireNoTransfer = new List<Item.KId>(){
		Item.KId.Unknown, Item.KId.Weapon
	};
	bool helperIsRequireTransfer(Item.KId type){
		foreach (var itemType in itemsRequireNoTransfer) {
			if(type == itemType) return false;
		}
		return true;
	}


	void Transfer(Entity entity){
		foreach (var i in inventory.items) {
			if(helperIsRequireTransfer(i.Key) ){
				entity.inventory.Add(entity,i.Value);
				continue;
			}
			else{
				SpecialTransfer(entity, i.Value);
			}
		}
	}
	void SpecialTransfer(Entity entity, Item item){
		if(item.id == Item.KId.Weapon){
			entity.weapon= ((NItem.NWeapon.Weapon)item)
				.SetTargets(entity.weapon.targets);
			//fetch the new weapon to the entity. 
		}
	}


}

