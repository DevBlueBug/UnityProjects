using UnityEngine;
using System.Collections;

public class RenderEntityEquipped : RenderEntityDirected
{
	//public enum KTypeEquip	{None, Head, Head_Hat, Body, Body_Front,Body_Back, Body_Pants, Body_Floor};
	public GameObject 
		slotHead,
		slotHead_Hat,
		slotBody,
		slotBody_Front,
		slotBody_Back,
		slotBody_Pants,
		slotBody_Floor;
	public void Equip(NItem.Item.KTypeEquip id, GameObject item){
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
		case NItem.Item.KTypeEquip.Body_Floor:
			Replace(ref slotBody_Floor,item);
			break;
		}

	}
	void Replace(ref GameObject original, GameObject replacedBy){
		replacedBy.transform.parent = original.transform.parent;
		replacedBy.transform.localPosition = original.transform.localPosition;
		Destroy (original);
		original = replacedBy;
	}

}

