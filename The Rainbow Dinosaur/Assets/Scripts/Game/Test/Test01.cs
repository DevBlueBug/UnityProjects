using UnityEngine;
using System.Collections;

public class Test01 : MonoBehaviour
{
	public GameBrain gameBrain;
	// Use this for initialization
	void Start ()
	{
		//var map = new  Data.DMapGenerator().GenerateMap (10, 10, 10);
		//gameBrain.map = map;
		gameBrain.Init ();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

