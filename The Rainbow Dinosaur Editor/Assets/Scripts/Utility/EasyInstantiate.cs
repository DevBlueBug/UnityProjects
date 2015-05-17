using UnityEngine;
using System.Collections;

namespace Utility
{
	
	public class EasyInstantiate : MonoBehaviour
	{
		public static GameObject InstantiateScale(GameObject Prefab, Transform myT){
			var obj = Instantiate (Prefab);
			var scaleOld = obj.transform.localScale;
			var rotOld = obj.transform.rotation.eulerAngles;
			obj.transform.parent = myT;
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localScale = scaleOld;
			obj.transform.rotation = Quaternion.Euler (rotOld);

			return obj;
		}
		
	}
	

}