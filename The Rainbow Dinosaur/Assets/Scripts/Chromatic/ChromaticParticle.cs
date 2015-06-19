using UnityEngine;
using System.Collections;

public class ChromaticParticle : MonoBehaviour
{
	public Material mat;
	public SpriteRenderer sprRender;
	// Use this for initialization
	public int count;
	void Start(){
		sprRender= this.gameObject.AddComponent<SpriteRenderer> ();
		sprRender.material = mat;
	}
	public void On(Sprite sprite, int layer,Vector3 position,Color color, int count){
		this.gameObject.SetActive (true);
		this.transform.position = position;
		sprRender.color = color;
		this.count = count;
		this.gameObject.layer = layer;
		this.sprRender.sprite = sprite;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 c = new Vector3 (sprRender.color.r, sprRender.color.g, 0);

		Vector3 color = c + (new Vector3 (.5f, .5f, 0) - c )* 1.1f * Time.deltaTime;
		this.sprRender.color = new Color (c.x, c.y, 0,sprRender.color.a - .1f*Time.deltaTime);
		if (--count <= 0)
			this.gameObject.SetActive (false);
	
	}
}

