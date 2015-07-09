using UnityEngine;
using System.Collections;

public class RenderWalls : MonoBehaviour
{
	public GameObject P_WallInside,P_WallOutside;
	// Use this for initialization
	void Start ()
	{
		Init (P_WallInside,.1f);
		Init (P_WallOutside,0);
	
	}
	void Init(GameObject P_Wall , float z){
		for(int i = -1; i < 16; i++){
			((GameObject)Instantiate(P_Wall,new Vector3(i,-2,z),Quaternion.Euler(0,0,180)))
				.transform.parent= this.transform;;
			((GameObject)Instantiate(P_Wall,new Vector3(i,10,z),Quaternion.Euler(0,0,0)))
				.transform.parent= this.transform;;
		}
		for (int j = -2; j < 11; j++) {
			((GameObject)Instantiate(P_Wall,new Vector3(-2,j,z),Quaternion.Euler(0,0,90)))
				.transform.parent= this.transform;
			((GameObject)Instantiate(P_Wall,new Vector3(16	,j,z),Quaternion.Euler(0,0,270)))
				.transform.parent= this.transform;
		}
	}
}

