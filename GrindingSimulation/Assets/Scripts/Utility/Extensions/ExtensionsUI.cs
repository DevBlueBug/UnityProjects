using UnityEngine;
using System.Collections;

public static class ExtensionsUI
{
	public static float GetWidth(this UnityEngine.UI.Image me){
		return me.rectTransform.rect.width * me.transform.lossyScale.x;
	}
	public static float GetHeight(this UnityEngine.UI.Image me){
		return me.rectTransform.rect.height * me.transform.lossyScale.y;
	}

}

