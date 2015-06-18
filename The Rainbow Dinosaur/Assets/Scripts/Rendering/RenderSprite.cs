using UnityEngine;
using System.Collections.Generic;

public class RenderSprite : MonoBehaviour
{

	public List<SpriteRenderer> sprites; 
	public Color color {
		set {
			for(int i = 0;  i< sprites.Count;i++){
				sprites[i].color = value;
			}
		}
		get{
			return sprites[0].color;
		}
		
	}
	public List<Color> colors {
		set{
			for(int i = 0 ; i< value.Count;i++){
				sprites[i].color = value[i];
			}
		}
		get{
			List<Color> colors = new List<Color>();
			for(int i = 0;  i< sprites.Count;i++){
				colors.Add(sprites[i].color);
			}
			return colors;
		}
		
	}
	public int layer{
		set{
			for(int i = 0;  i< sprites.Count;i++){
				sprites[i].gameObject.layer = value;
			}
		}
		get{
			return this.gameObject.layer;
		}
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

