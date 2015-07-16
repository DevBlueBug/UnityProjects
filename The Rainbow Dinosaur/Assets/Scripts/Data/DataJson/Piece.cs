using System;
namespace Data
{
	public class Piece
	{
		public enum KId { 
			Unknown,
			Empty,
			Edge,Ground,Door,
			Decoration,Item,
			Block_Soft, Block_Hard , 
			TrapFoot,TrapTower,TrapGravity, 
			EnemyFoot,EnemyFlying,EnemyTower,EnemyTurret,
			Player};
		public KId meType;
		public int hp;
		public bool 
			isAttacked_Bullet,
			isAttacked_Bomb;
		public int X, Y;

		public Piece (KId type, int x, int y)
		{
			this.X = x;
			this.Y = y;
			this.meType = type;
		}
	}
}

