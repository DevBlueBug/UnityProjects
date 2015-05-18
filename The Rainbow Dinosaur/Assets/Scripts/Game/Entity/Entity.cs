using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
	public delegate void D_Refreshed(Entity me, Room room);
	public enum KType{Enemy,Player,World, };
	public D_Refreshed E_Refreshed = delegate {};

	public KType meType;

	internal bool isAlive = true;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	public virtual void KUpdate (Room room)
	{
	
	}

	public void Refresh(Room room){
		E_Refreshed (this, room);
	}
	public void Kill(Room room){
		Destroy (this.gameObject);
	}
}

