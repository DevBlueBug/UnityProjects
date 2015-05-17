using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelUI : MonoBehaviour {
	public EditorBoard_Theme boardTheme;
	public List<EditorBoard> boards;
	public Button 
		bttnBack,bttnForth,
		bttnScreen;
	int id = 0; 

	// Use this for initialization
	string helperGetPath(int number){
		return "Data/" + number + ".txt";
	}
	void Start () {
		bttnBack.onClick.AddListener (delegate {
			Move(-1);
			
		});	
		bttnForth.onClick.AddListener (delegate {
			Move(1);
		});
		bttnScreen.onClick.AddListener (delegate {
			Select();
		});
		Refresh ();
		//Debug.Log(Ut

	
	}
	void Move(int direction){
		id =(int)Mathf.Max(0, Mathf.Min(100, id  + boards.Count*direction) );
		Refresh ();
	}
	void Select(){
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	
		for (int i = 0; i< boards.Count; i++) {
			var posLocal = boards[i].ToLocalIndex(pos);
			if(posLocal == null) continue;
			EditorBrain.id = this.id + i;
			//Debug.Log("SELECTED LEVEL LOADING... " +EditorUI.id);
			Application.LoadLevel("Editor");
			return;
		}
	}
	void Refresh(){
		for (int i = 0; i < boards.Count; i++) {
			int level = id +i;
			var content = Utility.EasyFile.Load(helperGetPath(level) );
			Data.Board data;
			if(!Data.JsonWrapper.UnWrap(content,out data)){
				Debug.Log("FAILED TO REFRESH AT " + level);
				Utility.EasyFile.Save(helperGetPath(level), Data.JsonWrapper.Wrap(new Data.Board()).ToString() );
			}

			boards[i].Apply(boardTheme, data);
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {

			//Debug.Log((Resources.Load("Data/Test") as TextAsset ).text);
		}
	}
}
