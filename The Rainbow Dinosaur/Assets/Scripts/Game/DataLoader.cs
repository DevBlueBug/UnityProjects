using UnityEngine;
using Data;
public class DataLoader{
	
	public static Board Load(int n){
		Board board;
		try{
			JsonWrapper.UnWrap( Utility.EasyFile.Load ("Data/"+n+".txt"),out board);
			return board;
		}
		catch{
			Debug.Log("Load failed...");
			return new Data.Board();

		}
	}
	public static bool Save(int n, string content){
		return Utility.EasyFile.Save ("Data/" + n + ".txt", content);
	}
}