using UnityEngine;
using System.Collections;

public class RenderEntity : MonoBehaviour
{
	public RenderSprite renderSprite; 
	public bool isChromatic, isChromaticSelfUpdate;

	internal PPE_Object chroObject;

	public virtual void Awake ()
	{
		if (isChromatic) {
			chroObject = GetComponent<PPE_Object>();
			if(chroObject == null){
				AwakeAddChromaticObject();
			}
		}


	}
	public virtual void AwakeAddChromaticObject(){
		
		chroObject = renderSprite.gameObject.AddComponent<PPE_Object>()
			.Init(renderSprite,isChromaticSelfUpdate);
	}


	// Update is called once per frame
	public virtual void Update ()
	{
		//Debug.Log ("UPDATEEE");
	
	}
}

