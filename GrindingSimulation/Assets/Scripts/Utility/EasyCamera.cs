using UnityEngine;
using System.Collections;
namespace Utility
{
	public class EasyCamera : MonoBehaviour
	{
		public Camera cam;
		public RenderTexture appliedTexture;
		public static void Clear(Camera cam, RenderTexture texture){
			
			var flagBefore = cam.clearFlags;
			var textureBefore = cam.targetTexture;
			cam.clearFlags = CameraClearFlags.Color;
			cam.targetTexture = texture;
			cam.Render();
			cam.clearFlags = flagBefore;
			cam.targetTexture = textureBefore;
		}


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
