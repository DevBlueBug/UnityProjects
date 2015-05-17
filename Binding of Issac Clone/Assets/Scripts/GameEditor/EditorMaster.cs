using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Data;

namespace GameEditor{

	public class EditorMaster : MonoBehaviour
	{
		public string PathRoomLayout;
		public int idCount;
		void Awake(){

			idCount = 0;
			DRoomLayout layout;
			while (Load(idCount++,out layout)) {
			};
			idCount--;
			Debug.Log ("EditorMaster Report Begin...");
			Debug.Log (idCount);
			Debug.Log ("EditorMaster Report End.");
		}
		public void Refresh(){
			idCount = 0;
			DRoomLayout layout;
			while (Load(idCount+1,out layout)) {
				idCount++;
			};


		}
		public void Save(int id, DRoomLayout layout){
			var file = System.IO.File.CreateText (PathRoomLayout+"/"+id+".txt");
			file.WriteLine (layout);
			file.Close ();
		}
		public bool Load(int id, out DRoomLayout layout){
			try{
				string s;
				using (var file = System.IO.File.OpenText(PathRoomLayout+"/"+id+".txt")){
					s = file.ReadToEnd ();
				}
				var json = SimpleJSON.JSON.Parse (s);
				layout = (DRoomLayout)s;
				return true;
			}
			catch{
				Debug.Log("LOAD FAILED ");
				layout = new DRoomLayout(13,7);
				//Save (id,layout);
				return false;
			}
		}
		public bool Refresh(List<DRoomLayout> layouts){
			layouts.Sort (new DRoomLayout_Sort());
			if (layouts.Count != idCount) 
				throw new UnityException("Refresh failed. Incorrect List size.");
			for (int i = 0; i < layouts.Count; i++) {
				Save(i,layouts[i]);
			}
			return true;

		}
		// Use this for initialization
		void Start ()
		{
		}
		
		// Update is called once per frame
		void Update ()
		{
			
		}
	}
	

}