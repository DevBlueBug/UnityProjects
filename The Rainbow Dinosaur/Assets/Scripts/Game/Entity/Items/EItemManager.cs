using UnityEngine;
using System.Collections.Generic;
using NItem;
using NItem.NWeapon;

public class EItemManager : MonoBehaviour
{
	public EItem
		ItemDrop,
		ItemStationary,
		ItemPrefab;
	
	public EItem GetDrop(Item item){
		var obj = Instantiate(ItemDrop);
		obj.inventory.Add(item);
		return obj;
	}
	
	public EItem GetStationary(Item item){
		var obj = Instantiate(ItemDrop);
		obj.inventory.Add(item);
		return obj;
	}


	public EItem Get(EItem.ItemId type){
		var obj = Instantiate(ItemPrefab);

		/**
		try{
			obj.GetComponentInChildren<TextMesh> ().text = type.ToString ();
		}
		catch{
		}
		**/
		obj.inventory = GetInventory (type);
		return obj;
	}

	public EItem GetRandom(){
		EItem.ItemId type = (EItem.ItemId)Random.Range (0, 5);
		return Get (type);
	}
	internal NItem.Inventory GetInventory(EItem.ItemId type){
		var inventory = new NItem.Inventory ();
		//inventory.Add (NItem.Item.GetMoney ());
		inventory.Add (new Shotgun());
		return inventory;
	}

}

