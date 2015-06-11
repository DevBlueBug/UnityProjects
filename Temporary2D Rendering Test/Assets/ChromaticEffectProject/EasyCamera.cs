using UnityEngine;
using System.Collections;

public class EasyCamera : MonoBehaviour
{
	public Camera cam;

	public RenderTexture Render(RenderTexture texture){
		cam.targetTexture = texture;
		cam.Render ();
		return texture;
	}
}

