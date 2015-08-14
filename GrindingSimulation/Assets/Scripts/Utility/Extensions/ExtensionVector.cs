using UnityEngine;
using System.Collections;

public static class ExtensionVector 
{
	public static Vector3 ToVec3(this Vector2 me, float z ){
		return new Vector3(me.x,me.y,z);
	}
	public static Vector3 Mult(this Vector3 me, Vector3 other ){
		return new Vector3(me.x * other.x , me.y * other.y , me.z * other.z);
	}

}

