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
		Debug.Log ("MAT " + mat);
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
		if (--count <= 0)
			this.gameObject.SetActive (false);
	
	}
}

