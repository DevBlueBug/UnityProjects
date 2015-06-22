using UnityEngine;
public class PPE_Effect {
	public void Render(Camera camera, RenderTexture texture){
		camera.targetTexture = texture;
		camera.Render ();
	}

}