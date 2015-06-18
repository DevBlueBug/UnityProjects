using UnityEngine;
using System.Collections;

public class RenderEntity : MonoBehaviour
{
	public RenderSprite renderSprite; 
	public bool isChromatic;

	internal ChromaticObject chroObject;

	public virtual void Awake ()
	{
		if (isChromatic) {
			chroObject = GetComponent<ChromaticObject>();
			if(chroObject == null){
				chroObject = this.gameObject.AddComponent<ChromaticObject>();
				chroObject.renderSprite = renderSprite;
			}
		}

	}

	// Update is called once per frame
	public virtual void Update ()
	{
		//Debug.Log ("UPDATEEE");
	
	}
}

