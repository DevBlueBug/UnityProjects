using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Data;

public class Room : MonoBehaviour
{

	public float width,height;
	public Entity[,] entitiesWorldIndex;
	public List<Entity> entities,entitiesWorld; 
	// Use this for initialization
	void Awake ()
	{
		entitiesWorldIndex = new Entity[(int)width,(int)height];
	}
	public void KUpdate(){
		for (int i = 0; i< entities.Count; i++) {
			entities[i].KUpdate(this);
		}
	}
	public void Reset(RoomAsset asset){
		for (int i = 0; i< width; i++) {
			AddEntity(asset.Get(Piece.KType.Edge) ,i,0,true);
			AddEntity(asset.Get(Piece.KType.Edge) ,i,(int)height-1,true);
			
		}
		for(int j = 0 ; j<height;j++) {
			AddEntity(asset.Get(Piece.KType.Edge) ,0,j,true);
			AddEntity(asset.Get(Piece.KType.Edge) ,(int)width-1,j,true);
		}
		Refresh ();
	}
	
	public void Reset(RoomAsset asset, Data.Board data){
		for (int i = 0; i< width; i++) {
			AddEntity(asset.Get(Piece.KType.Edge) ,i,0,true);
			AddEntity(asset.Get(Piece.KType.Edge) ,i,(int)height-1,true);
			
		}
		for(int j = 0 ; j<height;j++) {
			AddEntity(asset.Get(Piece.KType.Edge) ,0,j,true);
			AddEntity(asset.Get(Piece.KType.Edge) ,(int)width-1,j,true);
		}
		for (int i  = 0; i < 4; i++) {
			var door = data.doors[i];
			if(door){
				var pos = GetDoorPosition(i);
				AddEntity(asset.Get(Piece.KType.Door),(int)pos.x,(int)pos.y,true);
			}
		}
		foreach (var piece in  data.piecesWorld) {
			AddEntity(asset.Get(piece.meType), piece.X,piece.Y,true);
		}
		for (int i = 0; i< width; i++) for (int j = 0; j<height; j++) {
			if(entitiesWorldIndex[i,j] == null){
				AddEntity(asset.Get(Piece.KType.Ground), i,j,true);
			}
			}
		Refresh ();
	}
	
	public void Refresh(){
		for (int i = 0; i< width; i++)
		for (int j = 0; j < this.height; j++) {
			var entity = entitiesWorldIndex[i,j];
			if(entity!=null)entity.Refresh(this);
		}
	}
	public void AddEntity(Entity entity, int x, int y, bool isWorld){
		float _w = 1.0f / (width-1), _h = 1.0f / (height-1);
		float sizeCell = (_w < _h )? _w : _h;
		var pos  = new Vector3 (x, y, 0);
		pos.Scale (new Vector3 (sizeCell, sizeCell, 1));

		entity.transform.parent = this.transform;
		entity.transform.localScale = Vector3.one;
		entity.transform.localPosition = new Vector3(x,y,0);

		if (isWorld) {

			entitiesWorld.Add(entity);
			if (entitiesWorldIndex [x, y] != null) {
				entitiesWorldIndex [x, y].Kill (this);
				entitiesWorld.Remove(entitiesWorldIndex [x, y]);
			}
			entitiesWorldIndex [x, y] = entity;
		} else {
			entities.Add(entity);
		}
	}
	public void RemoveEntity(Entity entity){
		try{
			entity.Kill (this);
			entities.Remove (entity);
		}
		catch{
			Debug.Log("Room Remove Entity call has failed.");
		}

	}
	
	public Vector3 GetDoorPosition(int n){
		List<Vector3> positions = new List<Vector3> (){
			new Vector3((int)width/2,height-1,0),
			new Vector3(width-1,(int)height/2,0),
			new Vector3((int)width/2,0,0),
			new Vector3(0,(int)height/2,0)
		};
		return positions [n];
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}

