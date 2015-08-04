using UnityEngine;
using System.Collections.Generic;

namespace NUI{
	public class UIManager : MonoBehaviour
	{
		public GameObject cam;
		public UIDescription uiDesc;
		public UIItemsEquipped uiEquip;



		Dictionary<KeyCode, Vector2> dirCam = new Dictionary<KeyCode, Vector2>(){
			{KeyCode.W, Vector3.up},
			{KeyCode.S, Vector3.down},
			{KeyCode.A, Vector3.left},
			{KeyCode.D, Vector3.right}
		};
		// Use this for initialization
		void Awake ()
		{
			uiEquip.E_DisplayDescription += H_Equip_DisplayDescription;
			uiEquip.E_UnDisplayDescription += H_Equip_UnDisplayDescription;
		}
		
		// Update is called once per frame
		void Update ()
		{
			foreach (var d in dirCam) {
				if(Input.GetKey(d.Key) ){
					cam.transform.localPosition += new Vector3(d.Value.x,d.Value.y,0) * 10.0f * Time.deltaTime ;
				}
			}

			
			if (Input.GetKeyDown (KeyCode.X)) {
				if(uiEquip.IsOn()) {
					uiEquip.Off();
					uiDesc.Off();
					return;
				}
			}
		
		}
		public void Link(RenderEntity entity){
			//entity.E_Clicked += H_EntityClicked;
		}
		public void Link(RenderUnit unit){
			unit.E_Clicked += H_UnityClicked;
		}
		void H_UnityClicked(RenderUnit renderE){
			uiEquip.On ();
			uiEquip.Display (renderE.Unit);
		}
		void H_Equip_DisplayDescription(UIItemsEquipped caller, string description){
			uiDesc.On ().SetText (description);;
		}
		void H_Equip_UnDisplayDescription(UIItemsEquipped caller, string description){
			uiDesc.Off ();
		}
	}
}

