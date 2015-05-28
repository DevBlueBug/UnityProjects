using UnityEngine;
using System.Collections;
using NBehaviour;

public class EnemyFoot : Enemy
{
	public override void Awake ()
	{
		base.Awake ();
		this.bhvs.Add (new BhvFollowPlayer (this));
	}
}

