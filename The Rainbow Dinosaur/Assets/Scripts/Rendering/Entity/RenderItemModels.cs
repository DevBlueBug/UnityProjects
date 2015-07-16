using UnityEngine;
using System.Collections.Generic;

public class RenderItemModels : MonoBehaviour
{
	public GameObject
		Error,
		Money00,Money01,Money02,Bomb,Heart,WeaponStraight,WeaponTriple;
	// Use this for initialization
	public GameObject GetModel(EItem.ItemId id, Dictionary<NItem.Item.KType,NItem.Item> items){

		switch (id) {
		default:
			return Instantiate(Error);
		case EItem.ItemId.Money:
			if(items[NItem.Item.KType.Money].Count <5){
				return Instantiate(Money00)	;
			}
			else if(items[NItem.Item.KType.Money].Count >=5 && items[NItem.Item.KType.Money].Count <10){
				return Instantiate(Money01)	;
			}
			else {
				return Instantiate(Money02)	;
			}
		}
	}


}

