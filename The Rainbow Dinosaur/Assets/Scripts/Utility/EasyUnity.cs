using UnityEngine;
using System.Collections.Generic;
namespace Utility{
	public class EasyUnity{
		public static List<Vector3> dirFour3 = new List<Vector3> (){
			new Vector3(0,1,0),
			new Vector3(1,0,0),
			new Vector3(0,-1,0),
			new Vector3(-1,0,0)
		};
		public static List<float> angleFour = new List<float>(){
			0, 270,180, 90
		};
		public static Vector3 Vector3RandomDirection(float angleFrom,float angleTo){
			float radom = Random.Range (angleFrom,angleTo);
			return new Vector3(Mathf.Cos(radom),Mathf.Sin(radom),0);
		}
		
		public static Vector3 Vector3Direction(float radius){
			return new Vector3(Mathf.Cos(radius),Mathf.Sin(radius),0);
		}
	}

}