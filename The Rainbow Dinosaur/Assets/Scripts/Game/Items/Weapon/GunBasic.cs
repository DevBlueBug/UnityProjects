//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
namespace NItem.NWeapon
{
	public class GunBasic : Weapon
	{
		
		public GunBasic ():base(-1,1f,1,.3f){
		}

		public override void Process (Entity entity,Room room,UnityEngine.Vector3 direction)
		{
			AddBackswing (entity, -direction, backSwing);
			AddBullet(room,entity.transform.localPosition.x,entity.transform.localPosition.y,
			          hpChange,speed,backSwing ,direction);
		}
		/**
		 * 
		 * **/
	}
}

