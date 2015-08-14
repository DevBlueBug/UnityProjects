using UnityEngine;
using System.Collections.Generic;

namespace NUI{
	public class UIManager : MonoBehaviour
	{
		public UIDescription uiDesc;
		public UIItemsEquipped uiEquip;
		
		public List<UI_Icon> iconsMenuTile; //
		public List<UI_Icon> iconsMenuEntities; //



		void Awake ()
		{
			uiEquip.E_DisplayDescription += H_Equip_DisplayDescription;
			uiEquip.E_UnDisplayDescription += H_Equip_UnDisplayDescription;
		}
		
		// Update is called once per frame
		void Update ()
		{
			
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

