using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NEntity{
	
	public class Entity 
	{
		public delegate void D_Me(Entity me);
		public delegate float D_Me_Return_Float(Entity me);
		public delegate void D_MeInventoryItem(Entity me, NItem.Inventory inventory,NItem.Item item);

		public D_Me E_DebugText = delegate {	};
	
		public string debug_text = "";
		public void Debug_Text(string text){
			debug_text = text;
			E_DebugText (this);

		}

		internal float x,y,hp =1,hpMax=1;
		internal Stats stats = new Stats();

		internal Dictionary<int,D_Me_Return_Float> 
			sumHpMax = new Dictionary<int, D_Me_Return_Float>(), 
			sumSpeed = new Dictionary<int, D_Me_Return_Float>() , 
			sumArmor = new Dictionary<int, D_Me_Return_Float>(), 
			sumDamage = new Dictionary<int, D_Me_Return_Float>();

		
		internal NItem.Inventory inventory = new NItem.Inventory();
	
		internal EntityRequestDelegates.GiveMe E_ReceiveRequestGiveMe = delegate {return false;};
		internal EntityRequestDelegates.PayMe E_ReceiveRequestPayMe = delegate {return false;};
		internal EntityRequestDelegates.TakeIt E_ReceiveRequestTakeIt = delegate {return false;};
		internal EntityRequestDelegates.ShowMe E_ReceiveRequestShowMe = delegate {return false;};
		internal EntityRequestDelegates.SellMe E_ReceiveRequestSellMe = delegate {return false;};
		
		public bool RequestGiveMe(Entity entity, params int[] arguments){
			return E_ReceiveRequestGiveMe (this, entity, arguments);
		}
		public bool RequestPayMe(Entity entity, NItem.Price price){
			return E_ReceiveRequestPayMe (this, entity, price);
		}
		public bool RequestTakeIt(Entity entity, params int[] arguments){
			return E_ReceiveRequestTakeIt (this, entity, arguments);
		}
		public bool RequestShowMe(Entity entity, params int[] arguments){
			return E_ReceiveRequestShowMe (this, entity, arguments);
		}
		public bool RequestSellMe(Entity entity, NItem.ItemChecker check){
			return E_ReceiveRequestSellMe (this, entity, check);
		}

		internal D_MeInventoryItem E_InventoryItemNew = delegate {	};
		internal D_MeInventoryItem E_InventoryItemRemoved = delegate {	};

		public Entity(){
			this.x = 0;
			this.y = 0;
			Init ();
		}
		public Entity(int x, int y){
			this.x = x;
			this.y = y;
			Init ();
		}
		void Init(){
			
			inventory.E_ItemAdded += delegate {
			};
			
			inventory.E_ItemAdded += (NItem.Inventory.D_MeItem )delegate (NItem.Inventory inv, NItem.Item item){
				E_InventoryItemNew(this,inv,item);
			};
			
			inventory.E_ItemAdded += (NItem.Inventory.D_MeItem )delegate (NItem.Inventory inv, NItem.Item item){
				E_InventoryItemRemoved(this,inv,item);
			};
		}
		public virtual void Update(World world){
		}
		float GetSum(Dictionary<int,D_Me_Return_Float> dic){
			float value = 0;
			var key = dic.Keys.GetEnumerator();
			do {
				value += dic[key.Current](this);
			} while(key.MoveNext());
			return value;

		}
		public float GetHpMax(){
			return GetSum(this.sumHpMax);
		}
		public float GetSpeed(){
			return GetSum(this.sumSpeed);
		}
		public float GetArmor(){
			return GetSum(this.sumArmor);
		}
		public float GetDamage(){
			return GetSum(this.sumDamage);
		}
	}
}

