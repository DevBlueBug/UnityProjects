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
namespace Data
{
	public class Board
	{
		public bool[] doors;
		public int width,height;
		public List<Piece> piecesWorld,piecesUnits;
		
		public Board ()
		{
			width = 15;
			height = 9;
			piecesWorld = new List<Piece> ();
			piecesUnits = new List<Piece> ();
			doors = new bool[4];
		}
		public Board(bool up, bool right, bool down, bool left):this(){
			doors = new bool[]{up,right,down,left};
		}
	}
}

