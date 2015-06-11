using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AlphaClear : MonoBehaviour
{
	public Camera cam;
	public Material alphaClearMaterial;

	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		
		//int rtW = source.width;
		//int rtH = source.height;
		//var color2A = RenderTexture.GetTemporary (rtW / 2, rtH / 2, 0, source.format);
		//chromaticAberrationMaterial.SetTexture ("_MainTex", source);
		
		Graphics.Blit (source, destination, alphaClearMaterial);
		
	}
}

