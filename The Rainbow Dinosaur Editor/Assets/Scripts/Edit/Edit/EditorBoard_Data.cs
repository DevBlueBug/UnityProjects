using System.Collections.Generic;

public class EditorBoard_Data{

	public bool[] doors = new bool[]{false,false,false,false};
	public List<EditorPiece_Data> piecesWorld,piecesUnits;
	public EditorBoard_Data(){
		piecesWorld = new List<EditorPiece_Data> ();
		piecesUnits = new List<EditorPiece_Data> ();
	}

}