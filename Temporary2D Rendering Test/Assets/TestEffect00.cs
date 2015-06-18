using UnityEngine;
using System.Collections;

public class TestEffect00 : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		ColorDistortionParticleEngine.E_Effect00 (this.transform.position);
	
	}
}

