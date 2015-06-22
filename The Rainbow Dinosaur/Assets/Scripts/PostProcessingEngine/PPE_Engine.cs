using UnityEngine;
using System.Collections.Generic;

public class PPE_Engine : MonoBehaviour
{

	public delegate void D_Effect00(Vector3 position);
	public delegate void D_NewForce(PPE_Force force);
	public delegate void D_NewChromaticObjects(List<PPE_Object> objects);
	public static D_Effect00 E_Effect00 = delegate {};
	public static D_NewForce E_NewForce = delegate {};
	public static D_NewChromaticObjects E_NewChromaticObjects = delegate {};

	public PPE_Particle P_Particle;
	public ChromaticAberration effectChromatic;
	public ColorDeteriorate effectDeteriorate;


	public List<Sprite> particleSprites;

	float powerAll = 0;
	float powerEffect00 = 0;
	int particleId = 0;
	float powerDeteriorate = 0;

	List<PPE_Object> objects;
	List<PPE_Particle> particles;
	List<PPE_Force> forces;
	// Use this for initialization
	void Awake(){
		PPE_Engine.E_NewChromaticObjects = H_NewChromaticObjects;
		PPE_Engine.E_Effect00 = H_Effect00;
		PPE_Engine.E_NewForce += H_NewForce;
		effectChromatic.Init ();
		effectDeteriorate.Init ();
		objects = new List<PPE_Object> ();
		particles = new List<PPE_Particle> ();
		forces = new List<PPE_Force> ();
		for (int i = 0; i < 30; i++) {
			var p =  Instantiate(P_Particle);
			particles.Add(p);
		}

	}
	void H_Effect00(Vector3 position ){
		IncreasePower (.2f);
		particleId = (particleId + 1) % particles.Count;
		Color color = new Color (.5f +Random.Range(-1.0f,1.0f)*(powerEffect00*.005f),
		                         .5f +Random.Range(-1.0f,1.0f)*(powerEffect00*.005f),0,.1f);
		int camLayer = effectChromatic.camPairs[Random.Range (0, effectChromatic.camPairs.Count)].cameraLayer;
		particles [particleId].On (
			particleSprites[Random.Range(0,particleSprites.Count)],
			camLayer,
			position, color,(int)(  10+ powerEffect00*20 	));
	}
	void H_NewChromaticObjects(List<PPE_Object> objects){
		this.objects = objects;
	}
	void H_NewForce(PPE_Force force){
		IncreasePower (1.0f);
		this.forces.Add (force);
	}
	void Start ()
	{

	}
	void OnGUI(){
	}
	// Update is called once per frame
	public void KUpdate ()
	{
		effectDeteriorate.Update ();
		effectChromatic.UpdateAll ();
		for(int i = 0 ; i<objects.Count;i++){
			objects[i].Begin();
		}
		if (powerDeteriorate > 55.0f) {
			for(int i = 0 ; i<objects.Count;i++){
				objects[i].renderSprite.layer = effectDeteriorate.layer;
			}
			powerDeteriorate= 0;
			effectDeteriorate.Render();

		}

		for(int i = 0 ; i<objects.Count;i++){
			int camLayer = effectChromatic.camPairs[Random.Range (0, effectChromatic.camPairs.Count)].cameraLayer;
			objects[i].KUpdate(camLayer,forces);
		}
		effectChromatic.Render (0);
		effectChromatic.Render (1);
		effectChromatic.Render (2);

		for(int i = 0 ;i< objects.Count;i++){
			objects[i].End();
		}
		for (int i =  forces.Count-1; i>=0; i--) {
			if(--forces[i].turn<=0){
				forces.RemoveAt(i);
			}
		}
		UpdatePowers ();
	}
	void IncreasePower(float n){
		powerDeteriorate += n;
		powerAll += n;
		powerEffect00  = Mathf.Min(powerEffect00+ n* .1f, 2.0f);
	}
	void UpdatePowers(){
		powerDeteriorate = Mathf.Max (0, powerDeteriorate - 23.1f * Time.deltaTime);
		powerAll = Mathf.Max (0, powerAll*(1 - .3f * Time.deltaTime) - 200 * Time.deltaTime);
		powerEffect00 = Mathf.Max (0, powerEffect00 - 2f * Time.deltaTime);
	}
}

