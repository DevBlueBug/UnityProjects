using UnityEngine;
using System.Collections.Generic;

public class RenderEntity : MonoBehaviour
{
	public Entity entity;
	public List<RenderSprite> renderSprites; 
	public PPE_Object ppeObject;

	public virtual void Awake ()
	{



	}
	public virtual void Start(){
	}


	// Update is called once per frame
	public virtual void Update ()
	{
		//Debug.Log ("UPDATEEE");
	
	}
}

