using UnityEngine;
using System.Collections;

public class ShaderUpdate : MonoBehaviour
{
	public Material mat;
	// Use this for initialization
	void Start ()
	{
		PlayerManager.E_PlayerPositionChanged += UpdateMat;
	
	}
	
	// Update is called once per frame
	void UpdateMat (Vector3 position)
	{
		mat.SetVector ("_PlayerPosition", position);
	}
}

