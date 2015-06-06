using System;
namespace NBehaviour.NCondition
{
	public class TimeElapsed :Condition
	{
		float timeMax,timeElapsed;
		public TimeElapsed (Entity entity, float timeMax):base(entity)
		{
			this.timeMax = timeMax;
		}
		public override int Process (Entity entity, Room room, int bhvProcessResult)
		{
			timeElapsed += UnityEngine.Time.deltaTime;
			if (timeElapsed > timeMax) {
				timeElapsed = 0;
				return 1;
			}
			return 0; //nothing 
		}
	}
}

