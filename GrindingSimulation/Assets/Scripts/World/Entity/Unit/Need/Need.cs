using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NEntity.NUnit.NNeed{
	
	public class Need
	{
		public float point , pointMax;
		public Need(float currentPoint, float pointMax){
			this.point = currentPoint;
			this.pointMax = pointMax;
		}
		public virtual void Init(Entity entity){

		}
		public virtual void Update(World world, Unit entity){
		}
		
		public virtual void UpdateAction(World world, Unit entity){
		}
		
		NTask.Task GetBestTask(List<NTask.Task> tasks){
			if (tasks == null || tasks.Count == 0)
				return null;
			NTask.Task taskSeleced = null;
			float weight = -1;
			for (int i = 0; i < tasks.Count; i++) {
				var t= tasks[i];
				var w = tasks[i].GetWeight();
				if( w > weight){
					taskSeleced = tasks[i];
					weight = w;
				}
			}
			return taskSeleced;
		}

	}
	
}
