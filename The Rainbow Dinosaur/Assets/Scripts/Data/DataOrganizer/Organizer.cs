//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
namespace Data
{
	public class Organizer
	{
		static int idMax = 3;
		Dictionary<string,List<Board>> dicBoards;
		public Organizer ()
		{
			dicBoards = new Dictionary<string, List<Board>> ();
			for (int i = 0; i < idMax; i++) {
				var board =  DataLoader.Load(i);
				var id = GetId(board);
				List<Board> boardOut;
				if(!dicBoards.TryGetValue(id, out boardOut)){
					dicBoards.Add(id, new List<Board>());
				}
				dicBoards[id].Add(board);
			}
		}
		public Board GetBoard(bool up, bool right,bool down,bool left){
			List<List<Board>> boards = new List<List<Board>> ();
			string code = GetId (up, right, down, left);

			foreach (var d in dicBoards) {
				//UnityEngine.Debug.Log("COMPARING " + code + " " + d.Key + " " + IsSameMinimum(code,d.Key));
				if(IsSameMinimum(code,d.Key)){
					boards.Add(d.Value);
				}
			}
			int countMax = boards.Select (s => s.Count).Sum ();
			int id = UnityEngine.Random.Range(0, countMax);
			int idBoard = 0;
			for(  idBoard =0; idBoard < boards.Count;idBoard++){
				if(id>boards[idBoard].Count-1  ){
					//UnityEngine.Debug.Log("SUBTRACTING : "+boards[idBoard].Count + " " +id);
					id -= boards[idBoard].Count;
				}
				else break;
			}
			boards[idBoard][id].doors[0] = up;
			boards[idBoard][id].doors[1] = right;
			boards[idBoard][id].doors[2] = down;
			boards[idBoard][id].doors[3] = left;

			return boards[idBoard][id];
		}
		public string GetId(Board board){
			return GetId (board.doors [0], board.doors [1], board.doors [2], board.doors [3]);
		}
		public string GetId(bool up, bool right, bool down, bool left){
			string code = "";
			List<bool> bools = new List<bool>(){up,right,down,left};
			foreach (var b in bools)code += (b) ? "1" : "0";
			//UnityEngine.Debug.Log (code);
			return code;
		}
		bool IsSameMinimum(string codeOriginal, string codeCompare){
			for (int i = 0; i < 4; i++) {
				if(codeOriginal[i] == '0') continue;
				if(codeOriginal[i] != codeCompare[i]){
					//UnityEngine.Debug.Log(codeOriginal + " " + codeCompare + " " + i + " " +codeOriginal[i]);
					return false;
				}
			}
			return true;
		}
	}
}
