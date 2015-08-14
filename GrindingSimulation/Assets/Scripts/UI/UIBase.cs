using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIBase : MonoBehaviour
{
	public delegate void D_IconCalled(BaseEventData data, string content);
	public D_IconCalled E_IconCalled = delegate {};

	List<UI_Icon> icons; 


	public UnityEngine.UI.Image body;
	public UnityEngine.RectTransform GetRectTransform(RectTransform parent, Vector3 position, Vector2 size){
		var obj = new GameObject();
		var rectTransform = obj.AddComponent<RectTransform>();
		rectTransform.SetParent (parent);
		rectTransform.position = position;
		rectTransform.sizeDelta = size;
		return rectTransform;
	}
	public EventTrigger.Entry GetEventTrigger(UnityAction<BaseEventData,string>  action, EventTriggerType type, string context){
		var entry = new EventTrigger.Entry ();//{callback = action, eventID = type};
		entry.eventID = type;
		entry.callback.AddListener((e) =>action(e,context));
		return entry;
	}
	internal void InitIcons(int w, int h){

	}


}

