using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraRenderHere : MonoBehaviour
{
	public RenderTexture texutre;
	public Material material;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	
	void OnRenderImage(RenderTexture src,RenderTexture dest) {
		Graphics.Blit(texutre, dest, material);
		
	}
}

