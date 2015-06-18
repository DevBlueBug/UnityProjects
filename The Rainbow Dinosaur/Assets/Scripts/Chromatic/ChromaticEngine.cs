using UnityEngine;
using System.Collections.Generic;

public class ChromaticEngine : MonoBehaviour
{
	[System.Serializable]
	public class CameraPair{
		public Camera camera;
		public int layer;
		public RenderTexture texture;
	}
	public delegate void D_Effect00(Vector3 position);
	public delegate void D_NewChromaticObjects(List<ChromaticObject> objects);
	public static D_Effect00 E_Effect00 = delegate {};
	
	public static D_NewChromaticObjects E_NewChromaticObjects = delegate {};
	
	public Material matClear;
	public List<CameraPair> cameras;
	public List<Sprite> particleSprites;

	float powerEffect00 = 0;
	int particleAt = 0;
	List<ChromaticObject> objects;
	List<ChromaticParticle> particles;
	List<ChromaticForce> forces;
	// Use this for initialization
	void Awake(){
		ChromaticEngine.E_NewChromaticObjects = H_NewChromaticObjects;
		ChromaticEngine.E_Effect00 = H_Effect00;
		objects = new List<ChromaticObject> ();
		particles = new List<ChromaticParticle> ();
		for (int i = 0; i < 30; i++) {
			var p = new GameObject("Particle " + i ).AddComponent<ChromaticParticle>();
			particles.Add(p);
		}

	}
	void H_Effect00(Vector3 position ){
		powerEffect00 += .005f;
		particleAt = (particleAt + 1) % particles.Count;
		Color color = new Color (.5f +Random.Range(-1.0f,1.0f)*(powerEffect00*.001f),
		                         .5f +Random.Range(-1.0f,1.0f)*(powerEffect00*.001f), 0);
		particles [particleAt].On (
			particleSprites[Random.Range(0,particleSprites.Count)],
			cameras[particleAt%cameras.Count].layer,
			position, color,(int)( 10 + powerEffect00 * 10));
	}
	void H_NewChromaticObjects(List<ChromaticObject> objects){
		this.objects = objects;
	}
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		powerEffect00 = Mathf.Max (0, powerEffect00 - .1f * Time.deltaTime);
		if (Input.GetKeyDown (KeyCode.X)) {

		}
		
		for (int i = 0; i < cameras.Count; i++) {
			Graphics.Blit(cameras[i].texture,cameras[i].texture,matClear);
		}
		
		Debug.Log ("obejcts " + objects.Count);
		for(int i = 0 ; i<objects.Count;i++){
			objects[i].Begin();
		}
		for (int i = 0; i< cameras.Count; i++) {
			Render(cameras[i]);
		}
		for(int i = 0 ;i< objects.Count;i++){
			objects[i].End();
		}
	}
	void Render(CameraPair cam){
		
		for(int i = 0 ; i<objects.Count;i++){
			objects[i].KUpdate(cam.layer,forces);
				
		}
		cam.camera.Render ();
	}
}

