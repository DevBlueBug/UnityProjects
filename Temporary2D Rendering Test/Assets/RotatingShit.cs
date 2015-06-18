using UnityEngine;
using System.Collections;

public class RotatingShit : MonoBehaviour
{
	SpriteRenderer r;
	float length = 2.0f;
	// Use this for initialization
	void Start ()
	{
		r = GetComponent<SpriteRenderer> ();
	
	}

	float angle = 0;
	int count =0;
	// Update is called once per frame
	void Update ()
	{
		count++;
		if (count %50 ==0) {
			r.enabled = true;
			count=0;
		}
		if (count == 1) {
			r.enabled = false;
			//UnityEditor.EditorApplication.isPaused = true;
		}
		length += Random.Range (-.1f, .1f)*Time.deltaTime;
		length = Mathf.Max( .5f,(float) Mathf.Min (length, .3f));
		angle += Random.Range (6, 10.5f)*Time.deltaTime;
		transform.localPosition = 3*new Vector3 (Mathf.Cos(angle) ,Mathf.Sin(angle) ,0 ) * length;
		//r.color += new Color (Random.Range (-.1f, .1f), Random.Range (-.1f, .1f), Random.Range (-.1f, .1f))
		//	*1.5f * Time.deltaTime;

	
	}
}

