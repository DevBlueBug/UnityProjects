using UnityEngine;
using System.Collections;

namespace Game.Entity.Delegates{
	public delegate void D_TriggerEnter(Collider c);
	public delegate void D_TriggerExit(Collider c);
}