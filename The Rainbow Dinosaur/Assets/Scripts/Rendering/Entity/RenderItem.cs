using UnityEngine;
using System.Collections;

public class RenderItem : MonoBehaviour
{
	public GameObject model;
	// Use this for initialization
	void Start ()
	{
		var item = GetComponent<EItem> ();

		var m =  Prefabs_RenderItem.GetModel (item.idItem,item.inventory.items);
		m.transform.parent = model.transform.parent;
		m.transform.localPosition = model.transform.localPosition;
		Destroy (model);
		model = m;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

