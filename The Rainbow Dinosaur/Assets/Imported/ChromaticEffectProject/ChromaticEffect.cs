using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChromaticEffect : MonoBehaviour
{
	public delegate void D_NewChroOjbects(List<ChromaticObject03> list);
	public delegate void D_NewFrce(ChromaticForce force);
	public static D_NewChroOjbects E_NewChroObjects = delegate{};
	public static D_NewFrce E_NewForce = delegate{};

	public Utility.EasyCamera cam;
	List<Color> colorsApplied;
	public List<Color> colorsApplied00;
	public List<Color> colorsApplied01;
	public List<Color> colorsApplied02;
	public Material 
		matApply,
		matChromatic,
		matClear;
	public RenderTexture textureHolder, textureApplied;

	public List<ChromaticObject03> objects;
	public static List<ChromaticObject03> objectsGlobal = new List<ChromaticObject03> ();

	internal List<ChromaticForce> forces = new List<ChromaticForce>();
	// Use this for initialization
	void Awake ()
	{
		ChromaticEffect.E_NewChroObjects += H_NewChroOjbects;
		ChromaticEffect.E_NewForce += H_NewForce;
	
	}
	void H_NewChroOjbects(List<ChromaticObject03> list){
		this.objects = list;
	}
	void H_NewForce(ChromaticForce force){
		this.forces.Add (force);
		//	Debug.Log ("newForceAdded");
	}
	bool isRendered = true;
	void OnGUI(){
		isRendered = true;
		//Graphics.DrawTexture (new Rect (0, 0 , Screen.width,Screen.height), textureApplied,matApply);
		//
	}
	void OnRenderImage( RenderTexture src,  RenderTexture dest)
	{
		//Debug.Log ("CALLED");
		// Copy the source Render Texture to the destination,
		// applying the material along the way.
		//Cycle ();
	}
	// Update is called once per frame
	public void KUpdate ()
	{
		
		return;
		List<List<Color>> dic = new List<List<Color>>(){
			colorsApplied00,colorsApplied01,colorsApplied02
		};
		var cTemp = dic [Random.Range (0, dic.Count)];
		var intList = new List<int> ();
		for (int i = 0; i < cTemp.Count; i++) {
			intList.Add(i);
		}
		colorsApplied = new List<Color> ();
		for (int i = intList.Count-1; i>=0; i--){
			int index = Random.Range(0,intList.Count);
			colorsApplied.Add(cTemp[index]);
			intList.RemoveAt(index);

		}
		//Cycle ();

		if (true || forces.Count != 0) {
			StartCoroutine(Cycle());
		}
		//Graphics.Blit (textureApplied, textureApplied, matClear);	



		//RenderTexture.active = rt;                      //Set my RenderTexture active so DrawTexture will draw to it.
	
		//GL.PushMatrix ();                                //Saves both projection and modelview matrices to the matrix stack.
		//GL.LoadPixelMatrix (0, 512, 512, 0);            //Setup a matrix for pixel-correct rendering.

		//Draw my stampTexture on my RenderTexture positioned by posX and posY.
		//Graphics.DrawTexture (new Rect (posX - stampTexture.width / 2, (rt.height - posY) - stampTexture.height / 2, stampTexture.width, stampTexture.height), stampTexture);
		//RenderTexture.active = null;                    //De-activate my RenderTexture.
		//GL.PopMatrix ();                                //Restores both projection and modelview matrices off the top of the matrix stack.
	



	
	}
	void Render(
		List<ChromaticForce> forces,
		Utility.EasyCamera camera, Color color){
		//apply forces
		for (int i = 0; i < objects.Count; i++) {
			objects[i].ApplyForce(forces);
		}
		for (int i = 0; i < objectsGlobal.Count; i++) {
			objectsGlobal[i].ApplyForce(forces);
		}

		//then render the camera with the color
		camera.Render (textureHolder);
		matChromatic.color = color;
		Graphics.Blit (textureHolder, textureApplied, matChromatic);
	}
	IEnumerator Cycle(){
		
		if (isRendered) {
			Graphics.Blit (textureApplied, textureApplied, matClear);	
			isRendered = false;
		}

		//render the scene
		for (int i = 0; i< colorsApplied.Count; i++) {
			
			Render(forces, cam, colorsApplied[i]);
		}
		//update the forces
		for (int i = forces.Count-1; i >=0; i--) {
			if(--forces[i].turn <=0 ) forces.RemoveAt(i);
		}
		//set the objects to end state
		for (int i  = 0; i< objects.Count; i++) {
			objects[i].End();
		}
		for (int i  = 0; i< objectsGlobal.Count; i++) {
			objectsGlobal[i].End();
		}

		//finally apply the baked scene
		//then aplpy the end filter
		yield return 1;
	}
}

