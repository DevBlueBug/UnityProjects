using UnityEngine;
using System.Collections;

public class TextureCreator : MonoBehaviour
{
	public Noise.NoiseMethodType type;

	[Range(1,3)]
	public int dimensions = 3;
	[Range(2,512)]
	public int resolution = 256;
	public float frequency =1;
	private Texture2D texture;
	// Use this for initialization

	private void OnEnable () {
		if (texture == null) {
			
			texture = new Texture2D(resolution, resolution, TextureFormat.RGB24,false);
			texture.name = "Procedural Texture";
			texture.wrapMode = TextureWrapMode.Clamp;
			texture.filterMode = FilterMode.Point;
			texture.anisoLevel = 9;
			GetComponent<MeshRenderer>().material.mainTexture = texture;
		}
		FillTexture();
	}

	public void FillTexture () {
		if (texture.width != resolution) {
			texture.Resize(resolution,resolution);
		}
		Vector3 point00 = transform.TransformPoint(new Vector3(-0.5f,-0.5f) );
		Vector3 point01 = transform.TransformPoint(new Vector3(-0.5f, 0.5f) );
		Vector3 point10 = transform.TransformPoint(new Vector3( 0.5f,-0.5f) );
		Vector3 point11 = transform.TransformPoint(new Vector3( 0.5f, 0.5f) );

		float stepSize = 1.0f / resolution;
		Noise.NoiseMethod method = Noise.valueMethods [dimensions - 1];
		for (int y = 0; y < resolution; y++) {
			
			Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
			Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);

			for (int x = 0; x < resolution; x++) {
				Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
				texture.SetPixel(x, y, Color.white * method(point,frequency)  );
				//texture.SetPixel(x, y, new Color(point.x, point.y,point.z ) );
				//texture.SetPixel(x, y,
				//       new Color((x+.5f) * stepSize%.1f ,(y+.5f)*stepSize%.1f ,0.0f) * 10.0f);
			}
		}
		texture.Apply();
	}
	void Update () {
		if (transform.hasChanged) {
			transform.hasChanged = false;
			FillTexture();
		}
	}
}

