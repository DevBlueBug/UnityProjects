using UnityEngine;
using System.Collections.Generic;

public class ChromaticApply : MonoBehaviour
{
	public Material material;
	public RenderTexture textureRed,textureGreen,textureBlue;
	// Use this for initialization
	void Start ()
	{
		material.SetTexture ("_distortionRed", textureRed);
		material.SetTexture ("_distortionGreen", textureGreen);
		material.SetTexture ("_distortionBlue", textureBlue);
	
	}
	
	// Update is called once per frame
	void OnRenderImage(RenderTexture src, RenderTexture dest){
		Graphics.Blit (src, dest, material);

	}
}

