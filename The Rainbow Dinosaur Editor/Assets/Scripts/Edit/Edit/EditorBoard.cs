﻿using UnityEngine;
using System.Collections.Generic;

public class EditorBoard : MonoBehaviour {

	public EditorPiece entity;
	//public Dictionary<KEntity.KType,int> dic;
	internal int width = 15, height = 9;
	public static int[][] doorPositions = new int[][]{new int[]{7,8},new int[]{14,4},new int[]{7,0},new int[]{0,4}};
	public List<List<EditorPiece>> entitiesWorld,entitiesUnits;
	// Use this for initialization
	void Awake () {
		entitiesWorld = helperGetList (width, height);
		entitiesUnits = helperGetList (width, height);
	}
	List<List<EditorPiece>> helperGetList(int w, int h){
		List<List<EditorPiece>> l = new List<List<EditorPiece>>();
		for (int i = 0; i < w; i++) {
			l.Add(new List<EditorPiece>());
			for(int j = 0 ; j < h;j++){
				l[i].Add(null);
			}
		}
		return l;
	}
	bool helperIsIndexValid(int x, int y){
		return x >= 0  && x < width &&y >= 0&& y < height;
	}
	public void Reset(EditorBoard_Theme boardTheme){
		for (int x = 0; x < width; x++) {
			AddPiece(Instantiate (boardTheme.Get(EditorPiece.KType.Edge)),x,0,true);
			AddPiece(Instantiate (boardTheme.Get(EditorPiece.KType.Edge)),x,height-1,true);
		}
		for (int y= 1; y < height-1; y++) {
			AddPiece(Instantiate (boardTheme.Get(EditorPiece.KType.Edge)),0,y,true);
			AddPiece(Instantiate (boardTheme.Get(EditorPiece.KType.Edge)),width-1,y,true);
		}
		/**
		for (int x = 1; x< width-1; x++)
		for (int y= 1; y < height-1; y++) {
			AddPiece(Instantiate (boardTheme.Get(EditorPiece.KType.Ground)),x,y,true);
		}
		for (int i = 0; i < 4; i++) {
			AddPiece(Instantiate (boardTheme.Get(EditorPiece.KType.Door)),doorPositions[i][0],doorPositions[i][1],true);

		}
		**/
	}

	public void Apply (EditorBoard_Theme theme, EditorBoard_Data boardData)
	{
		Reset (theme);
		foreach (var piece in boardData.piecesWorld)
			AddPiece (Instantiate (theme.Get (piece.type)), piece.X, piece.Y, true);
		foreach (var piece in boardData.piecesUnits)
			AddPiece (Instantiate (theme.Get (piece.type)), piece.X, piece.Y, false);
		for (int i = 0; i < 4; i++) {
			if(boardData.doors[i])AddPiece(
				Instantiate(theme.Get(EditorPiece.KType.Door)),
				doorPositions[i][0],doorPositions[i][1],true);

		}
			  
		//throw new System.NotImplementedException ();
	}

	public int GetScore(List<List<EditorPiece>> map, int x, int y, EditorPiece.KType type){
		int score = 0;
		for (int i =0;i< 4; i++) {
			var d = KUtiltiy.dirs[i];
			float xNew = x + d.x, yNew = y + d.y;
			if(!helperIsIndexValid((int)xNew,(int)yNew))continue;
			var entity = entitiesWorld[(int)xNew][(int)yNew];
			if(entity == null) continue;
			if(entity.meType == type) score += KUtiltiy.maskValue[i];
		}
		//Debug.Log ("SCORE " + score);
		return score;
	}
	public int[] ToLocalIndex(Vector3 posWorld){
		var p  = posWorld - this.transform.position;
		//Debug.Log (p);
		int 
			x = Mathf.FloorToInt((p.x + transform.lossyScale.x*.5f)  / transform.lossyScale.x), 
			y = Mathf.FloorToInt((p.y + transform.lossyScale.y*.5f) / transform.lossyScale.y);
		if (!helperIsIndexValid (x, y)) return null;
		return new int[]{x,y};
	}
	public void DeleteEntity(int x, int y, bool isWorld){
		if (isWorld) {
			if (entitiesWorld [x] [y] != null)
				Destroy (entitiesWorld [x] [y].gameObject);
			entitiesWorld [x] [y] = null;
		} else {
			if (entitiesUnits [x] [y] != null)
				Destroy (entitiesUnits [x] [y].gameObject);
			entitiesUnits [x] [y] = null;
		}
		/**
		for (int i = 0; i <  entitiesUnits.Count; i++) {
			if((int)entitiesUnits[i].transform.localPosition.x == x &&
			   (int)entitiesUnits[i].transform.localPosition.y == y ){
				entitiesUnits.RemoveAt(i);
				break;
			}
		}
		**/
		Refresh ();
	}
	public void AddPiece(EditorPiece entity,int x, int y, bool isWorld){	
		//var entity = pack.Get(GetScore(x,y, pack.entity.meType ));
		entity.transform.parent = this.transform;
		entity.transform.localPosition = new Vector3 (x,y,0);
		entity.transform.localScale = Vector3.one;
		entity.transform.rotation = Quaternion.Euler( Vector3.zero);
		
		if (isWorld) {
			if (entitiesWorld [x] [y] != null)
				Destroy (entitiesWorld [x] [y].gameObject);
			entitiesWorld [x] [y] = entity;
			Refresh ();
		} else {
			if (entitiesUnits [x] [y] != null)
				Destroy (entitiesUnits [x] [y].gameObject);
			entitiesUnits [x] [y] = entity;
		}
	}
	public void Refresh(){
		for(int i = 0 ; i <width;i++)for(int j = 0 ; j < height;j++){
			if(entitiesWorld[i][j]!=null) entitiesWorld[i][j].Refresh(this);
			//if(entitiesUnits[i][j]!=null) entitiesUnits[i][j].Refresh(this);
		}
	}


	
	// Update is called once per frame
	void Update () {

	
	}
}
