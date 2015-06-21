using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EffectColorDistortion : MonoBehaviour {
	public Texture2D texture1x1;
	public RenderTexture textureRed,textureGreen,textureBlue;
	public Material matEffect,mayClear,mayApply;
	// Use this for initialization
	void Start () {
		mayApply.SetTexture ("_distortionRed", textureRed);
		mayApply.SetTexture ("_distortionGreen", textureGreen);
		mayApply.SetTexture ("_distortionBlue", textureBlue);
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}
	void OnRenderImage(RenderTexture src,RenderTexture dest) {

		// Copy the source Render Texture to the destination,
		// applying the material along the way.\

		Profiler.BeginSample ("MyPieceOfCode");
		Graphics.Blit(src, dest, mayApply);
		Graphics.Blit(textureRed, textureRed, mayClear);
		Graphics.Blit(textureBlue, textureBlue, mayClear);
		Graphics.Blit(textureGreen, textureGreen, mayClear);
		// do something that takes a lot of time
		Profiler.EndSample ();

		//Graphics.Blit(textureRed, textureRed, mayClear);
		//mayClear.SetTexture ("_MainTex", textureRed);
		//RenderTexture.active = textureRed;
		//Graphics.Blit(textureRed, textureRed, mayClear);
		//Graphics.Blit(texture1x1, textureGreen, mayClear);
		//Graphics.Blit(texture1x1, textureBlue, mayClear);

		//RenederScene (textureRed, mayClear);
		//Graphics.Blit(textureGreen, textureGreen, mayClear);
		//Graphics.Blit(textureBlue, textureBlue, mayClear);
	}
	void RenederScene(RenderTexture texture, Material material){
		//Graphics.Blit(texture1x1, texture, material);



		//RenderTexture.active = texture;
		//Graphics.Blit (texture, material);
		//Graphics.DrawTexture (new Rect (0, 0, 1, 1),texture1x1, material);
		//Graphics.DrawTexture (new Rect (0, 0, 200, 300), texture1x1, material);
		//Graphics.DrawTexture(new Rect(0,0,100,100),texture1x1,material);

		//RenderTexture.active = null;
	}
}
