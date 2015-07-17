using UnityEngine;
using System.Collections;

public class EReposion : MonoBehaviour
{
	public Entity entity;
	public SpriteRenderer sprRenderer;
	void Awake(){
		this.entity.E_Refreshed += H_Refresh;
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void H_Refresh (Entity entity,Room room)
	{
		int index = (int)(this.transform.localPosition.x + this.transform.localPosition.y);
		//sprRenderer.sortingOrder = index;
		//this.transform.localPosition += new Vector3(0,0,index);
	
	}
}

