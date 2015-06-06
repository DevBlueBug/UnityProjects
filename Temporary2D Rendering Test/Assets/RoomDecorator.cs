using UnityEngine;
using System.Collections;

public class RoomDecorator : MonoBehaviour
{
	public SpriteRenderer sprRenderer;

	public Vector3 Bound {
		get{return sprRenderer.bounds.extents;}
	}

	// Use this for initialization
	void Start ()
	{
		if (this.transform.parent != null) {
			this.transform.localScale = Vector3.one;
		}
			//	Debug.Log (this.gameObject.name+ " " + sprRenderer.bounds);
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

