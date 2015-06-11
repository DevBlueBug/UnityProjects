using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChromaticEffect : MonoBehaviour
{
	public Texture2D Example;
	public EasyCamera cam;
	public List<Color> colorsApplied;
	public Material 
		matApply,
		matChromatic,
		matClear;
	public RenderTexture textureHolder, textureApplied;

	public List<ChromaticObject> objects;

	internal List<ChromaticForce> forces = new List<ChromaticForce>();
	// Use this for initialization
	void Start ()
	{	
	
	}
	void OnGUI(){
		Graphics.DrawTexture (new Rect (0, 0 , Screen.width,Screen.height), textureApplied,matApply);
	}
	// Update is called once per frame
	void Update ()
	{
		Graphics.Blit (textureApplied, textureApplied, matClear);	
		if (forces.Count != 0)
			StartCoroutine (Cycle ());



		//RenderTexture.active = rt;                      //Set my RenderTexture active so DrawTexture will draw to it.
	
		//GL.PushMatrix ();                                //Saves both projection and modelview matrices to the matrix stack.
		//GL.LoadPixelMatrix (0, 512, 512, 0);            //Setup a matrix for pixel-correct rendering.

		//Draw my stampTexture on my RenderTexture positioned by posX and posY.
		//Graphics.DrawTexture (new Rect (posX - stampTexture.width / 2, (rt.height - posY) - stampTexture.height / 2, stampTexture.width, stampTexture.height), stampTexture);
		//RenderTexture.active = null;                    //De-activate my RenderTexture.
		//GL.PopMatrix ();                                //Restores both projection and modelview matrices off the top of the matrix stack.
	


		
	
	}
	void Render(
		List<ChromaticObject> chroObjects, List<ChromaticForce> forces,
		EasyCamera camera, Color color){
		//apply forces
		for (int i = 0; i < chroObjects.Count; i++) {
			for(int j = 0 ; j < forces.Count;j++)
				chroObjects[i].ApplyForce(forces[j]);
		}
		//then render the camera with the color
		camera.Render (textureHolder);
		matChromatic.color = color;
		Graphics.Blit (textureHolder, textureApplied, matChromatic);
	}
	IEnumerator Cycle(){
		//render the scene
		for (int i = 0; i< colorsApplied.Count; i++) {

			Render(objects,forces, cam, colorsApplied[i]);
		}
		//update the forces
		for (int i = forces.Count-1; i >=0; i--) {
			if(--forces[i].turn <=0 ) forces.RemoveAt(i);
		}
		//set the objects to end state
		for (int i  = 0; i< objects.Count; i++) {
			objects[i].End();
		}

		//finally apply the baked scene
		//then aplpy the end filter
		yield return 1;
	}
}

