using UnityEngine;
using System.Collections;

public class RenderEntity : MonoBehaviour
{
	public TextMesh textMesh;
	NWorld.Entity entity;
	Vector3 position;
	public RenderEntity Init(NWorld.Entity entity){
		this.entity = entity;
		entity.E_DebugText += Link;
		return this;
	}
	public void KUpdate(float timeElapsed){
		Link (entity);
		this.transform.localPosition = transform.localPosition + (position - transform.localPosition) * (timeElapsed *10);
	}

	void Link(NWorld.Entity e){
		position = new Vector3 (e.x, e.y, 0);
		textMesh.text = e.debug_text;
	}
}

