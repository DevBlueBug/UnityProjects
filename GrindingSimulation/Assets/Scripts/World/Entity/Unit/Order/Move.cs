using UnityEngine;
using System.Collections.Generic;


namespace NWorld.NEntity.NUnit.NOrder {
	
	public class Move : Order
	{
		float x, y;
		float turnElapsed, turnRequired;
		public Move(World world, Entity entity, float x, float y):base(world,entity){
			this.x = x;
			this.y = y;
		}
		public override bool Init (World world, Unit unit)
		{
			return base.Init (world, unit);
		}
		public override void UpdateInit_Process (World world, Unit unit)
		{
			int turnN = world.tiles [(int)x, (int)y].GetTurnRequiredToGetHere (unit);
			if (turnN < 1) stateMe = State.Failure;
			turnElapsed = 0;
			turnRequired = turnN;

		}
		public override void Update_Process (World world, Unit unit)
		{
			//turnElapsed++;
			if (++turnElapsed >= turnRequired) {
				unit.x = x;
				unit.y = y;
				stateMe = State.Success;
			}
		}
	}
	
}
