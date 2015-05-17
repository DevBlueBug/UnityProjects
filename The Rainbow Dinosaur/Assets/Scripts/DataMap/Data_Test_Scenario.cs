using UnityEngine;
using System.Collections;

public class Data_Test_Scenario : MonoBehaviour
{
	public GameObject PrefabTest;
	// Use this for initialization
	void Start ()
	{
		var map = new Data.DMapGenerator().GenerateMap (10,10,10);
		for(int x=0; x< map.width;x++)
		for(int y= 0 ; y < map.height;y++){
			if(map[x,y]==null) continue;
			var prefab = Instantiate(PrefabTest);
			prefab.transform.position = new Vector3(x,y,0) - new Vector3(5,5,0);
		}
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

