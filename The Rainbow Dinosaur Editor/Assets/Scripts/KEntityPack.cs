using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class KEntityPack : MonoBehaviour
{
	public EditorPiece entity;
	public List<Sprite> sprites; //sprite that will be patched 

	// Use this for initialization
	void Start ()
	{
	
	}
	public EditorPiece Get(int n){
		var e = Instantiate (entity);
		e.meRenderer.sprite = sprites [n];
		return e;
	}
	
	public Sprite GetSprite(int n){
		return sprites [n];;
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}

