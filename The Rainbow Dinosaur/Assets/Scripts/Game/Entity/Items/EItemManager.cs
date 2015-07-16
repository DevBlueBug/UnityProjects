using UnityEngine;
using System.Collections.Generic;
using NItem;
using NItem.NWeapon;

public class EItemManager : MonoBehaviour
{
	static int 
		RatioGoldS = 1,
		RatioGoldM = 5,
		RatioGoldL = 10;
	public EItem
		ItemDrop,
		ItemDropStuck,
		ItemStationary;
	public List<EItem> GetMoney(int amount){
		var list = new List<EItem> ();
		int nLarge = (int)(amount / RatioGoldL);
		int nMiddle = (int)((amount - nLarge * RatioGoldL) / RatioGoldM);
		int nSmall = (int)((amount - nLarge * RatioGoldL - nMiddle * RatioGoldM  ) / RatioGoldS);
		for(int i = 0 ; i < nLarge;i++){
			list.Add(GetDrop(new Item(Item.KType.Money,RatioGoldL)));
		}
		for (int i = 0; i < nMiddle; i++) {
			list.Add(GetDrop(new Item(Item.KType.Money,RatioGoldM)));
		}
		for (int i = 0; i < nSmall; i++) {
			list.Add(GetDrop(new Item(Item.KType.Money,RatioGoldS)));
		}

		return list;
	}
	
	public EItem GetDrop(Item item){
		var obj = AddItem( Instantiate(ItemDrop), item);
		return obj;
	}
	
	public EItem GetDropStuck(Item item){
		var obj = AddItem( Instantiate(ItemDropStuck), item);
		return obj;
	}
	
	public EItem GetStationary(Item item){
		var obj = AddItem( Instantiate(ItemDrop),item);
		return obj;
	}
	EItem AddItem(EItem model, Item item){
		model.inventory.Add(item);
		model.idItem = EItem.ItemId.Error;
		if (item.meType == Item.KType.Money) {
			model.idItem = EItem.ItemId.Money;
		}
		return model;
	}


	internal NItem.Inventory GetInventory(EItem.ItemId type){
		var inventory = new NItem.Inventory ();
		//inventory.Add (NItem.Item.GetMoney ());
		inventory.Add (new Shotgun());
		return inventory;
	}

}

