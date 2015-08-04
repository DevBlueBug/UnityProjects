using UnityEngine;
using System.Collections.Generic;
namespace NUI{
	public class UIItemsEquipped : MonoBehaviour
	{
		public delegate void D_MeString(UIItemsEquipped me, string context);

		public UnityEngine.UI.Image panel;
		public UnityEngine.UI.Image
			head,shoulders,neck,chest,waist,legs,feet,hands,ringL,ringR,arm,weaponL,weaponR;
		
		internal D_MeString E_DisplayDescription = delegate {	};
		internal D_MeString E_UnDisplayDescription = delegate {	};

		Dictionary<string, string> dicItemDescriptions;// = new Dictionary<UIManager, UnityEngine.UI.Image>();
		void Awake(){
			dicItemDescriptions = new Dictionary<string, string> (){
				{"Head",""},{"Shoulders",""},{"Neck",""},
				{"Chest",""},{"Waist",""},{"Legs",""},
				{"Feet",""},{"Hands",""},
				{"RingL",""},{"RingR",""},{"Arms",""},
				{"WeaponL\t",""},{"WeaponR",""}
			};
		}
		public bool IsOn(){
			return panel.gameObject.activeSelf;
		}
		public void On(){
			panel.gameObject.SetActive (true);
		}
		public void Off(){
			panel.gameObject.SetActive (false);
		}
		public void Display(NWorld.NEntity.NUnit.Unit unit){

			UpdateEquipmentDescription ("Head", unit.equipments.head);
			UpdateEquipmentDescription ("Shoulders", unit.equipments.shoulders);
			UpdateEquipmentDescription ("Neck", unit.equipments.neck);
			UpdateEquipmentDescription ("Chest", unit.equipments.chest);
			UpdateEquipmentDescription ("Waist", unit.equipments.waist);
			UpdateEquipmentDescription ("Legs", unit.equipments.legs);
			UpdateEquipmentDescription ("Feet", unit.equipments.feet);
			UpdateEquipmentDescription ("Hands", unit.equipments.hands);
			UpdateEquipmentDescription ("RingL", unit.equipments.ringL);
			UpdateEquipmentDescription ("RingR", unit.equipments.ringR);
			UpdateEquipmentDescription ("Arms", unit.equipments.arms);
			UpdateEquipmentDescription ("WeaponL", unit.equipments.weaponL);
			UpdateEquipmentDescription ("WeaponR", unit.equipments.weaponR);


		}
		void UpdateEquipmentDescription(string id,NWorld.NEntity.NUnit.EquipmentSlots.Slot slot){
			string s = id ;
			if (slot == null) {
				s +="\nName : null\nDescription : unavailable."; 
			}
			dicItemDescriptions [id] = s;
		}
		public void DisplayInformation(string id){
			E_DisplayDescription (this, dicItemDescriptions [id]);
			Debug.Log ("Display " + id);
		}
		
		public void UnDisplayInformation(string id){
			E_UnDisplayDescription (this, dicItemDescriptions [id]);
			Debug.Log ("UnDisplay " + id);
		}

	}
}

