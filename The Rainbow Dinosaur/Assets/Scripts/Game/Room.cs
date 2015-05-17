using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{

	public float width,height;
	public Entity[,] entitiesWorld;
	// Use this for initialization
	void Awake ()
	{
		entitiesWorld = new Entity[(int)width,(int)height];
	}
	Vector3 GetDoorPosition(int n){
		List<Vector3> positions = new List<Vector3> (){
			new Vector3((int)width/2,height-1,0),
			new Vector3(width-1,(int)height/2,0),
			new Vector3((int)width/2,0,0),
			new Vector3(0,(int)height/2,0)
		};
		return positions [n];
	}
	public void Reset(RoomAsset asset){
		for (int i = 0; i< width; i++) {
			AddEntity(Instantiate(asset.EEdge) ,i,0,true);
			AddEntity(Instantiate(asset.EEdge) ,i,(int)height-1,true);
			
		}
		for(int j = 0 ; j<height;j++) {
			AddEntity(Instantiate(asset.EEdge) ,0,j,true);
			AddEntity(Instantiate(asset.EEdge) ,(int)width-1,j,true);
		}
	}
	
	public void Reset(RoomAsset asset, Data.DRoom data){
		for (int i = 0; i< width; i++) {
			AddEntity(Instantiate(asset.EEdge) ,i,0,true);
			AddEntity(Instantiate(asset.EEdge) ,i,(int)height-1,true);
			
		}
		for(int j = 0 ; j<height;j++) {
			AddEntity(Instantiate(asset.EEdge) ,0,j,true);
			AddEntity(Instantiate(asset.EEdge) ,(int)width-1,j,true);
		}
		for (int i  = 0; i < 4; i++) {
			var door = data.doors[i];
			if(door){
				var pos = GetDoorPosition(i);
				AddEntity(Instantiate(asset.EDoor),(int)pos.x,(int)pos.y,true);
			}
		}
	}
	void AddEntity(Entity entity, int x, int y, bool isWorld){
		float _w = 1.0f / (width-1), _h = 1.0f / (height-1);
		float sizeCell = (_w < _h )? _w : _h;
		var pos  = new Vector3 (x, y, 0);
		pos.Scale (new Vector3 (sizeCell, sizeCell, 1));

		entity.transform.parent = this.transform;
		entity.transform.localScale = Vector3.one;
		entity.transform.localPosition = new Vector3(x,y,0);

		if (isWorld) {
			if(entitiesWorld[x,y] !=null){
				entitiesWorld[x,y].Kill(this);
			}
			entitiesWorld[x,y] = entity;
		}
	}
	public void Refresh(){
		for (int i = 0; i< width; i++)
			for (int j = 0; j < this.height; j++) {
			var entity = entitiesWorld[i,j];
			if(entity!=null)entity.Refresh(this);
			}
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}

