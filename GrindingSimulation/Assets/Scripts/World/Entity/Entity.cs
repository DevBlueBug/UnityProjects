using UnityEngine;
using System.Collections.Generic;

namespace NWorld{
	
	public class Entity 
	{
		public delegate void D_Me(Entity me);
		public delegate bool D_ReceiveRequest(Entity me, Entity other, params System.Object[] arguments);

		public D_Me E_DebugText = delegate {	};
		public string debug_text = "";
		public void Debug_Text(string text){
			debug_text = text;
			E_DebugText (this);

		}

		internal float 
			x,y,
			hp=1,speed=0,armor=0,damage=0;
		
		internal NItem.Inventory inventory = new NItem.Inventory();
		
		internal D_ReceiveRequest E_ReceiveRequestGiveMe = delegate {return false;};
		internal D_ReceiveRequest E_ReceiveRequestTakeIt = delegate {return false;};

		public Entity(int x, int y){
			this.x = x;
			this.y = y;
		}
		public virtual void Update(World world){
		}
		public bool Requested(Entity entity, KEnums.TypeRequest request, params System.Object[] arguments){
			if(request == KEnums.TypeRequest.GiveMe){
				return E_ReceiveRequestGiveMe(this,entity,arguments);
			}
			else if(request == KEnums.TypeRequest.TakeIt){
				return E_ReceiveRequestTakeIt(this,entity,arguments);
			}
			return false;
		}
	}
}

