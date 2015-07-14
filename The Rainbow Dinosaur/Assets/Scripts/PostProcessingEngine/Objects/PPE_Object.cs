using UnityEngine;
using System.Collections.Generic;

public class PPE_Object : MonoBehaviour
{
	public RenderSprite renderSprite;
	public bool isSelfUpdate = false;
	internal Vector3 pos;
	List<Color> colors;
	internal int layer;


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
			var color = ChromaticAberration.GetColor(pos, renderSprite.transform.position);
			renderSprite.color = color;
		}
			 

	}
	public virtual void KUpdateSelf(int layer){
		/**
		var posNew = pos + new Vector3 (
			Random.Range(-.5f,.5f),
			Random.Range(-.5f,.5f),
			0);

		var color = ChromaticAberration.GetColor(pos, posNew);
		
		Debug.Log ("SELF UPDATE " + posNew);
		renderSprite.layer = layer;
		renderSprite.transform.position = posNew;
		renderSprite.color = color;
		**/
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
	// Update is called once per frame
	void Update ()
	{
	
	}
}

