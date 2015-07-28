using UnityEngine;
using System.Collections;

public class PiggyZombie : Enemy
{
	enum State{Idl, followingPlayer};

	public override void Awake ()
	{
		base.Awake ();

	}
}

