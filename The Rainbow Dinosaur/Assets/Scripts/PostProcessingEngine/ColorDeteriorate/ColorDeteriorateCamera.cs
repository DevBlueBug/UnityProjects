using UnityEngine;
using System.Collections;

public class ColorDeteriorateCamera : MonoBehaviour
{
	public static RenderTexture texture;
	public static Material mat,matUpdate;
	// Use this for initialization
	void Start ()
	{
	
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest){
		Graphics.Blit (texture, dest, ColorDeteriorateCamera.mat);
	}
}

