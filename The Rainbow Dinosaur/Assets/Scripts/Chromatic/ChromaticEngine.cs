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
	public delegate void D_NewForce(ChromaticForce force);
	public delegate void D_NewChromaticObjects(List<ChromaticObject> objects);
	public static D_Effect00 E_Effect00 = delegate {};
	public static D_NewForce E_NewForce = delegate {};
	
	public static D_NewChromaticObjects E_NewChromaticObjects = delegate {};
	
	public Material 
		matApply, matClear,
		matClearTrace;
	public List<CameraPair> cameras;
	public List<Sprite> particleSprites;
	public CameraPair camTrace;

	float powerAll = 0;
	float powerEffect00 = 0;
	int particleAt = 0;
	List<ChromaticObject> objects;
	List<ChromaticParticle> particles;
	List<ChromaticForce> forces;
	// Use this for initialization
	void Awake(){
		ChromaticEngine.E_NewChromaticObjects = H_NewChromaticObjects;
		ChromaticEngine.E_Effect00 = H_Effect00;
		ChromaticEngine.E_NewForce += H_NewForce;
		objects = new List<ChromaticObject> ();
		particles = new List<ChromaticParticle> ();
		forces = new List<ChromaticForce> ();
		for (int i = 0; i < 30; i++) {
			var p = new GameObject("Particle " + i ).AddComponent<ChromaticParticle>();
			particles.Add(p);
		}

	}
	void H_Effect00(Vector3 position ){
		powerAll += 1.0f;
		powerEffect00  = Mathf.Min(powerEffect00+.01f, 2.0f);
		particleAt = (particleAt + 1) % particles.Count;
		Color color = new Color (.5f +Random.Range(-1.0f,1.0f)*(powerEffect00*.01f),
		                         .5f +Random.Range(-1.0f,1.0f)*(powerEffect00*.01f),0,.1f);
		particles [particleAt].On (
			particleSprites[Random.Range(0,particleSprites.Count)],
			cameras[particleAt%cameras.Count].layer,
			position, color,(int)( 1 + powerEffect00*20 	));
	}
	void H_NewChromaticObjects(List<ChromaticObject> objects){
		this.objects = objects;
	}
	void H_NewForce(ChromaticForce force){
		powerAll += 1.0f;
		this.forces.Add (force);
	}
	void Start ()
	{
		for (int i = 0; i < cameras.Count; i++) {
			var flagBefore = cameras[i].camera.clearFlags;
			cameras[i].camera.clearFlags = CameraClearFlags.Color;
			cameras[i].camera.Render();
			cameras[i].camera.clearFlags = flagBefore;
		}
	}
	int countClear = 0;
	// Update is called once per frame
	public void KUpdate ()
	{
		powerAll = Mathf.Max (0, powerAll*(1 - .3f * Time.deltaTime) - 200 * Time.deltaTime);

		//matApply.SetFloat ("_Time", Time.time);
		powerEffect00 = Mathf.Max (0, powerEffect00 - 2f * Time.deltaTime);
		//Debug.Log ("obejcts " + objects.Count + " " +forces.Count);
		for(int i = 0 ; i<objects.Count;i++){
			objects[i].Begin();
		}
		for (int i = 0; i< cameras.Count; i++) {
			Render(cameras[i]);
		}
		for(int i = 0 ;i< objects.Count;i++){
			objects[i].End();
		}
		for (int i =  forces.Count-1; i>=0; i--) {
			if(--forces[i].turn<=0){
				forces.RemoveAt(i);
			}
		}
		
		for (int i = 0; i < cameras.Count *   Mathf.Min(1,powerAll/10.3f)  ; i++) {
			Graphics.Blit(cameras[i].texture,cameras[i].texture,matClear);
		}

		Graphics.Blit (camTrace.texture, camTrace.texture, matClearTrace);
	}
	void Render(CameraPair cam){
		
		for(int i = 0 ; i<objects.Count;i++){
			objects[i].KUpdate(cam.layer,forces);
				
		}
		cam.camera.Render ();
	}
}

