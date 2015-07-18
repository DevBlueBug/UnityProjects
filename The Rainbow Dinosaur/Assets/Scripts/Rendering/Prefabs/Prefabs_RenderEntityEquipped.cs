using UnityEngine;
using System.Collections;

public class Prefabs_RenderEntityEquipped : MonoBehaviour
{
	public static Prefabs_RenderEntityEquipped me;

	public RenderEntityMovePart 
		Undefined,
		Head_Player_Default,
		Body_Player_Default,
		Head_Kaonash;
	public void Init(){
		me = this;
	}
	public static RenderEntityMovePart Get(NItem.Item.KId id){
		switch (id) {
		default:
			return Instantiate(me.Undefined);
		case NItem.Item.KId.Equip_Head_Player_Default:
			return Instantiate(me.Head_Player_Default);
		case NItem.Item.KId.Equip_Body_Player_Default:
			return Instantiate(me.Body_Player_Default);
		case NItem.Item.KId.Equip_Head_Kaonash:
			return Instantiate(me.Head_Kaonash);

		}
	}
}

