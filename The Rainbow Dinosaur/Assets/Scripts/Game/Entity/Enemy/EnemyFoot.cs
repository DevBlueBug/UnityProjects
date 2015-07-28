using UnityEngine;
using System.Collections;
using NBehaviour;
using NBehaviour.NCondition;
public class EnemyFoot : Enemy
{
	public override void Awake ()
	{
		base.Awake ();
		this.bhvs.Add (new BhvFollowPlayer (this));
		this.bhvs [0].condition = new CdtPathToPlayerAvailable (this);
	}
}

