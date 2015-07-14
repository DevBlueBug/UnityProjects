using UnityEngine;
using System.Collections.Generic;

public class RenderRoom : MonoBehaviour
{
	public static int
		wallLayer,wallOrder, 
		wallShadowLayer,wallShadowOrder;
	public RenderRoomWalls P_RenderWalls;
	public RenderRoomFloor P_RenderFloor;

	List<PPE_Object> chroObjects = new List<PPE_Object>();
	List<ChromaticObject03> chroObjects03 = new List<ChromaticObject03>();
	// Use this for initialization
	void Awake(){
		var room = this.GetComponent<Room> ();
		if (room == null) {
			Destroy(this);
			return;
		}

		//var renderWall = Instantiate (P_RenderWalls);
		P_RenderWalls.Init (this.transform, wallLayer,wallOrder,wallShadowLayer,wallShadowOrder , (int)room.width,(int)room.height);
		P_RenderFloor.Init (this.transform, wallLayer, wallOrder - 1,wallShadowLayer,wallShadowOrder-1, room.width,room.height);

		room.E_On += H_On;
		room.E_EntityAdded += H_EntityAdded;
		room.E_EntityDeleted += H_EntityAdded;
	}
	void H_On(Room room){
		PPE_Engine.E_NewChromaticObjects (chroObjects);
	}
	void H_EntityAdded(Room room, Entity entity){
		var renderEntity = entity.GetComponent<RenderEntity> ();
		if (renderEntity == null) {
			return;
		}
		var chromaticObject = renderEntity.ppeObject;
		if (chromaticObject != null) {
			chroObjects.Add (chromaticObject);
			entity.E_Kill += delegate(Entity me){
				H_PPE_Object_Killed(null,me);
				
			};
		}
	}
	void H_PPE_Object_Killed(Room room, Entity entity){
		var chroObject = entity
			.GetComponent<RenderEntity> ().ppeObject;
		if (chroObject == null) return;
		try{ chroObjects.Remove(chroObject);}
		catch{ }
	}
}

