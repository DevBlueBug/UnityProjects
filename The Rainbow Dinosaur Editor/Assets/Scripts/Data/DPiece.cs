using System;
namespace Data
{
	public class DPiece
	{
		public enum KType { 
			Edge,Ground,Door,
			Decoration,Item,
			Block_Soft, Block_Hard , 
			TrapFoot,TrapTower,TrapGravity, 
			EnemyFoot,EnemyFlying,EnemyTower,EnemyTurret,
			Player};

		public DPiece ()
		{
		}
	}
}

