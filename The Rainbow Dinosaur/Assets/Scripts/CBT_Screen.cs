using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class CBT_Screen : MonoBehaviour
{
	public List<Camera> cameras; 
	public Material mat;
	public RenderTexture textureScreen00,textureScreen01;
	// Update is called once per frame
	void Awake(){

	}
	void OnRenderImage (RenderTexture a, RenderTexture b)
	{
		//redraw a to b but with distorted view
		Graphics.Blit (textureScreen00, textureScreen01);
		Graphics.Blit (textureScreen00, b,mat);
		for (int i = 0; i < cameras.Count; i++) {
			cameras[i].targetTexture = textureScreen01;
		}
		var textureScreen02 = textureScreen00;
		textureScreen00 = textureScreen01;
		textureScreen01 = textureScreen02;
		// switch a to b

		//apply the 
		//render the current scene just as it is.

	}
}

