using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorBrain : MonoBehaviour
{
	public static int id;
	//public enum TypeEdit{World,Block,Decoration,Enemy,Trap,Item };
	public EditorBoard board;
	public EditorBoard_Theme boardTheme;

	Data.Piece.KId meTypeEdit = Data.Piece.KId.Edge;
	int modeMoveEdit =0;

	Vector3 mousePosOld = Vector3.zero;

	public void Start(){

		Reset ();
	}

	public void Save ()
	{
		var content = Data.JsonWrapper.Wrap((Data.Board)board);
		Utility.EasyFile.Save (helperGetPath (id), content.ToString());
		Debug.Log ("SAVED " +  +id +" " + content.ToString() );
	}

	public void Reset(){
		var contnet = Utility.EasyFile.Load (helperGetPath (id));
		Data.Board data;
		Data.JsonWrapper.UnWrap (contnet, out data);

		board.Apply (boardTheme, data);
	}
	public void BoardClear(){
		board.Reset (boardTheme);
	}
	public void SetMode(int n ){
		modeMoveEdit = n;
		mousePosOld = Input.mousePosition;
	}

	public void E_SetEditType (Data.Piece.KId type)
	{
		Debug.Log ("SET " + type);
		meTypeEdit = type;

	}
	
	string helperGetPath(int number){
		return "Data/" + number + ".txt";
	}
	void SetSelectMode(int type){
	}
	public void E_Click(){
		if (modeMoveEdit == 1) {
			ClickEdit();
		}
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.A)) {
			board.Reset (boardTheme);
			Debug.Log(Data.JsonWrapper.Wrap((Data.Board) board).ToString());
		}
		if (modeMoveEdit == 0) {
			UpdateMove();
		}
		if (modeMoveEdit == 1) {
			//UpdateEdit();
		}
	}
	void UpdateMove(){
		var isButtonDown = Input.GetMouseButton (0);
		var mousePosNew = Input.mousePosition;
		if (isButtonDown) {
			var mousePosDiff = 
				Camera.main.ScreenToWorldPoint(mousePosOld) 
					- Camera.main.ScreenToWorldPoint(mousePosNew);

			Camera.main.transform.position += mousePosDiff;
			//Debug.Log(mousePosDiff);

		}
		mousePosOld = mousePosNew;
	}
	void ClickEdit(){
		var index = board.ToLocalIndex (Camera.main.ScreenToWorldPoint (Input.mousePosition));
		if (index == null) return;
		Debug.Log ("CLICK " + meTypeEdit);
		if (meTypeEdit == Data.Piece.KId.Edge) {
			
			if (index [0] == 0 || index [0] == board.width - 1)
				index [1] = 4;
			if (index [1] == 0 || index [1] == board.height - 1)
				index [0] = 7;
			var entity = board.entitiesWorld [index [0]] [index [1]];

			if(index[0] >= 1 && index[0] < board.width-1 && index[1] >= 1 && index[1] < board.height-1){
				
				if (entity.meType != Data.Piece.KId.Ground) {
					board.AddPiece (Instantiate( boardTheme.Get(Data.Piece.KId.Ground)), index [0], index [1], true);
				}else if (entity.meType == Data.Piece.KId.Ground) {
					board.AddPiece (Instantiate( boardTheme.Get(Data.Piece.KId.Empty)), index [0], index [1], true);
				}
				return;
			}
			else{

				if (entity.meType == Data.Piece.KId.Edge) {
					board.AddPiece (Instantiate(boardTheme.Get(Data.Piece.KId.Door)), index [0], index [1], true);
				} else if (entity.meType == Data.Piece.KId.Door) {
					board.AddPiece (Instantiate(boardTheme.Get(Data.Piece.KId.Edge)), index [0], index [1], true);
				} 
			}
		} else {
			if (board.entitiesUnits [index [0]] [index [1]] == null) {
				board.AddPiece (Instantiate (boardTheme.Get(meTypeEdit)), index [0], index [1], false);
			}
			else if (board.entitiesUnits [index [0]] [index [1]].meType != boardTheme.Get(meTypeEdit).meType){
				board.AddPiece (Instantiate (boardTheme.Get(meTypeEdit)), index [0], index [1], false);

			}
			else board.DeleteEntity (index [0], index [1], false);
		}
	}
}

