using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Data;

public class Room : MonoBehaviour
{
	public delegate void D_Me(Room me);
	public delegate void D_NextRoom(int n);
	public delegate void D_MeEntity(Room me, Entity entity);
	public delegate EBullet D_RequireBullet(Entity entityRequesting, List<Entity.KType> targets);
	public delegate EItem D_GetItem(NItem.Item item);
	public static EItemManager P_ItemManager;

	internal D_NextRoom	E_NextRoom = delegate {};
	internal D_Me		E_On = delegate {};
	internal D_MeEntity	
	E_EntityAdded = delegate {},
	E_EntityDeleted = delegate {	};

	
	public DRoom.KType type = DRoom.KType.Normal;
	public int posX,posY;
	public float width,height;
	public Entity[,] entitiesWorldIndex;
	public List<Entity> entities,entitiesWorld; 

	internal EDoor[] entityDoors;
	internal AStar.KMap aStarMap;
	// Use this for initialization
	void Awake ()
	{
		entitiesWorldIndex = new Entity[(int)width,(int)height];
		entityDoors = new EDoor[]{null,null,null,null};
	}
	public void KUpdate(){
		bool isSomethingDead = false;
		isSomethingDead = UpdateEntities (entities);
		isSomethingDead = UpdateEntities (entitiesWorld) || isSomethingDead;

		//SetAllDoors(true	);
		if (isSomethingDead) {
			if(IsGameOver())SetAllDoors(true);
		}
	}
	public bool UpdateEntities(List<Entity> entities){
		bool isSomethingDead = false;
		for (int i = 0; i< entities.Count; i++) {
			var entity = entities[i];
			if(!entities[i].isAlive){
				if(entities[i].meType != Entity.KType.Bullet) isSomethingDead = true;
				UnWeightOnAStartMap(entity,(int)entity.posX,(int)entity.posY);
				entity.Terminate();
				entities.RemoveAt(i);
				continue;
			}
			entities[i].KUpdate(this);
		}
		return isSomethingDead;
	}
	public void On(){
		E_On (this);
		Debug.Log (" I A M O N ");	
		this.gameObject.SetActive (true);
		SetAllDoors ( IsGameOver ());
		for(int i = 0; i < entities.Count;i++){
			entities[i].Init();
		}
		for(int i = 0; i < entitiesWorld.Count;i++){
			entitiesWorld[i].Init();
		}
	}
	public void Off(){
		this.gameObject.SetActive (false);
	}

	
	public void Reset(RoomAsset asset, Data.Board data){
		aStarMap = new AStar.KMap ((int)width,(int)height);
		for (int i = 0; i< width; i++) {
			AddEntity(asset.Get(Piece.KId.Edge) ,i,0,true);
			AddEntity(asset.Get(Piece.KId.Edge) ,i,(int)height-1,true);
			
		}
		for(int j = 0 ; j<height;j++) {
			AddEntity(asset.Get(Piece.KId.Edge) ,0,j,true);
			AddEntity(asset.Get(Piece.KId.Edge) ,(int)width-1,j,true);
		}
		if (data == null) {
			
			AddGround (asset);
			Refresh ();
			return;
		}
		foreach (var piece in  data.piecesWorld) {
			AddEntity(asset.Get(piece.meType), piece.X,piece.Y,true);
		}
		foreach (var piece in  data.piecesUnits) {
			AddEntity(asset.Get(piece.meType), piece.X,piece.Y,false);
		}
		AddGround (asset);

		Refresh ();
	}
	public void AddGround(RoomAsset asset){
		for (int i = 1; i< width-1; i++) for (int j = 1; j<height-1; j++) {
			if(entitiesWorldIndex[i,j] == null){
				AddEntity(asset.Get(Piece.KId.Ground), i,j,true);
			}
		}
	}
	
	public void Refresh(){
		for (int i = 0; i< width; i++)
		for (int j = 0; j < this.height; j++) {
			var entity = entitiesWorldIndex[i,j];
			if(entity!=null)entity.Refresh(this);
		}
	}
	
	public void Kill(){
		Destroy (this.gameObject);
	}

	public void AddDoor(Entity entity, int doorIndex){
		((EDoor) entity).E_TriggerBack += delegate(Entity door,Entity doorEntered, Collider2D collider) {
			E_NextRoom (doorIndex);
			return 1;
		};

		var doorPosition = GetDoorPosition (doorIndex);
		entityDoors [doorIndex] = (EDoor) entity;
		AddEntity (entity, (int)doorPosition.x, (int)doorPosition.y , true);
		if (doorPosition.x == (int)width / 2) {
			RemoveEntityWorld ((int)doorPosition.x - 1, (int)doorPosition.y, true);
			RemoveEntityWorld ((int)doorPosition.x + 1, (int)doorPosition.y, true);
		} else {
			RemoveEntityWorld ((int)doorPosition.x , (int)doorPosition.y-1, true);
			RemoveEntityWorld ((int)doorPosition.x , (int)doorPosition.y+1, true);
		}
	}
	public void AddItemDrop(NItem.Item item, float x, float y){
	}
	public void AddEntity(Entity entity, float x, float y, bool isWorld){
		E_EntityAdded (this, entity);
		entity.transform.localPosition = new Vector3(x,y,0);


		int indexX = Mathf.RoundToInt(x),
			indexY = Mathf.RoundToInt(y);
		float _w = 1.0f / (width-1), _h = 1.0f / (height-1);
		float sizeCell = (_w < _h )? _w : _h;
		var pos  = new Vector3 (x, y, 0);
		pos.Scale (new Vector3 (sizeCell, sizeCell, 1));

		entity.transform.parent = this.transform;
		entity.transform.localScale = Vector3.one;
		AddEventHandlers (entity);
		WeightOnAStartMap(entity,indexX,indexY);

		if (isWorld) {
			if (entitiesWorldIndex [indexX, indexY] != null) {
				E_EntityDeleted (this, entitiesWorldIndex [indexX, indexY]);
				entitiesWorldIndex [indexX, indexY].Kill();
				entitiesWorldIndex [indexX, indexY].Terminate();
				entitiesWorld.Remove(entitiesWorldIndex [indexX, indexY]);
				//aStarMap.Reset(indexX,indexY);
			}
			//Debug.Log("ADD ENTITY WORLD : " + entity.meId);
			entitiesWorld.Add(entity);
			entitiesWorldIndex [indexX, indexY] = entity;
		} else {
			entities.Add(entity);
		}
		entity.Refresh (this);
	}
	public void RemoveEntityWorld(int x, int y,bool isKill){
		try{
			var entity = entitiesWorldIndex[x,y];
			entitiesWorld.Remove(entity);
			entitiesWorldIndex[x,y] = null;
			if(isKill) entity.Terminate();
		}
		catch{
			Debug.Log("Room Remove Entity call has failed.");
		}
	}
	public void RemoveEntity(Entity entity, bool isKill){
		try{
			entities.Remove (entity);
			if(isKill)entity.Terminate();
		}
		catch{
			Debug.Log("Room Remove Entity call has failed.");
		}

	}
	
	public void SetPosition(int x, int y){
		this.posX = x;
		this.posY = y;
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

	void AddEventHandlers(Entity entity)
	{
		entity.E_Kill += H_Kill;
		entity.E_NewEntity += H_NewEntity;
	}
	void SetAllDoors(bool open){
		for (int i = 0; i < 4; i++)
			if(entityDoors [i] !=null)entityDoors [i].SetOpen (open);
	}
	bool IsGameOver(){
		for (int i = 0; i< entities.Count; i++) {
			if(entities[i].isAlive&& entities[i].meType == Entity.KType.Enemy)
				return false;
		}
		return true;
	}
	void WeightOnAStartMap(Entity entity, int x, int y){
			
		if(entity.id == Data.Piece.KId.Edge ||
		   entity.id == Data.Piece.KId.Empty ||
		   entity.id == Data.Piece.KId.Block_Soft ||
		   entity.id == Data.Piece.KId.Block_Hard 
		   ) aStarMap [x, y].isAlive = false;
	}
	void UnWeightOnAStartMap(Entity entity, int x, int y){
		//aStarMap.Reset (x,y);
		if (entity.id == Data.Piece.KId.Edge ||
			entity.id == Data.Piece.KId.Empty ||
			entity.id == Data.Piece.KId.Block_Soft ||
			entity.id == Data.Piece.KId.Block_Hard 
		   ) {
			aStarMap.Reset(x,y);
		}
	}
	void H_NewEntity(Entity original, Entity newOne){
		AddEntity (newOne,newOne.transform.localPosition.x,newOne.transform.localPosition.y,false);
	}
	void H_Kill(Entity entity){
		var item = GetDrops (entity);
		if (item != null) {
			for(int i = 0 ; i < item.Count;i++){
				if(item[i] ==null) continue;
				AddEntity (item[i],item[i].transform.localPosition.x,item[i].transform.localPosition.y, false);
			}
		}
	}
	void H_DoorEntered(int n , Entity door, Entity doorEntered){
		if (doorEntered.meType == Entity.KType.Player)
			E_NextRoom (n);
	}
	public virtual List<EItem> GetDrops(Entity entity){
		List<EItem> items = new List<EItem> ();
		var inventory = entity.GetInventory ();
		if (entity.meType == Entity.KType.Enemy) {
			foreach (var entry in inventory.items) {
				items.Add(P_ItemManager.GetDrop(entry.Value));

				//items.Add(E_GetItemDrop(entry.Value)) ;
			}
			var money = P_ItemManager.GetMoney(Random.Range(0,10) );
			foreach(var m in money)
				items.Add(m);
			
			items.Add(P_ItemManager.GetDrop(NItem.Item.Get(NItem.Item.KId.Equip_Head_Kaonash) ));
			//var gold = E_GetItemDrop (new NItem.Item (NItem.Item.KType.Money,1 ));
			//gold.transform.position = entity.transform.position;

			//	items.Add (gold);
		}
		foreach (var i in items) {
			i.transform.position = entity.transform.position + new Vector3(Random.Range(-1,.1f),Random.Range(-1,.1f),0);

		}
		if (entity.meType == Entity.KType.Enemy) {
			
		}
		return items;
	}



}

