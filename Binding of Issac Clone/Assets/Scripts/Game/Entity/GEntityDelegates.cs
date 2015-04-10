using UnityEngine;
using System.Collections;

namespace Game.Entity.Delegates{
	public delegate void D_FixedUpdate(GEntity me, GRoom room);
	public delegate void D_Attack(GEntity entity, int n);
	public delegate void D_Birth(GEntity me, GEntity child);
	public delegate void D_Kill(GEntity me);
	public delegate void D_HpChange(GEntity me, float change);
	public delegate void D_ForceApplied(GEntity me, Vector3 force);

	public delegate void D_OnCollisionEnter(GEntity me, Collision c);
	public delegate void D_OnTriggerEnter(GEntity me, Collider c);
	public delegate void D_OnTriggerStay(GEntity me, Collider c);
}