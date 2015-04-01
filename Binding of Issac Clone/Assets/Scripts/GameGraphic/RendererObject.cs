using UnityEngine;
using System.Collections;

public class RendererObject : MonoBehaviour
{
	public Vector3 Position { 
		get { return transform.localPosition; } 
		set { transform.localPosition = value; } 
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	public void Destroy(){
		GameObject.Destroy (this.gameObject);
	}
}

