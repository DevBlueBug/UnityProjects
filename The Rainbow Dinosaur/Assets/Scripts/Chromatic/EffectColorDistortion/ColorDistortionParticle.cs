using UnityEngine;
using System.Collections;

public class ColorDistortionParticle : MonoBehaviour
{

	public SpriteRenderer sprRenderer;
	void Awake(){
		sprRenderer = this.gameObject.AddComponent<SpriteRenderer> ();
		this.gameObject.SetActive (false);

	}
	// Use this for initialization
	public void On ()
	{
		this.gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.gameObject.SetActive (false);
	
	}
}

