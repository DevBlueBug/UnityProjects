using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ChromaticAberration : PPE_Effect
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
	List<RenderTexture> renderTextureCopy;
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
	

		renderTextureCopy = new List<RenderTexture> ();
		for (int i = 0; i < camPairs.Count; i++) {
			Utility.EasyCamera.Clear (cam, camPairs[i].texture);
			renderTextureCopy.Add(Utility.EasyRenderTexture.GetCopy(camPairs[i].texture));
			Utility.EasyCamera.Clear (cam, renderTextureCopy[i]);
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
	public void UpdateAll(){
		for (int i = 0; i < camPairs.Count; i++) {
			Update(i);
		}

	}
	public void Update(int index){
		var texture00 =camPairs[index].texture;
		var texture01 = renderTextureCopy [index];
		camPairs [index].texture = texture01;
		renderTextureCopy [index] = texture00;

		Graphics.Blit(texture00,texture01,matClear);

	}
	public void Render(int index){
		var texture =camPairs[index].texture;
		cam.targetTexture = texture;
		cam.cullingMask = 1 << camPairs[index].cameraLayer;
		cam.Render();
	}
}

