using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EditorUI : MonoBehaviour
{

	public Sprite sprMove,sprSelect;
	public GameObject CollectionIcons;
	public EditorBrain brain;
	public Button 
		bttnMoveSelect,
		bttnLeft, bttnRight,
		bttnReset,bttnClear,
		bttnSave,
		bttnBefore,

		bttnScreen;
	public List<Button> bttnsContent;

	List<EditorUIIcon> icons = new List<EditorUIIcon>();
	int iconInit = 0;
	int modeMoveSelect = 1;


	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i< CollectionIcons.transform.childCount; i++) {
			var c = CollectionIcons.transform.GetChild(i).GetComponent<EditorUIIcon>();
			icons.Add(c);
			//Debug.Log(i);
		}
		bttnMoveSelect.onClick.AddListener (delegate {
			E_BttnMoveSelect();
			Refresh();
		});
		bttnLeft.onClick.AddListener (delegate {
			E_BttnLeftRight(0);
			Refresh ();
		});
		bttnRight.onClick.AddListener (delegate {
			E_BttnLeftRight(1);
			Refresh ();
		});
		bttnScreen.onClick.AddListener (delegate {
			E_BttnScreen();
		});
		bttnReset.onClick.AddListener (delegate {
			brain.Reset();
		});
		bttnClear.onClick.AddListener (delegate {
			brain.BoardClear();
		});
		bttnSave.onClick.AddListener (delegate {
			brain.Save();

		});
		bttnBefore.onClick.AddListener (delegate {
			Application.LoadLevel("MapSelect");
		});
		for (int i = 0; i < bttnsContent.Count; i++) {
			int index = i;
			bttnsContent [i].onClick.AddListener (delegate {
				E_BttnContent(index);
			});
		}

		Refresh ();
	}
	
	// Update is called once per frame

	void Refresh(){
		Sprite[] sprMoveSelect = new Sprite[]{sprMove,sprSelect};
		bttnMoveSelect.GetComponent<Image> ().sprite = sprMoveSelect [modeMoveSelect];
		for(int i = 0 ; i < bttnsContent.Count;i++){
			var image = bttnsContent[i].GetComponent<Image>();
			int index = iconInit +i;
			if(index >= icons.Count){
				image.sprite = null;
			}
			else image.sprite = icons[index].spr;
			//Debug.Log(index);
		}
		brain.SetMode (modeMoveSelect);
	}
	void E_BttnContent(int n){
		if (n >= icons.Count) return;
		brain.E_SetEditType (icons [iconInit + n].type);
	}
	void E_BttnMoveSelect(){
		modeMoveSelect = (modeMoveSelect + 1) % 2;
	}
	void E_BttnLeftRight(int leftOrRight){
		int iconInitNew = iconInit + (int)(bttnsContent.Count) * ((leftOrRight==0)? -1 : 1); 
		iconInit =(int)Mathf.Max(0 ,Mathf.Min (iconInitNew, bttnsContent.Count * (int)( icons.Count/bttnsContent.Count)  ));
	}
	void E_BttnScreen ()
	{
		brain.E_Click ();
		
	}
}

