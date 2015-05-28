using UnityEngine;
using System.Collections;

public class EItemList : MonoBehaviour
{
	public EItem 
		Error,
		Heart,
		Money,
		MoneyBig,
		Bomb,
		BombBig;

	public EItem Get(EItem.ItemId type){
		switch (type) {
		default : return Instantiate(Error);
		case EItem.ItemId.Heart : return Instantiate(Heart);
		case EItem.ItemId.Money : return Instantiate(Money);
		case EItem.ItemId.MoneyBig : return Instantiate(MoneyBig);
		case EItem.ItemId.Bomb : return Instantiate(Bomb);
		case EItem.ItemId.BombBig : return Instantiate(BombBig);
		}
	}
	public EItem GetRandom(){
		EItem.ItemId type = (EItem.ItemId)Random.Range (0, 5);
		return Get (type);
	}
}

