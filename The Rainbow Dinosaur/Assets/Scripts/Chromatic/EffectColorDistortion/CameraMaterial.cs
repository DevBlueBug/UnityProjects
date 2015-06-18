using UnityEngine;
using System.Collections;

public class CameraMaterial : MonoBehaviour
{

	public Texture2D texture1x1;
	public Material material;
	public RenderTexture rendertexture;
	void OnPreRender(){
		//Debug.Log ("CAMERA MATERIAL ");
		material.SetTexture ("_SubTex", rendertexture);
	}
	void OnRenderImage(RenderTexture src,RenderTexture dest) {
		Graphics.Blit (rendertexture, rendertexture,material);
		//Graphics.Blit (src, dest);
	}
}

