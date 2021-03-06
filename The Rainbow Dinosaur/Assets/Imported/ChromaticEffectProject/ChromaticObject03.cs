using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChromaticObject03 : MonoBehaviour
{
	public bool isGlobal = false,isAlwaysOn;
	public float power;
	Vector3? posInit = null;
	// Use this for initialization

	void Awake(){
		if (isGlobal) {
			//Debug.Log(counttt++);
			//ChromaticEffect.objectsGlobal.Add(this);
		}
	}
	void OnDestroy(){
		if (isGlobal) {
			//ChromaticEffect.objectsGlobal.Remove(this);
		}
	}
	void H_PrepareRender(){
		//Debug.Log ("prepare " + ChromaticEffectManager.forces.Count);


	}
	void H_FinishRender(){
		//this.transform.position = posInit.Value;
		//posInit = null;
	}
	void OnPostRender (){
		//Debug.Log ("POST RENDER");
	}
	public void ApplyForce(List<PPE_Force> forces){
		bool isApplied = false;
		if (isAlwaysOn) {
			//this.gameObject.SetActive (true);
			ApplyForce(new PPE_Force(this.transform.localPosition.x,this.transform.localPosition.y,power*Random.Range(.3f,1.0f),1.0f));
			return;
		}
		for (int i = 0; i < forces.Count; i++) {
			if(ApplyForce(forces[i])) isApplied = true;
		}
		this.gameObject.SetActive (isApplied);
	}
	bool ApplyForce(PPE_Force force){
		if (posInit == null) posInit = transform.position;
		var angle02 = Random.Range (0, 6.28f);
		var dis = posInit.Value - (force.position + new Vector3( Mathf.Cos(angle02) , Mathf.Sin(angle02),0) *.01f) ;
		var distance = dis.magnitude;
		//var forceSqrt = force.distance * force.distance;
		if (distance > force.distance) {
			return false;
		}
		
		var dir = dis.normalized;
		//float angle = Random.Range (0, 6.28f);
		//var dir = new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0);
		var ratio = (1 - distance / force.distance) * Random.Range (.01f, 1.0f);
		if (Random.Range (0, 3) == 0)
			ratio *= -1;
		//Debug.Log (ratio +" " + this.transform.position + " " +
		 //          ( dir*ratio*force.magnitude));
		//Debug.Log(dir*ratio*force.magnitude);
		this.transform.position += dir*ratio*force.magnitude;
		return true;
	}
	public void End(){
		this.transform.gameObject.SetActive(true);
		if (posInit != null) {
			this.transform.position = posInit.Value;
			posInit = null;
		}
	}
}

