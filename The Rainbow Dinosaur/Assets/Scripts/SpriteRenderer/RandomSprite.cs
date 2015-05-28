using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSprite : MonoBehaviour
{
	public SpriteRenderer render;
	public List<Sprite> sprites;
	// Use this for initialization
	public void Awake(){
		render.sprite = sprites[Random.Range(0,sprites.Count)];

	}
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

