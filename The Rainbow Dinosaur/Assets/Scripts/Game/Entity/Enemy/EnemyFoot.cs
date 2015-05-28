using UnityEngine;
using System.Collections;

public class EnemyFoot : Enemy
{
	public override void Awake ()
	{
		base.Awake ();
		this.bhvs.Add (new NS_Behaviour.BhvFollowPlayer (this));
	}
}

