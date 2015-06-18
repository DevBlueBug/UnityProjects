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
		for(int i = 0; i < 15; i++){
			((GameObject)Instantiate(P_Wall,new Vector3(i,-1,z),Quaternion.Euler(0,0,180)))
				.transform.parent= this.transform;;
			((GameObject)Instantiate(P_Wall,new Vector3(i,9,z),Quaternion.Euler(0,0,0)))
				.transform.parent= this.transform;;
		}
		for (int j = -1; j < 10; j++) {
			((GameObject)Instantiate(P_Wall,new Vector3(-1,j,z),Quaternion.Euler(0,0,90)))
				.transform.parent= this.transform;
			((GameObject)Instantiate(P_Wall,new Vector3(15	,j,z),Quaternion.Euler(0,0,270)))
				.transform.parent= this.transform;
		}
	}
}

