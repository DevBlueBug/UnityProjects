using UnityEngine;
using System.Collections;

public class RenderItem : RenderEntity
{
	public GameObject model;

	EItem GetEntity(){
		return(EItem)entity;
	}
	public override void Start ()
	{
		base.Start ();
		
		var item = GetEntity ();
		
		var m =  Prefabs_RenderItem.GetModel (item.idItem,item.inventory.items);
		renderSprites.Add (m);
		m.transform.parent = model.transform.parent;
		m.transform.position = model.transform.position;
		Destroy (model);
		model = m.gameObject;
	}
}

