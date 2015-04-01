using UnityEngine;
using System.Collections;

public class EntitySwitch : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	void OnTriggerEnter(Collider c){
		Debug.Log (this +"ENTER!");
	}
	void OnTriggerExit(Collider c){
		Debug.Log (this +"EXIT!");
	}
}

