using UnityEngine;
using System.Collections;
namespace Utility
{
public class EasyCamera : MonoBehaviour
{
	public Camera cam;
	public RenderTexture appliedTexture;


	public RenderTexture Render(RenderTexture texture){
			//Debug.Log ("RENDER");
			var before = cam.targetTexture;
		//cam.targetTexture = texture;
		cam.Render ();
		//	cam.targetTexture = before;
		return texture;
	}
}
}
