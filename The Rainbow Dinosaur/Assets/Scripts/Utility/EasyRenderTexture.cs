using UnityEngine;
using System.Collections;

namespace Utility
{
	
	public class EasyRenderTexture : MonoBehaviour
	{
		public static RenderTexture GetCopy(RenderTexture texture){
			return new RenderTexture (texture.width, texture.height,24, texture.format);
		}
		
	}
	
	
}