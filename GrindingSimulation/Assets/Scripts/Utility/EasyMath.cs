using UnityEngine;
using System.Collections;

namespace Utility
{
	
	public class EasyMath
	{
		public static Vector3 GetVectorAngle(float angle){
			return new Vector3(Mathf.Cos(angle),Mathf.Sin(angle) ,0);
		}

		
	}
	
	
}