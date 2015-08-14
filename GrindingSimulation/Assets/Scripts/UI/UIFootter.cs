using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIFootter : UIBase
{
	public List<UI_Icon> icons;
	void Awake(){

	}
	void Start(){
		//Debug.Log ("MY WIDTH AND HEIGHT " + body.transform.lossyScale + " " + body.GetWidth () + " " + body.GetHeight ());
		Init ();
	}
	public void Init(){
		var length = body.GetHeight();
		var posInit = body.transform.position + new Vector3(-body.GetWidth () * .5f + length*.5f ,0,0);
		for (int i = 0; i < icons.Count; i++) {
			var pos = posInit + new Vector3(length*i,0,0);
			var size = new Vector2(length,length );
			var obj = GetRectTransform(body.rectTransform,pos,size);
			var image =obj.gameObject.AddComponent<UnityEngine.UI.Image>();
			var eventTrigger = obj.gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();

			image.sprite = icons[i].sprite;
			eventTrigger.triggers.Add( GetEventTrigger( H_Pointer, EventTriggerType.PointerClick, icons[i].context ) );

		}
	}


	void H_Pointer(BaseEventData data, string context){
		var pointer = (PointerEventData)data;
		Debug.Log ("DOWN DOWN 1 "  + " " + context);
		this.E_IconCalled (data, context);
	}


}

