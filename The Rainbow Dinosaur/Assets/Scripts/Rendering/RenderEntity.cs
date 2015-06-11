using UnityEngine;
using System.Collections;

public class RenderEntity : MonoBehaviour
{
	public bool isChromatic;
	// Use this for initialization
	public virtual void Awake ()
	{
		if(isChromatic) this.gameObject.AddComponent<ChromaticObject> ();
		var entity = GetComponent<Entity> ();


	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

