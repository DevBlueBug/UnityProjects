using UnityEngine;
using System.Collections.Generic;

public class PPE_Object : MonoBehaviour
{
	public RenderSprite renderSprite;
	public bool isSelfUpdate = false;
	internal Vector3 pos;
	List<Color> colors;
	internal int layer;
	internal PPE_Object Init (RenderSprite renderSprite, bool isSelfUpdated)
	{
		this.renderSprite = renderSprite;
		this.isSelfUpdate = isSelfUpdated;
		return this;
	
	}

	public void Begin(){
		pos = renderSprite.transform.position;
		layer = renderSprite.layer;
		colors = renderSprite.colors;
	}
	public void End(){
		renderSprite.transform.position = pos;
		renderSprite.layer = layer;
		renderSprite.colors = colors;

	}
	public void KUpdate(int layer, List<PPE_Force> forces){
		if (isSelfUpdate) {
			KUpdateSelf (layer);
			return;
		}
		bool isForceApplied = false;
		for(int i = 0 ; i< forces.Count;i++){
			if(ApplyForce(forces[i]) ) isForceApplied = true;
		}
		if (isForceApplied) {
			renderSprite.layer = layer;
			var color = GetColor(pos, renderSprite.transform.position);
			renderSprite.color = color;
		}
			 

	}
	public virtual void KUpdateSelf(int layer){
		var posNew = pos + new Vector3 (
			Random.Range(-.5f,.5f),
			Random.Range(-.5f,.5f),
			0);

		var color = GetColor(pos, posNew);
	
		renderSprite.layer = layer;
		renderSprite.transform.position = posNew;
		renderSprite.color = color;
	}
	bool ApplyForce(PPE_Force force){
		var dis = renderSprite.transform.position - force.position ;
		var sqrtMagnitude = dis.sqrMagnitude;
		if (sqrtMagnitude > force.distance * force.distance) {
			return false;
		}
		var distance = Mathf.Sqrt (sqrtMagnitude);
		var ratio = (1 - distance / force.distance) *Random.Range(.9f, 1.0f);
		if (Random.Range (0, 5) == 0)
			ratio *= -1;
		this.transform.position += new Vector3 (dis.x /distance,dis.y/distance,0) * ratio * force.magnitude;
		return true;
	}
	public Color GetColor( Vector3 posFrom, Vector3 posTo){
		this.gameObject.layer = layer;
		posFrom = Camera.main.WorldToScreenPoint (posFrom);
		posTo = Camera.main.WorldToScreenPoint (posTo);
		var dis = posFrom - posTo;
		float x = dis.x / Screen.width, y = dis.y / Screen.height;

		Color color = new Color (.5f + x *.5f,.5f+  y*.5f, 0);
		return color;

	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}

