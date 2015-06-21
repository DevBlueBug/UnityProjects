using UnityEngine;
using System.Collections;

public class ChromaticAberrarionCamera : MonoBehaviour
{
	public static Material material;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	
	void OnRenderImage(RenderTexture src,RenderTexture dest) {
		Graphics.Blit(src, dest, material);
		
	}
}

