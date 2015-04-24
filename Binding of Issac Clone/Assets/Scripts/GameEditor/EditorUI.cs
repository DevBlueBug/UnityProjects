using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


namespace GameEditor{
	
	public class EditorUI : MonoBehaviour
	{
		public EditorTile P_BttnRoom;
		public Graphic PN_Room; 
		public Button 
			btnGround,btnAir,
			btnWallHard,btnWallSoft,btnWallSolid,
			btnTrap,
			btnEnemy,btnItem;
		public EditorTile 
			doorUp,doorRight,doorDown,doorLeft;

		EditorTile.KType typeSelected = EditorTile.KType.Ground;
		EditorTile[,] room;
		EditorTile[] doors;
		bool[] doorIsOpen;

		// Use this for initialization
		void Start	 ()
		{
			InitBttnSelectTile ();
			InitDoors ();
			InitMapButtons (PN_Room,P_BttnRoom);

			UpdateDoors ();
			//canvasScale.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		}
		void InitBttnSelectTile(){
			Dictionary<Button,EditorTile.KType> dicButtonSelectedState = new Dictionary<Button, EditorTile.KType> (){
				{btnGround, EditorTile.KType.Ground},{btnAir, EditorTile.KType.Air},
				{btnWallHard, EditorTile.KType.Wall_Hard},{btnWallSoft, EditorTile.KType.Wall_Soft},{btnWallSolid, EditorTile.KType.Wall_Solid},
				{btnTrap, EditorTile.KType.Trap},
				{btnEnemy, EditorTile.KType.Enemy},{btnItem, EditorTile.KType.Item}
			};
			foreach (var dic in dicButtonSelectedState) {
				var typeSelected = dic.Value;
				dic.Key.onClick.AddListener( delegate{E_Click_Type(typeSelected);});
			}
		}
		void InitDoors(){
			doors = new EditorTile[]{doorUp,doorRight,doorDown,doorLeft};
			doorIsOpen = new bool[]{true,true,true,true};
			for (int i = 0; i < 4; i++) {
				int n = i;
				doors[i].GetComponent<Button>().onClick.AddListener(
					delegate {doorIsOpen[n] =!doorIsOpen[n] ; UpdateDoors();}
				);
			}
		}
		void UpdateDoors(){
			for (int i = 0; i < 4; i++) {
				doors[i].SetType((doorIsOpen[i])? EditorTile.KType.Ground : EditorTile.KType.Air);
			}

		}
		void E_Click_Type(EditorTile.KType type){
			typeSelected = type;
		}
		void E_Click_Tile(EditorTile room, int x, int y){
			Debug.Log (x + " " + y + " " +typeSelected);
			room.SetType (typeSelected);
		}
		void InitMapButtons(Graphic panel, EditorTile PrefabButton){

			var sizePanel = Vector3.Scale (panel.rectTransform.sizeDelta, this.transform.localScale);
			var sizeButton = Vector3.Scale(sizePanel, new Vector3 (1 / 13.0f, 1 / 7.0f, 1));
			
			//size = Vector3.Scale (size, this.transform.lossyScale);
			var posInit = panel.rectTransform.position 
				+ Vector3.Scale (sizePanel, new Vector3 (-.5f, -.5f))
					+ Vector3.Scale (sizeButton, new Vector3 (.5f, .5f));

			room = new EditorTile[13, 7];
			for (int i = 0; i < 13; i++) 
				for(int j = 0 ; j < 7;j++){
				var o = (room[i,j] = Instantiate(PrefabButton));
				var bttn = o.GetComponent<Button>();
				int x = i;
				int y = j;
				bttn.onClick.AddListener(delegate {E_Click_Tile(o, x,y);});

				o.transform.SetParent(panel.transform);
				o.transform.localScale = Vector3.one;
				o.rect.sizeDelta =  Vector3.Scale(panel.rectTransform.sizeDelta, new Vector3 (1 / 13.0f, 1 / 7.0f, 1));
				o.rect.position  = posInit+Vector3.Scale(sizeButton, new Vector3(i,j,1) );
				o.gameObject.name = "" + i + " " + j;
			}
		}
		void Update(){
		}
	}
}

