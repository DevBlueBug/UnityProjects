using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorDeteriorate
{
	
	public Camera cam;
	public int layer;
	public RenderTexture texture;
	public Material matApply,matUpdate,matClear;
	// Use this for initialization
	public void Init ()
	{	
		ColorDeteriorateCamera.texture = texture;
		ColorDeteriorateCamera.mat = matApply;
		Utility.EasyCamera.Clear (cam, texture);
	
	}

	public void Update(){
		Graphics.Blit (texture, texture, matUpdate);
	}
	public void Render(){
		Debug.Log ("DETERIORATE RENDER");
		cam.Render ();
	}
}

