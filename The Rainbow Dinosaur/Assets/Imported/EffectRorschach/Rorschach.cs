using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Rorschach : MonoBehaviour {
	public Camera cam;
	public RenderTexture texture00,texture01;
	public Material matNoise,matBlur,matApply;

	[Range(0,99999)]
	public float frequency;
	public float seed;
	// Use this for initialization
	void Awake () {
		if(texture01==null)texture01 = new RenderTexture (texture00.width, texture00.height, 25, texture00.format);
	
	}
	public void Refresh(){
		Debug.Log ("refreshing");
		matNoise.SetFloat ("_Frequency", frequency);
		matNoise.SetFloat ("_Seed", seed);
	}
	int count = 0;
	// Update is called once per frame
	void Update () {
		if (count++ > 1) {
			//seed+= .001f;
			//count=0;
			//Refresh();
		}
		Graphics.Blit (texture00, texture01, matNoise);
		Graphics.Blit (texture01, texture00, matBlur);

		//Graphics.Blit (texture00, (RenderTexture)null,matApply);
		var texture02 = texture01;
		//texture01 = texture00;
		//texture00 = texture02;
		//cam.targetTexture = texture00;
	
	}
}
