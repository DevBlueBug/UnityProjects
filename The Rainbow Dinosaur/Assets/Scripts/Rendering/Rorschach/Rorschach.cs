using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Rorschach : MonoBehaviour {
	public delegate void D_NewSeed (float seed);
	public D_NewSeed E_NewSeed =delegate {};
	public Camera cam;
	public RenderTexture texture00,texture01;
	public Material matNoise,matBlur,matApply;

	[Range(0,99999)]
	public float frequency;
	public float seed;
	// Use this for initialization
	void Awake () {
		if(texture01==null)texture01 = new RenderTexture (texture00.width, texture00.height, 25, texture00.format);
		E_NewSeed += H_NewSeed;
	}

	public void Refresh(){
		Debug.Log ("refreshing");
		matNoise.SetFloat ("_Frequency", frequency);
		matNoise.SetFloat ("_Seed", seed);
	}
	// Update is called once per frame
	public void KUpdate () {
		Graphics.Blit (texture01, texture00, matNoise);
		//Graphics.Blit (texture01, texture00, matBlur);

		//Graphics.Blit (texture00, (RenderTexture)null,matApply);
		var texture02 = texture01;
		//texture01 = texture00;
		//texture00 = texture02;
		//cam.targetTexture = texture00;
	
	}
	void H_NewSeed(float seed){
		this.seed = seed;
		Refresh ();
	}
}
