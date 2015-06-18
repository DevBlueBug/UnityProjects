using UnityEngine;
using System.Collections.Generic;

public class ChromaticObject : MonoBehaviour
{
	public RenderSprite renderSprite;

	Vector3 pos;
	List<Color> colors;
	int layer;
	void Start ()
	{
	
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
	public void KUpdate(int layer, List<ChromaticForce> forces){

		var posNew = pos + new Vector3 (
			Random.Range(-.5f,.5f),
			Random.Range(-.5f,.5f),
			0);
		var color = GetColor(pos, posNew);
		renderSprite.layer = layer;
		renderSprite.transform.position = posNew;
		renderSprite.color = color;


	}
	Color GetColor( Vector3 posFrom, Vector3 posTo){
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

