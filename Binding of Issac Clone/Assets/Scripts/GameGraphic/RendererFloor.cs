using UnityEngine;
using System.Collections;

public class RendererFloor : RendererObject
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	public void Display(int width, int height){
		Position = new Vector3 (width*.5f-.5f,-.5f, height*.5f-.5f);
		transform.localScale = new Vector3 (width,1,height );
	}
}

