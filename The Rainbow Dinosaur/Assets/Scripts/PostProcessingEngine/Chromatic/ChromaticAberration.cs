using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ChromaticAberration
{
	[System.Serializable]
	public class CameraPairs
	{
		public int cameraLayer;
		public RenderTexture texture;
	}
	public Camera cam;
	public Material matClear, matApply;
	public List<CameraPairs> camPairs;
	public int TextureCount{
		get{
			return camPairs.Count;
		}
	}
	public void Init(){
		ChromaticAberrarionCamera.material = matApply;
		
		matApply.SetTexture ("_distortionRed", camPairs[0].texture );
		matApply.SetTexture ("_distortionGreen", camPairs[1].texture);
		matApply.SetTexture ("_distortionBlue", camPairs[2].texture);
	

		for (int i = 0; i < camPairs.Count; i++) {
			Utility.EasyCamera.Clear (cam, camPairs[i].texture);
		}

	}
	public void RenderAll(){
		for(int i = 0 ; i < camPairs.Count;i++){
			Render(i);
		}

	}
	public void Render(params int[] index){
		for(int i = 0 ; i< index.Length;i++){
			Render(index[i]);
		}

	}
	public void Render(int index){
		var texture =camPairs[index].texture;

		Graphics.Blit(texture,texture,matClear);
		
		cam.targetTexture = texture;
		cam.cullingMask = 1 << camPairs[index].cameraLayer;
		cam.Render();
	}
}

