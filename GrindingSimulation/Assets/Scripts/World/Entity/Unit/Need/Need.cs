using UnityEngine;
using System.Collections;

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

	}
	
}
