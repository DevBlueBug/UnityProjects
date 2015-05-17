using UnityEngine;
using System.Collections.Generic;

public class EditorPiece_Bitmask : MonoBehaviour
{

	public EditorPiece entity;
	public bool isWorld;
	public List<Sprite> sprites; //sprite that will be patched 

	void Awake(){
		entity.E_Refreshed += H_Refreshed;
	}
	void H_Refreshed(EditorPiece entity, EditorBoard world){
		//Debug.Log ("REFEFRES");
		var map = world.entitiesWorld;
		var score = world.GetScore (map, 
		                Mathf.FloorToInt(entity.transform.localPosition.x), 
		                Mathf.FloorToInt(entity.transform.localPosition.y), 
		                entity.meType);
		//Debug.Log ("REFERSHED + " + score);
		entity.meRenderer.sprite = sprites [score];
	}
}

