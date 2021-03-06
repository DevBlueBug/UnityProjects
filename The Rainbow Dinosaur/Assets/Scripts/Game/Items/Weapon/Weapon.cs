//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NItem.NWeapon
{
	public class Weapon : Item
	{
		public delegate void D_Backswing(Vector3 position, Vector3 direction, float force);
		public static D_Backswing E_Backswing = delegate {	};
		internal BulletManager.KTypes typeBullet;
		internal float 
			speed = 0.0f,
			backSwing = 0.0f, 
			delay = 1.0f,
			hpChange = -1.0f;

		float timeOld = 0.0f;
		internal List<Entity.KType> targets;


		public Weapon ()
		{
			this.targets = new List<Entity.KType> ();
			base.id = KId.Weapon;
			typeBullet = BulletManager.KTypes.Normal;
		}
		public Weapon(float hpChange, float speed, float backswing,float delay):this(){
			this.hpChange = hpChange;
			this.speed = speed;
			this.backSwing = backswing;
			this.delay = delay;
		}
		
		public void Attack(Entity entity,Room room, Vector3 direction){
			float timeElapsed = Time.time - timeOld;
			if (timeElapsed < delay) return;
			timeOld = Time.time;
			//Debug.Log (delay);
			Process (entity,room,direction);
			//return null;
		}
		public virtual void Process(Entity entity, Room room, Vector3 direction){
		}

		public Weapon SetTargets(params Entity.KType[] targetList){
			foreach (var t in targetList) {
				targets.Add(t);
			}
			return this;
		}
		
		public Weapon SetTargets(List<Entity.KType> targetsList){
			targets = targetsList;
			return this;
		}
		public Weapon SetSpeed(float s){
			this.speed = s;
			return this;
		}
		
		
		internal void AddBackswing(Entity entity, Vector3 direction, float amount){
			E_Backswing (entity.transform.position, direction, amount);
			((EntityMove)entity).AddForceVelocity(direction * amount);
		}
		public EBullet AddBullet(Room room,  float x, float y,
		                         float hpChange, float speed, float backswing, 
		                         Vector3 direction){
			var bullet = BulletManager.E_RequestBullet (BulletManager.KTypes.Normal);
			bullet.hpChange = hpChange;
			bullet.velocity = speed;
			bullet.direction = direction;
			bullet.forceApplied = backswing;
			bullet.targets = this.targets;
			room.AddEntity (bullet,x,y, false);
			
			return bullet;
		}

	}
}

