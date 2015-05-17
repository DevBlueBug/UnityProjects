using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorBrain : MonoBehaviour
{
	public static int id;
	//public enum TypeEdit{World,Block,Decoration,Enemy,Trap,Item };
	public EditorBoard board;
	public EditorBoard_Theme boardTheme;

	EditorPiece.KType meTypeEdit;
	int modeMoveEdit =0;

	Vector3 mousePosOld = Vector3.zero;

	public void Start(){

		Reset ();
	}

	public void Save ()
	{
		var content = EditorJsonWrapper.ToJson (board);
		Utility.EasyFile.Save (helperGetPath (id), content);
		Debug.Log ("SAVED " +  +id +" " + content );
	}

	public void Reset(){
		var contnet = Utility.EasyFile.Load (helperGetPath (id));
		EditorBoard_Data data;
		EditorJsonWrapper.ToBoardData (contnet, out data);

		board.Apply (boardTheme, data);
	}
	public void SetMode(int n ){
		modeMoveEdit = n;
		mousePosOld = Input.mousePosition;
	}

	public void E_SetEditType (EditorPiece.KType type)
	{
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
			Debug.Log(EditorJsonWrapper.ToJson(board));
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
		if (meTypeEdit == EditorPiece.KType.World) {
			
			if (index [0] == 0 || index [0] == board.width - 1)
				index [1] = 4;
			if (index [1] == 0 || index [1] == board.height - 1)
				index [0] = 7;
			var entity = board.entitiesWorld [index [0]] [index [1]];

			if (entity == null) {
				board.AddPiece (Instantiate( boardTheme.Get(EditorPiece.KType.Ground)), index [0], index [1], true);
			} else if (entity.meType == EditorPiece.KType.Edge) {
				board.AddPiece (Instantiate(boardTheme.Get(EditorPiece.KType.Door)), index [0], index [1], true);
			} else if (entity.meType == EditorPiece.KType.Door) {
				board.AddPiece (Instantiate(boardTheme.Get(EditorPiece.KType.Edge)), index [0], index [1], true);
			} else if (entity.meType == EditorPiece.KType.Ground) {
				board.DeleteEntity (index [0], index [1], true);
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

