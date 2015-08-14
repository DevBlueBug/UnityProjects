using UnityEngine;
using System.Collections.Generic;

public class UI_DrawLine : MonoBehaviour
{
	public GameObject P_Selected;
	Vector3
		posFrom	= Vector3.zero,
		posTo	= Vector3.zero;
	int mode = 0 ;

	List<GameObject> objsDisplay = new List<GameObject>();


	void Awake(){
		var obj = new GameObject ("UI_DrawLine");
		obj.transform.parent = this.transform;
		for (int i = 0; i < 50; i++) {
			var copy = Instantiate(P_Selected);
			copy.transform.parent = this.transform;
			copy.gameObject.SetActive(false);
			objsDisplay.Add(copy);
		}
	}
	void Update ()
	{
		var posToNew = GetMousePosition ();
		int modeNew = GetMode (posTo.x, posTo.y);
		if (modeNew != mode || posToNew.x != posTo.x || posToNew.y != posTo.y) {
			posTo = posToNew;
			mode = modeNew;
			if(mode == 0)
				RenderXAxis();
			else RenderYAxis();


		}
	}
	void RenderAt(int index, float x, float y){
		index = index % (objsDisplay.Count );
		objsDisplay [index].transform.position = new Vector3 (x, y, 0);
		objsDisplay [index].SetActive (true);
	}
	void RenderEndAt(int index){
		for(;
		    index < objsDisplay.Count;index++){
			objsDisplay[index].SetActive(false);
		}
	}
	void RenderXAxis(){
		float dis = posTo.x - posFrom.x;
		if (dis == 0) return;
		int index = 0;
		float increment = dis / Mathf.Abs (dis);
		float m = (posTo.y - posFrom.y) / (posTo.x - posFrom.x);
		//string s = "";
		for (float x = posFrom.x; x <= posTo.x; x+= increment) {
			float y = Mathf.Round( m * ( x - posFrom.x ) + posFrom.y );
			//s += "[ " + x + " , " + y + " ] ";
			RenderAt(index++, x,y);
		}
		
		//Debug.Log ("RENDERING AT " + s);
		RenderEndAt (index);
	}

	void RenderYAxis(){
		float dis = posTo.y - posFrom.y;
		if (dis == 0) return;
		int index = 0;
		float increment = dis / Mathf.Abs (dis);
		float m = (posTo.x - posFrom.x) / (posTo.y - posFrom.y);
		//string s = "";
		for (float y = posFrom.y; y <= posTo.y; y+= increment) {
			float x = Mathf.Round( m * ( y - posFrom.y ) + posFrom.x );
			//s += "[ " + x + " , " + y + " ] ";
			RenderAt(index++, x,y);
		}
		
		//Debug.Log ("RENDERING AT " + s);
		RenderEndAt (index);
	}
	bool IsNewRenderNeeded(){
		return false;
	}
	Vector3 GetMousePosition(){
		var mousePos = Camera.main.ScreenToWorldPoint ( Input.mousePosition);
		return new Vector3 (Mathf.Round(mousePos.x),Mathf.Round(mousePos.y),0);
	}
	int GetMode(float x, float y){
		return (Mathf.Abs(x) > Mathf.Abs(y))? 0:1;
	}
	public void Init(){
		posFrom = GetMousePosition ();
		posTo = GetMousePosition ();
	}
}

