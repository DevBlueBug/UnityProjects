using UnityEngine;
using System.Collections.Generic;

namespace UI.MiniMap{
	
	public class MinMapRoom : MonoBehaviour
	{
		public enum KType{Normal, Boss,Treasure,Shop,Secret};
		public enum KState{None, HasGold,HasHeart,HasBomb};
		public Sprite 
			S_RoomOn, S_RoomOff,
			S_Boss, S_Treasure, S_Shop, S_Secret;
		public SpriteRenderer 
			sprtRoom,sprtSymbol;

		public void SetRoom(bool on){
			sprtRoom.sprite = (on) ? S_RoomOn : S_RoomOff;
		}
		public void SetType(KType state){
			var dicSprites = new Dictionary<KType,Sprite> (){
				{KType.Normal,null},{KType.Boss,S_Boss},{KType.Treasure,S_Treasure},
				{KType.Shop,S_Shop},{KType.Secret,S_Secret}
			};
			sprtSymbol.sprite = dicSprites [state];
		}
		public void SetState(KState myState){
		}
	}

}
