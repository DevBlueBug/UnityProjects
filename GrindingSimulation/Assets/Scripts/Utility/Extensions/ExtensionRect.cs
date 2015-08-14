using UnityEngine;
using System.Collections;

public static class ExtensionRect 
{

	public static Vector3 ToVec3(this Rect me,float z){
		return new Vector3(Mathf.Abs( me.x), Mathf.Abs(me.y),z );
	}
}

