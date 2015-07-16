using UnityEngine;
using System.Collections;

public class EItem_Reward : EItem
{
	public static float radiusFollowPlayer = 3.0f;
	public override void KUpdate (Room room)
	{
		base.KUpdate (room);
		
		var dis = PlayerManager.PlayerPosFloat- new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, 0) ;
		if (dis.magnitude < radiusFollowPlayer) {
			Move (dis.normalized);
		} else
			SetVelocity (dis.normalized*.3f);
	}
}

