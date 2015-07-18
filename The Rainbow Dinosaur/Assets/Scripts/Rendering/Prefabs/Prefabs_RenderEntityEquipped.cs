using UnityEngine;
using System.Collections;

public class Prefabs_RenderEntityEquipped : MonoBehaviour
{
	public static Prefabs_RenderEntityEquipped me;

	public RenderSprite 
		Head_Player_Default,
		Body_Player_Default;
	public void Init(){
		me = this;
	}
	public RenderSprite Get(NItem.Item.KId id){
		return Head_Player_Default;
	}
}

