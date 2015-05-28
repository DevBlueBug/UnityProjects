using UnityEngine;
using System.Collections.Generic;

public class TextureSheet : MonoBehaviour
{
	public int w, h;
	public Texture2D texture;
	int width, height;
	List<Color[]> colors;
	public void Awake(){
		colors = new List<Color[]> ();
		width = texture.width / w;
		height = texture.height / h;
		for (int i = 0; i < width; i++)for (int j = 0; j < height; j++) {
			colors.Add(texture.GetPixels(width*i,height*j,width,height));
		}
	}
	public Color[] Get(int n){
		return colors[n%colors.Count];
	}
}

