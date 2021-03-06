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
		if (GetInventory() != null) Transfer (target);
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
		var inventoryMe = GetInventory ();

		//Debug.Log ("TRANSFER " + inventoryMe.items.Count);
		foreach (var i in inventoryMe.items) {
			if(helperIsRequireTransfer(i.Key) ){
				//Debug.Log("Rrequiring item Transfer " + i.Value.id);
				entity.GetInventory().Add(entity,i.Value);
				continue;
			}
			else{
				//Debug.Log("Not requiring item Transfer");
				SpecialTransfer(entity, i.Value);
			}
		}
	}

	//if weapons
	void SpecialTransfer(Entity entity, Item item){
		if(item.id == Item.KId.Weapon){
			entity.weapon= ((NItem.NWeapon.Weapon)item)
				.SetTargets(entity.weapon.targets);
			//fetch the new weapon to the entity. 
		}
	}


}

