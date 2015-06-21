using UnityEngine;
using System.Collections.Generic;

public class ColorDistortionParticleEngine : MonoBehaviour
{
	public delegate void D_Effect00(Vector3 pos);
	public static D_Effect00 E_Effect00 = delegate {};

	public int maxParticles;
	public Material mat;
	public List<Sprite> spriteTextures;
	public List<int> layers;

	int particleIndex = 0;
	int layerIndex = 0;
	List<ColorDistortionParticle> particles;
	void Awake(){
		E_Effect00 += this.Effet00;
		particles = new List<ColorDistortionParticle> ();
		for(int i = 0 ;  i< maxParticles;i++){
			var p = new GameObject("Particle " + i)
				.AddComponent<ColorDistortionParticle>();
			p.sprRenderer.material = mat;
			p.transform.localScale = new Vector3(2,2	,2);
			p.sprRenderer.sprite = spriteTextures[Random.Range(0,spriteTextures.Count)];
			particles.Add(p);

		}
	}
	// Use this for initialization
	void Start ()
	{

	
	}
	void CreateParticleAt(Vector3 at, Vector3 origin){
		layerIndex = (layerIndex+1) %layers.Count;
		particleIndex = (particleIndex + 1) % particles.Count;
		var particle = particles [particleIndex];
		float size = (at - origin).magnitude;
		float angle = Mathf.Atan2 (origin.y - at.y , origin.x - at.x) * (180/3.14f);

		particle.transform.localRotation = Quaternion.Euler (0,0,angle);
		particle.transform.position = at + (origin - at) * .5f;
		particle.transform.localScale = new Vector3 (Mathf.Max(1,size) ,1,1);

		var color = Camera.main.WorldToScreenPoint(origin) - Camera.main.WorldToScreenPoint(at);
		color = new Vector3 (.5f + color.x / Screen.width *.5f, .5f + color.y/Screen.height *.5f,color.z);
		particle.sprRenderer.color = new Color(color.x, color.y,0,1);
		particle.gameObject.layer = layers [layerIndex];
		particle.On ();

	}
	void Effet00(Vector3 position){

		for (int i = 0; i < 3; i++) {
			float angle = Random.Range (0, 6.3f);
			float distance = Random.Range (.1f, .5f);

		
			CreateParticleAt(position
		                 + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0) * distance
		                 , 
		                 position
		                 //Camera.main.ScreenToWorldPoint(Input.mousePosition)  
		                 );
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.Scale( new Vector3(1,1,0));
			Effet00(pos);
			for(int i = 0 ; i <3;i++){
			}
		}
	
	}
}

