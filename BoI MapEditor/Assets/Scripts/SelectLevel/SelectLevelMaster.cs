using UnityEngine;
using UnityEngine.UI;
using Data;
using System.Collections.Generic;

public class SelectLevelMaster : MonoBehaviour
{
	public enum KMode{ Load,Delete };
	KMode myMode = KMode.Load;
	public RoomDisplayer P_RDisplayer;
	public Button btnLoad,btnDelete,btnNew;
	public float width = 3;
	public float height = 5;

	public static int idInit = 0;

	List<RoomDisplayer> rooms = new List<RoomDisplayer>();

	// Use this for initialization
	void Start ()
	{ 
		DataEditor.UpdateIdMax (DataEditor.PathData_Layouts);
		btnLoad.onClick.AddListener(delegate {
			myMode = KMode.Load;
		});
		btnDelete.onClick.AddListener(delegate{
			myMode = KMode.Delete;
		});
		btnNew.onClick.AddListener (delegate {
			Load(DataEditor.idMax+1);
		});
		float 
			lengthX = 1 / width,
			lengthY = 1 / height;
		float length = (lengthX > lengthY) ? lengthY : lengthX;
		for (int y= 0; y< height; y++)
			for (int x= 0; x <width;x++) {
			int _x= x,_y=y;
			var roomDisplayer = Instantiate(P_RDisplayer);
			roomDisplayer.gameObject.name = " " + x + " " + y;
			roomDisplayer.transform.SetParent(this.transform);
			roomDisplayer.transform.localPosition = new Vector3(-length*(-.5f+width/2),length*(-1.0f+height)/2,0);
			//roomDisplayer.transform.localPosition = new Vector3((int)(width/2),(int)(height/2),0);
			roomDisplayer.transform.localPosition+= new Vector3(length*x,-length*y,0);
			roomDisplayer.transform.localScale = new Vector3(length,length,0);
			roomDisplayer.GetComponent<Button>().onClick.AddListener(delegate {
				Click(_x,_y);
			});
			roomDisplayer.Init(.95f);
			rooms.Add(roomDisplayer);
			}
		DataEditor.Refresh (DataEditor.PathData_Layouts);
		RefreshRoomDisplayers ();
	}
	void RefreshRoomDisplayers(){
		DRoomLayout layout;
		while (!DataEditor.Load (DataEditor.PathData_Layouts, idInit,out layout)) {
			if(idInit ==0){
				break;
			}
			idInit =(int)Mathf.Max(0,idInit- width*height);
		}

		for (int x= 0; x< width; x++)
		for (int y= 0; y <height; y++) {
			int id = (int)(idInit+ x+y*width);
			var room  = rooms[(int)(x+ y*width)];
			if(!DataEditor.Load( DataEditor.PathData_Layouts,id,out layout)){
				room.gameObject.SetActive(false);
				continue;
			}
			room.gameObject.SetActive(true);
			room.Init(layout);
		}
	}

	public void Click(int x, int y){
		int id = (int)(idInit+ x + y * width);
		if (myMode == KMode.Load)
			Load (id);
		if (myMode == KMode.Delete) {
			DataEditor.Delete(DataEditor.PathData_Layouts, id);
			RefreshRoomDisplayers();
		}

	}
	void Load(int id){
		GameEditor.EditorUI.idSelected = id;
		Debug.Log ("LOAD LEVEL " + id);
		Application.LoadLevel ("Editor");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		var input = Input.GetAxis ("Mouse ScrollWheel");
		if (input == 0) return;
		Debug.Log ("UPDATED");
		input /= Mathf.Abs (input);
		idInit = (int)Mathf.Max (0, idInit + input * width * height);
		RefreshRoomDisplayers ();
	
	}
}

