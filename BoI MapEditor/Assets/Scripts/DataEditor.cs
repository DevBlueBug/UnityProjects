using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Data;

public class DataEditor
{
	public static string PathData_Layouts = 
		"C:/Users/KyungHwan/Desktop/GitHub/DevBlueBug/UnityProjects/Binding of Issac Clone/Assets/Prefabs/RoomLayouts";
	public static string PathRoomLayout = "C:/Users/KyungHwan/Desktop/GitHub/DevBlueBug/UnityProjects/Test";
	public static int idMax;

	static DataEditor(){
		string path = PathData_Layouts;
		Init (path);
	}
	static string helperFileName(string path, int id){
		return path + "/" + id + ".txt";
	}
	static void Init(string path){
		idMax = 0;
		UpdateIdMax (path);
		Debug.Log ("EditorMaster Report Begin...");
		Debug.Log (idMax);
		Debug.Log ("EditorMaster Report End.");
	}

	public static void UpdateIdMax(string path){
		idMax = -1;
		DRoomLayout layout;
		while (Load(path,idMax+1,out layout)) {
			idMax++;
		};
		
		
	}
	public static void Delete(string path, int n){
		DRoomLayout layout;
		do {
			idMax = n;
			System.IO.File.Delete (helperFileName (path, n));
			if (!Load (path, n + 1, out layout)) break;
			Save (path, n, layout);
			n++;
		} while (true);
	}
	public static void Save(string path, int id, DRoomLayout layout){
		var file = System.IO.File.CreateText (path+"/"+id+".txt");
		file.WriteLine (layout);
		file.Close ();
	}
	public static bool Load(string path, int id, out DRoomLayout layout){
		try{
			string s;
			using (var file = System.IO.File.OpenText(path+"/"+id+".txt")){
				s = file.ReadToEnd ();
			}
			var json = SimpleJSON.JSON.Parse (s);
			layout = (DRoomLayout)s;
			return true;
		}
		catch{
			Debug.Log("LOAD FAILED " + id);
			layout = new DRoomLayout(13,7);
			//Save (id,layout);
			return false;
		}
	}
	public static bool Refresh(string path){
		List<DRoomLayout> layouts = new List<DRoomLayout> ();
		DRoomLayout layout;
		for (int i = 0; i <= idMax; i++) {
			Load(path,i,out layout);
			layouts.Add(layout);
		}
		layouts.Sort (new DRoomLayout_Sort());
		for (int i = 0; i < layouts.Count; i++) {
			Save(path,i,layouts[i]);
		}
		return true;
		
	}
	
	public static bool Refresh(string path, List<DRoomLayout> layouts){
		layouts.Sort (new DRoomLayout_Sort());
		if (layouts.Count != idMax) 
			throw new UnityException("Refresh failed. Incorrect List size.");
		for (int i = 0; i < layouts.Count; i++) {
			Save(path,i,layouts[i]);
		}
		return true;
		
	}
}