using UnityEngine;
using System.Collections;

namespace NWorld.NUnit.NNeed{
	
	public class Need
	{
		public float point , pointMax;
		public Need(float currentPoint, float pointMax){
			this.point = currentPoint;
			this.pointMax = pointMax;
		}
		public virtual void Update(World world, Unit entity){
		}
		
		public virtual void UpdateAction(World world, Unit entity){
		}

	}
	
}
