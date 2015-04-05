using UnityEngine;
using System.Collections;

namespace Game.Entity.Delegates{
	public delegate void D_Attack(GEntity entity, int n);
	public delegate void D_Birth(GEntity me, GEntity child);
	public delegate void D_Kill(GEntity me);
	public delegate void D_OnCollisionEnter(GEntity me, Collision c);
	public delegate void D_OnSwitchEnter(GEntity me, Collider c);
}