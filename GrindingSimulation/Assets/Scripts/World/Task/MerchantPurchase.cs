using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NTask{
	
	public class MerchantPurchase :Task
	{
		NWorld.NEntity.NNPC.NMerchant.Merchant merchants;
		new public void Init (World world, NWorld.NEntity.NUnit.Unit unit, NWorld.NItem.ItemChecker check)
		{
			base.Init (world, unit);
		}
	}
}

