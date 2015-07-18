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
			list.Add(GetDrop(new Item(Item.KId.Money,RatioGoldL)));
		}
		for (int i = 0; i < nMiddle; i++) {
			list.Add(GetDrop(new Item(Item.KId.Money,RatioGoldM)));
		}
		for (int i = 0; i < nSmall; i++) {
			list.Add(GetDrop(new Item(Item.KId.Money,RatioGoldS)));
		}

		return list;
	}
	public EItem FillItemContent(EItem body){
		switch (body.idItem) {
		case Item.KId.Equip_Body_Player_Default:
			break;
		case Item.KId.Equip_Head_Player_Default:
			break;
		}
		return body;
	}
	
	public EItem GetDrop(Item item){
		var obj = AddContent( Instantiate(ItemDrop), item);
		return obj;
	}
	
	public EItem GetDropStuck(Item item){
		var obj = AddContent( Instantiate(ItemDropStuck), item);
		return obj;
	}
	
	public EItem GetStationary(Item item){
		var obj = AddContent( Instantiate(ItemDrop),item);
		return obj;
	}
	EItem AddContent(EItem model, Item item){
		model.GetInventory().Add(model,item);
		model.idItem = item.id;
		switch (model.idItem) {
		case Item.KId.Equip_Head_Player_Default:
		case Item.KId.Equip_Body_Player_Default:
		case Item.KId.Equip_Head_Kaonash:
			break;
		}
		return model;
	}



}

