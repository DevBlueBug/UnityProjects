using UnityEngine;
using System.Collections;

public class BoardRenderer : MonoBehaviour
{
	public TextureSheet sheet;
	public SpriteRenderer sprRenderer00;
	public SpriteRenderer sprRenderer01;

	// Use this for initialization
	void Start ()
	{
		Texture2D texture = new Texture2D (1500,900);

		//Sprite sprite = new Sprite ();
		var sprite00 = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (.5f, .5f));
		var sprite01 = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (.5f, .5f),100);

		sprRenderer00.sprite = sprite00;
		sprRenderer01.sprite = sprite01;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

