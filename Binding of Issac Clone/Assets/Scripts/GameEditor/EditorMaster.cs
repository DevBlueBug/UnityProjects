using UnityEngine;
using System.Collections;
using Data;

namespace GameEditor{
	
	public class EditorMaster : MonoBehaviour
	{
		public string PathRoomLayout;
		
		public void Save(int id, DRoomLayout layout){
			var file = System.IO.File.CreateText (PathRoomLayout+"/"+id+".txt");
			file.WriteLine (layout);
			file.Close ();
		}
		public DRoomLayout Load(int id){
			DRoomLayout layout;
			try{
				string s;
				using (var file = System.IO.File.OpenText(PathRoomLayout+"/"+id+".txt")){
					s = file.ReadToEnd ();
				}
				var json = SimpleJSON.JSON.Parse (s);
				layout = (DRoomLayout)s;
			}
			catch{
				Debug.Log("LOAD FAILED ");
				layout = new DRoomLayout(13,7);
				Save (id,layout);
			}
			return layout;
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