using UnityEngine;
using System.Collections;

public class EditorUI_WheelMenu : MonoBehaviour
{
	public GameObject menu;
	public RectTransform rect;
	float speed;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	public void KUpdate ()
	{
		var input = Input.GetAxis ("Mouse ScrollWheel");

		if (input != 0) {
			if(!menu.activeSelf){
				menu.SetActive(true);
			}
			else{
				var height = Vector3.Scale (this.transform.lossyScale, rect.sizeDelta).y;
				speed += height * (-input / Mathf.Abs (input));
			}
		}
		//menu.SetActive(input!=0);
		UpdateMove ();

	}
	public void SetActive(bool b){
		this.gameObject.SetActive (b);
		menu.SetActive (b);

	}
	public void UpdateMove(){
		menu.transform.localPosition += new Vector3 (0,speed*.1f,0);
		speed *= .9f;
	}

}

