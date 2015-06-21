using UnityEngine;
using System.Collections;

public class _ChromaticTest : MonoBehaviour
{
	public ChromaticEffect effect;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			effect.forces.Add(new PPE_Force(pos.x,pos.y,1,3));
			//Debug.Log(pos + " : "  + ChromaticEffectManager.forces.Count	);
		}
		
	}
}

