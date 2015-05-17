using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
	public delegate void D_Refreshed(Entity me, Room room);
	public enum KType{Edge,Blcok };
	public D_Refreshed E_Refreshed = delegate {};

	public KType meType;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Refresh(Room room){
		E_Refreshed (this, room);
	}
	public void Kill(Room room){
		Destroy (this.gameObject);
	}
}

