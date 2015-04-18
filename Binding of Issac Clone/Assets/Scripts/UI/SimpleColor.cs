using UnityEngine;
using System.Collections;

public class SimpleColor : MonoBehaviour
{
	public Material myMaterial;
	public Color myColor;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	void OnInspectorGUI(){
		myMaterial.color = myColor;
	}
}

