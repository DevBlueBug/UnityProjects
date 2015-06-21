using UnityEngine;
using System.Collections;

public class CamMaterial : MonoBehaviour
{
	public Camera cam;
	public Material mat;
	// Use this for initialization
	void Start ()
	{
	
	}
	public void Render(){
		cam.Render ();
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest){
		Graphics.Blit (src, dest, mat);
	}
}

