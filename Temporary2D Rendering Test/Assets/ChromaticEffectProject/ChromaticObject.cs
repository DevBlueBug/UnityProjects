using UnityEngine;
using System.Collections;

public class ChromaticObject : MonoBehaviour
{
	Vector3? posInit = null;
	// Use this for initialization

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
	public void ApplyForce(ChromaticForce force){
		if (posInit == null) posInit = transform.position;
		var dis = posInit.Value - force.position;
		var distanceSqrt = dis.sqrMagnitude;
		var forceSqrt = force.distance * force.distance;
		if (distanceSqrt > forceSqrt) {
			this.transform.gameObject.SetActive(false);
			return;
		}
		
		var dir = dis.normalized;
		float angle = Random.Range (0, 6.28f);
		//var dir = new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0);
		var ratio = (1 - distanceSqrt / forceSqrt) * Random.Range (-1, 1.0f);
		Debug.Log (ratio +" " + this.transform.position + " " +
		           ( dir*ratio*force.magnitude));
		//Debug.Log(dir*ratio*force.magnitude);
		this.transform.position = posInit.Value + dir*ratio*force.magnitude;
	}
	public void End(){
		this.transform.gameObject.SetActive(true);
		if (posInit != null) {
			this.transform.position = posInit.Value;
			posInit = null;
		}
	}
}

