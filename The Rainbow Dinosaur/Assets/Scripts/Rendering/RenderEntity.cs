using UnityEngine;
using System.Collections;

public class RenderEntity : MonoBehaviour
{
	public RenderSprite renderSprite; 
	public bool isChromatic, isChromaticSelfUpdate;

	internal ChromaticObject chroObject;

	public virtual void Awake ()
	{
		if (isChromatic) {
			chroObject = GetComponent<ChromaticObject>();
			if(chroObject == null){
				AwakeAddChromaticObject();
			}
		}


	}
	public virtual void AwakeAddChromaticObject(){
		
		chroObject = renderSprite.gameObject.AddComponent<ChromaticObject>()
			.Init(renderSprite,isChromaticSelfUpdate);
	}


	// Update is called once per frame
	public virtual void Update ()
	{
		//Debug.Log ("UPDATEEE");
	
	}
}

