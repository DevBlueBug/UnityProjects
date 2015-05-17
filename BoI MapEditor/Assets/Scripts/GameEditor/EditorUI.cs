using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Data;

namespace GameEditor{
	
	public class EditorUI : MonoBehaviour
	{
		public static int idSelected = 0;
		public enum Mode {Select, Edit};
		public string path;
		public EditorTile P_BttnRoom;
		public Graphic PN_Room; 

		public Button 
			bttnBack,
			btnModeSelect,
			btnModeEdit,
			bttnPrevious,
			bttnNext;
		public Text
			txtIdSelected;

		public EditorUI_WheelMenu menuTypes;
		public Button 
			btnGround,btnAir,
			btnWallHard,btnWallSoft,btnWallSolid,
			btnTrap,
			btnEnemy,btnItem;
		public EditorTile 
			doorUp,doorRight,doorDown,doorLeft;


		Mode modeSelected = Mode.Edit;
		DRoomLayout.TileType typeSelected =  DRoomLayout.TileType.Ground;
		EditorTile[,] tiles;
		EditorTile[] doors;
		bool[] doorIsOpen;

		// Use this for initialization
		void Start	 ()
		{
			Debug.Log ("IDSELECTED " + idSelected);
			path = DataEditor.PathData_Layouts;
			//DataEditor.Init (path);
			bttnBack.onClick.AddListener (delegate {
				Application.LoadLevel("SelectLevel");
			});
			InitBttnNextPreviousLevel ();
			InitBttnModes ();
			InitDoors ();
			InitBttnSelectTile ();
			InitMapButtons (PN_Room,P_BttnRoom);
			UpdateDoors ();
			//RefreshFromFilesSaved ();
			Load ();
			//canvasScale.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		}
		void Update(){
			if(modeSelected == Mode.Edit){
				if (Input.GetMouseButtonDown (1)) {
					menuTypes.SetActive(!menuTypes.gameObject.activeSelf);
				}
				menuTypes.KUpdate();
				
			}
			else if(modeSelected == Mode.Select){
				UpdateSelect();

			}
		}
		void UpdateSelect(){
			
			if (Input.GetMouseButtonDown (1)) {

				this.modeSelected = Mode.Edit;
				return;
			}
			var input = Input.GetAxis ("Mouse ScrollWheel");
			if (input == 0) return;
			input /= Mathf.Abs (input);
			idSelected =Mathf.Max(0, idSelected+ (int)input);
			Load ();

		}
		
		void E_Click_Type(DRoomLayout.TileType type){
			typeSelected = type;
		}
		void E_Click_Tile(EditorTile room, int x, int y){
			Debug.Log (x + " " + y + " " +typeSelected);
			room.SetType (typeSelected);
		}
		
		void E_Click_Mode(Mode mode){
			this.modeSelected = mode;
			if (mode == Mode.Select) {
				Save ();
				menuTypes.SetActive (false);
			} else if (mode == Mode.Edit) {
				Save();
			}
		
		}
		void InitBttnNextPreviousLevel(){
			bttnNext.onClick.AddListener (delegate {
				Save();
				idSelected++;
				Load();
			});
			
			bttnPrevious.onClick.AddListener (delegate {
				Save();
				idSelected = Mathf.Max(0,idSelected-1);
				Load();
			});
		
		}
		void InitBttnModes(){
			btnModeEdit.onClick.AddListener ( delegate{E_Click_Mode( Mode.Edit);} );
			btnModeSelect.onClick.AddListener ( delegate{E_Click_Mode( Mode.Select);} );
		}
		void InitBttnSelectTile(){
			Dictionary<Button,DRoomLayout.TileType> dicButtonSelectedState = new Dictionary<Button, DRoomLayout.TileType> (){
				{btnGround, DRoomLayout.TileType.Ground},{btnAir, DRoomLayout.TileType.Air},
				{btnWallHard, DRoomLayout.TileType.Wall_Hard},{btnWallSoft, DRoomLayout.TileType.Wall_Soft},{btnWallSolid, DRoomLayout.TileType.Wall_Solid},
				{btnTrap, DRoomLayout.TileType.Trap},
				{btnEnemy, DRoomLayout.TileType.Enemy},{btnItem, DRoomLayout.TileType.Item}
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
		
		void InitMapButtons(Graphic panel, EditorTile PrefabButton){
			
			var sizePanel = Vector3.Scale (panel.rectTransform.sizeDelta, this.transform.localScale);
			var sizeButton = Vector3.Scale(sizePanel, new Vector3 (1 / 13.0f, 1 / 7.0f, 1));
			
			//size = Vector3.Scale (size, this.transform.lossyScale);
			var posInit = panel.rectTransform.position 
				+ Vector3.Scale (sizePanel, new Vector3 (-.5f, -.5f))
					+ Vector3.Scale (sizeButton, new Vector3 (.5f, .5f));
			
			tiles = new EditorTile[13, 7];
			for (int i = 0; i < 13; i++) 
			for(int j = 0 ; j < 7;j++){
				var o = (tiles[i,j] = Instantiate(PrefabButton));
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
		DRoomLayout ToLayout(){
			DRoomLayout layout = new DRoomLayout (13, 7);
			layout.doors = doorIsOpen;
			Debug.Log ("SAVING " + doorIsOpen [0]);
			for (int i = 0; i < 13; i++) for (int j = 0; j < 7; j++) {
				layout.tiles[i,j]= tiles[i,j].myType;
			}
			return layout;
		}
		void Save(){
			DataEditor.Save ( path,idSelected, ToLayout());
		}
		void Load(){
			DRoomLayout layout;
			DataEditor.Load (path,idSelected,out layout);
			LoadLayout (layout);
			txtIdSelected.text =""+ idSelected;
		}
		void LoadLayout(DRoomLayout layout){
			doorIsOpen = layout.doors;
			for (int i = 0; i< layout.width; i++)
			for (int j = 0; j < layout.height; j++) {
				this.tiles[i,j].SetType( layout.tiles[i,j]);
				}
			UpdateDoors ();
		}
		void UpdateDoors(){
			for (int i = 0; i < 4; i++) {
				doors[i].SetType((doorIsOpen[i])?  DRoomLayout.TileType.Ground : DRoomLayout.TileType.Air);
			}
			
		}
		void RefreshFromFilesSaved(){
			List<DRoomLayout> layouts = new List<DRoomLayout> ();
			for(int i = 0 ; i < DataEditor.idMax;i++){
				DRoomLayout layout;
				DataEditor.Load(path,i,out layout);
				layouts.Add(layout);
			}
			DataEditor.Refresh (path,layouts);
		}
	}
}

