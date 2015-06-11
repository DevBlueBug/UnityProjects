using UnityEngine;
using System.Collections.Generic;

public class RenderRoom : MonoBehaviour
{
	List<ChromaticObject> chroObjects = new List<ChromaticObject>();
	// Use this for initialization
	void Awake(){
		var room = this.GetComponent<Room> ();
		if (room == null) {
			Destroy(this);
			return;
		}
		room.E_On += H_On;
		room.E_EntityAdded += H_EntityAdded;
		room.E_EntityDeleted += H_EntityAdded;
	}
	void H_On(Room room){
		ChromaticEffect.E_NewChroObjects (chroObjects);
	}
	void H_EntityAdded(Room room, Entity entity){
		var chroObject = entity.GetComponent<ChromaticObject> ();
		if (chroObject == null)
			return;
		chroObjects.Add (chroObject);
		entity.E_Kill += delegate(Entity me){
			H_EntityDeleted(null,me);
	
		};
	}
	void H_EntityDeleted(Room room, Entity entity){
		var chroObject = entity.GetComponent<ChromaticObject> ();
		if (chroObject == null)
			return;
		try{
			chroObjects.Remove(chroObject);
		}
		catch{
			Debug.Log("ChroObjectRemoveFailure");
		}
	}
}

