using UnityEngine;
using System.Collections;

public class EffectChromaticForce : MonoBehaviour
{
	public int interval,intervalMax;

	public float force,distance;
	public int count;

	// Use this for initialization
	void Start ()
	{
		if (interval == -1)
			interval = Random.Range (0, intervalMax);
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ((interval = (interval + 1) % intervalMax) != 0) {
			//Debug.Log(interval + " AND " + (interval%intervalMax));
			return;
		}
		//Debug.Log(this.gameObject.name + " " + interval + " AND " + (interval%intervalMax));
		ChromaticEffect.E_NewForce (new PPE_Force (this.transform.position.x ,this.transform.position.y,force,distance,count)) ;
	
	}
}

