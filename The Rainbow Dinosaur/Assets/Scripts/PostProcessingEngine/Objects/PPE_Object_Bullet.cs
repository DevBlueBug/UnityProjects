using UnityEngine;
using System.Collections;

public class PPE_Object_Bullet : PPE_Object
{
	public RenderBullet renderBullet;
	public override void KUpdateSelf (int layer)
	{
		/**
		var dir = renderBullet.bullet.direction;
		var posNew = pos + dir * Random.Range (-1.5f, 1.5f);
		posNew += new Vector3 (-dir.y, dir.x, 0) * Random.Range (-.1f, .1f);
		renderSprite.layer = layer;
		renderSprite.transform.position = posNew;
		renderSprite.color = ChromaticAberration.GetColor (pos,posNew);
		**/

	}
}

