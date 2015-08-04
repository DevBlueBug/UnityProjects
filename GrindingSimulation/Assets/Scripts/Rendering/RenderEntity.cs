using UnityEngine;
using System.Collections;

public class RenderEntity : MonoBehaviour
{
	public delegate void D_ME(RenderEntity me);
	public D_ME E_Clicked =delegate {};
	public TextMesh textMesh;
	internal NWorld.NEntity.Entity entity;
	Vector3 position;
	public RenderEntity Init(NWorld.NEntity.Entity entity){
		this.entity = entity;
		entity.E_DebugText += Link;
		return this;
	}
	public void KUpdate(float timeElapsed){
		Link (entity);
		this.transform.localPosition = transform.localPosition + (position - transform.localPosition) * (timeElapsed *10);
	}

	void Link(NWorld.NEntity.Entity e){
		position = new Vector3 (e.x, e.y, 0);
		textMesh.text = e.debug_text;
	}
	void OnMouseDown(){
		E_Clicked (this);
		//Debug.Log (this.gameObject.name + " MOUSE DOWN ");
	}
}

