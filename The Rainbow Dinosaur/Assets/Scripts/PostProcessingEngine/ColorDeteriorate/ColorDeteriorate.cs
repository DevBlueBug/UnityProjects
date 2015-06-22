using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorDeteriorate: PPE_Effect
{
	
	public Camera cam;
	public int layer;
	public RenderTexture texture,textureCopy;
	public Material matApply,matUpdate,matClear;
	// Use this for initialization
	public void Init ()
	{	
		ColorDeteriorateCamera.texture = texture;
		ColorDeteriorateCamera.mat = matApply;
		ColorDeteriorateCamera.matUpdate = matUpdate;
		Utility.EasyCamera.Clear (cam, texture);
		textureCopy = new RenderTexture (texture.width, texture.height,24,texture.format);
	
	}

	public void Update(){
		//var t = RenderTexture.GetTemporary (texture.width, texture.height,24,RenderTextureFormat.ARGBFloat);
		Graphics.Blit (texture, textureCopy, matUpdate);
		var temporary = texture;
		texture = textureCopy;
		textureCopy = temporary;
	}
	public void Render(){
		Debug.Log ("DETERIORATE RENDER");
		Render (cam, texture);
	}
}

