using UnityEngine;
using System.Collections.Generic;


public class UI_IconsSquare : UIBase
{
	public UnityEngine.UI.Image uiParent;


	// Use this for initialization
	void Start ()
	{
		float maxCount = 5;
		//Debug.Log (uiParent.rectTransform.rect.width);
		//var e = (uiParent.rectTransform / 2.0f);
		var rect = uiParent.rectTransform.rect.ToVec3 (0).Mult(uiParent.transform.lossyScale);
		Debug.Log ("BEFORE "  +rect + " " + uiParent.transform.lossyScale);
		Debug.Log ("AFTER " +rect);
	

		var posInit = uiParent.rectTransform.position - new Vector3 (rect.x,-rect.y) + new Vector3();

		float size = uiParent.rectTransform.rect.width * uiParent.transform.lossyScale.x / (maxCount);

		Debug.Log (uiParent.preferredWidth);
		for (int i = 0; i < maxCount; i++) {
			Debug.Log (posInit);
			var obj = GetRectTransform(uiParent.rectTransform,
			                           posInit + new Vector3(size*(.5f+i),-size*.5f,0),
			                           new Vector2(size,size)*.7f);


		}
	}
	public void Init(List<UI_Icon> icons){
		//display all the icons

	}
	

	// Update is called once per frame
	void Update ()
	{
	
	}
}

