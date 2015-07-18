using UnityEngine;
using System.Collections.Generic;

public class Prefabs_RenderItem : MonoBehaviour
{
	public static Prefabs_RenderItem me;
	public RenderSprite
		Error,
		Money00,Money01,Money02,
		Head_Player_Default,
		Body_Player_Default,
		Head_Kaonash;
	// Use this for initialization
	public void Init(){
		me = this;
	}
	public static RenderSprite GetModel(NItem.Item.KId id, Dictionary<NItem.Item.KId,NItem.Item> items){

		switch (id) {
		default:
			return Instantiate(me.Error);
		case NItem.Item.KId.Money:
			if(items[NItem.Item.KId.Money].Count <5){
				return Instantiate(me.Money00)	;
			}
			else if(items[NItem.Item.KId.Money].Count >=5 && items[NItem.Item.KId.Money].Count <10){
				return Instantiate(me.Money01)	;
			}
			else {
				return Instantiate(me.Money02)	;
			}
		case NItem.Item.KId.Equip_Head_Player_Default:
			return Instantiate(me.Head_Player_Default);
		case NItem.Item.KId.Equip_Body_Player_Default:
			return Instantiate(me.Body_Player_Default);
		}
	}


}

