using System;
namespace Data
{
	public class Piece
	{
		public enum KType { 
			Empty,
			Edge,Ground,Door,
			Decoration,Item,
			Block_Soft, Block_Hard , 
			TrapFoot,TrapTower,TrapGravity, 
			EnemyFoot,EnemyFlying,EnemyTower,EnemyTurret,
			Player};
		public KType meType;
		public int hp;
		public bool 
			isAttacked_Bullet,
			isAttacked_Bomb;
		public int X, Y;

		public Piece (KType type, int x, int y)
		{
			this.X = x;
			this.Y = y;
			this.meType = type;
		}
	}
}

