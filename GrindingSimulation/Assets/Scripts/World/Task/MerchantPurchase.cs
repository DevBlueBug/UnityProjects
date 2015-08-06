using UnityEngine;
using System.Collections.Generic;
using NWorld.NEntity.NUnit.NOrder;

namespace NWorld.NTask{
	
	public class MerchantPurchase :Task
	{

		NWorld.NEntity.NNPC.Merchant merchant;
		internal float price;
		NItem.ItemChecker check;
		public MerchantPurchase(NEntity.NNPC.Merchant merchant, NItem.ItemChecker checker, float price){
			this.price = price;
			this.check = checker;
			this.merchant = merchant;
			this.type = KEnums.TypeTask.BuyFromMerchant;
		}
		public override float GetWeight ()
		{
			return 1/price;
			/**
			float weight = 0;
			for (int i = 0; i < price.prices.Count; i++) {
				var p =price.prices[i];
				weight += NItem.ItemList.GetValue( p.id) * p.count;
			}
			return weight;
			 **/
		}
		public override void Init (World world, NWorld.NEntity.NUnit.Unit unit)
		{
			var path = world.mapAStar.GetPath (new Vector2 ( unit.x,unit.y), new Vector2 (merchant.x, merchant.y));
			if (path == null)
				return;
			for (int i = 0; i< path.Count; i++) {
				unit.AddOrder(world,new Move(world,unit,path[i].x,path[i].y) );
			}
			unit.AddOrder(world,new NEntity.NUnit.NOrder.Request_SellMe(world,unit,this.merchant,this.check) );
			//unit.AddOrder(world, new  NWorld.NEntity.NUnit.NOrder.Request_GiveMe

			base.Init (world, unit);
		}
	}
}

