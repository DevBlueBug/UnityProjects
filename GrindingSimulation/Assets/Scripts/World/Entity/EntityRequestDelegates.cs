using UnityEngine;
using System.Collections;

namespace NWorld.NEntity{
	
	public class EntityRequestDelegates 
	{
		
		public delegate bool GiveMe(Entity me, Entity other, params int[] arguments);
		public delegate bool PayMe(Entity me, Entity other, NItem.Price price);
		public delegate bool TakeIt(Entity me, Entity other, params int[] arguments);
		public delegate bool ShowMe(Entity me, Entity other, params System.Object[] arguments);
		public delegate bool SellMe(Entity me, Entity other, NItem.ItemChecker checker);

	}
}