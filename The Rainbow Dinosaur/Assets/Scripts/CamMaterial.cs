using UnityEngine;
using System.Collections;

public class CamMaterial : MonoBehaviour
{
	public Material mat;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	void OnRenderImage(RenderTexture src,RenderTexture dest) {
		Graphics.Blit(src, dest, mat);

	}
}

