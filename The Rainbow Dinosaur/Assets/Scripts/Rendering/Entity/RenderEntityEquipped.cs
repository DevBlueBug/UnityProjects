using UnityEngine;
using System.Collections;
using NItem;
public class RenderEntityEquipped : RenderEntityDirected
{
	public Prefabs_RenderEntityEquipped P_Prefabs;
	//public enum KTypeEquip	{None, Head, Head_Hat, Body, Body_Front,Body_Back, Body_Pants, Body_Floor};
	public GameObject 
		slotHead,
		slotHead_Hat,
		slotBody,
		slotBody_Front,
		slotBody_Back,
		slotBody_Pants,
		slotFloor;
	public override void Awake ()
	{
		base.Awake ();
		this.entity.inventory.E_EquipItem += H_EquipItem;
	}
	public void Equip(RenderSprite item, NItem.Item.KTypeEquip id){
		switch (id) {
		default:
		case NItem.Item.KTypeEquip.Head:
			Replace(ref slotHead,item);
			break;
		case NItem.Item.KTypeEquip.Head_Hat:
			Replace(ref slotHead_Hat,item);
			break;
		case NItem.Item.KTypeEquip.Body:
			Replace(ref slotBody,item);
			break;
		case NItem.Item.KTypeEquip.Body_Front:
			Replace(ref slotBody_Front,item);
			break;
		case NItem.Item.KTypeEquip.Body_Back:
			Replace(ref slotBody_Back,item);
			break;
		case NItem.Item.KTypeEquip.Body_Pants:
			Replace(ref slotBody_Pants,item);
			break;
		case NItem.Item.KTypeEquip.Floor:
			Replace(ref slotFloor,item);
			break;
		}

	}
	void Replace(ref GameObject original, RenderSprite replacedBy){
		replacedBy.transform.parent = original.transform.parent;
		replacedBy.transform.localPosition = original.transform.localPosition;
		Destroy (original);
		original = replacedBy.gameObject;
	}
	void H_EquipItem(Item item){
		Debug.Log ("EQUIPPING ITEM " + item + " " + item.id + " " + item.typeEquip);
		Equip (P_Prefabs.Get (item.id), item.typeEquip);
		switch (item.id) {
		case Item.KId.Equip_Head_Default:

			break;
		}
		//equipping item now
	}

}

