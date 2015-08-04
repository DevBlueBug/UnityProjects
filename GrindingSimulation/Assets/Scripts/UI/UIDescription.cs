using UnityEngine;
using System.Collections;
namespace NUI{

	public class UIDescription : MonoBehaviour
	{

		public UnityEngine.UI.Image panel;
		public UnityEngine.UI.Text text;
		void Update(){
			if (panel.IsActive ()) {
				panel.transform.position = Input.mousePosition 
					+ new Vector3(
						(panel.rectTransform.rect.width* panel.rectTransform.lossyScale.x) *.5f,
						(-panel.rectTransform.rect.height* panel.rectTransform.lossyScale.y) *.55f,0);
			}
			//Debug.Log (panel.rectTransform.lossyScale);
			//var s = panel.rectTransform.rect.size * panel.rectTransform.localScale;
			//Input.mousePosition

		}
		public bool IsOn(){
			return panel.gameObject.activeSelf;
		}
		public UIDescription On(){
			panel.gameObject.SetActive (true);
			return this;
		}
		public UIDescription SetText(string s){
			text.text = s;
			return this;
		}
		public void Off(){
			panel.gameObject.SetActive (false);
		}
	}
}