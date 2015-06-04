using UnityEngine;
using System.Collections;
using NItem;
using NItem.NWeapon;

public class EItemManager : MonoBehaviour
{
	public EItem
		ItemPrefab,
		Error,
		Heart,
		Money,
		MoneyBig,
		Bomb,
		BombBig;

	public EItem Get(EItem.ItemId type){
		var obj = Instantiate(ItemPrefab);
		try{
			obj.GetComponentInChildren<TextMesh> ().text = type.ToString ();
		}
		catch{
		}
		obj.inventory = GetInventory (type);
		return obj;
	}

	public EItem GetRandom(){
		EItem.ItemId type = (EItem.ItemId)Random.Range (0, 5);
		return Get (type);
	}
	internal EItem GetPrefab(EItem.ItemId type){
		switch (type) {
		default : return Instantiate(Error);
		case EItem.ItemId.Heart : return Instantiate(Heart);
		case EItem.ItemId.Money : return Instantiate(Money);
		case EItem.ItemId.MoneyBig : return Instantiate(MoneyBig);
		case EItem.ItemId.Bomb : return Instantiate(Bomb);
		case EItem.ItemId.BombBig : return Instantiate(BombBig);
		}
	}
	internal NItem.Inventory GetInventory(EItem.ItemId type){
		var inventory = new NItem.Inventory ();
		//inventory.Add (NItem.Item.GetMoney ());
		inventory.Add (new Shotgun());
		return inventory;
	}

}

