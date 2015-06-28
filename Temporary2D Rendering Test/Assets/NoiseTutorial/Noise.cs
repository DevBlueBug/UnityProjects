using UnityEngine;
using System.Collections;

public static class Noise
{
	public enum NoiseMethodType {
		Value,
		Perlin
	}

	private static int[] hash = {
		151,160,137, 91, 90, 15,131, 13,201, 95, 96, 53,194,233,  7,225,
		140, 36,103, 30, 69,142,  8, 99, 37,240, 21, 10, 23,190,  6,148,
		247,120,234, 75,  0, 26,197, 62, 94,252,219,203,117, 35, 11, 32,
		57,177, 33, 88,237,149, 56, 87,174, 20,125,136,171,168, 68,175,
		74,165, 71,134,139, 48, 27,166, 77,146,158,231, 83,111,229,122,
		60,211,133,230,220,105, 92, 41, 55, 46,245, 40,244,102,143, 54,
		65, 25, 63,161,  1,216, 80, 73,209, 76,132,187,208, 89, 18,169,
		200,196,135,130,116,188,159, 86,164,100,109,198,173,186,  3, 64,
		52,217,226,250,124,123,  5,202, 38,147,118,126,255, 82, 85,212,
		207,206, 59,227, 47, 16, 58, 17,182,189, 28, 42,223,183,170,213,
		119,248,152,  2, 44,154,163, 70,221,153,101,155,167, 43,172,  9,
		129, 22, 39,253, 19, 98,108,110, 79,113,224,232,178,185,112,104,
		218,246, 97,228,251, 34,242,193,238,210,144, 12,191,179,162,241,
		81, 51,145,235,249, 14,239,107, 49,192,214, 31,181,199,106,157,
		184, 84,204,176,115,121, 50, 45,127,  4,150,254,138,236,205, 93,
		222,114, 67, 29, 24, 72,243,141,128,195, 78, 66,215, 61,156,180,
		
		151,160,137, 91, 90, 15,131, 13,201, 95, 96, 53,194,233,  7,225,
		140, 36,103, 30, 69,142,  8, 99, 37,240, 21, 10, 23,190,  6,148,
		247,120,234, 75,  0, 26,197, 62, 94,252,219,203,117, 35, 11, 32,
		57,177, 33, 88,237,149, 56, 87,174, 20,125,136,171,168, 68,175,
		74,165, 71,134,139, 48, 27,166, 77,146,158,231, 83,111,229,122,
		60,211,133,230,220,105, 92, 41, 55, 46,245, 40,244,102,143, 54,
		65, 25, 63,161,  1,216, 80, 73,209, 76,132,187,208, 89, 18,169,
		200,196,135,130,116,188,159, 86,164,100,109,198,173,186,  3, 64,
		52,217,226,250,124,123,  5,202, 38,147,118,126,255, 82, 85,212,
		207,206, 59,227, 47, 16, 58, 17,182,189, 28, 42,223,183,170,213,
		119,248,152,  2, 44,154,163, 70,221,153,101,155,167, 43,172,  9,
		129, 22, 39,253, 19, 98,108,110, 79,113,224,232,178,185,112,104,
		218,246, 97,228,251, 34,242,193,238,210,144, 12,191,179,162,241,
		81, 51,145,235,249, 14,239,107, 49,192,214, 31,181,199,106,157,
		184, 84,204,176,115,121, 50, 45,127,  4,150,254,138,236,205, 93,
		222,114, 67, 29, 24, 72,243,141,128,195, 78, 66,215, 61,156,180
	};
	private static float[] gradients1D = {
		1f, -1f
	};
	
	private const int gradientsMask1D = 1;
	public static NoiseMethod[] perlinMethods = {
		Perlin1D,
		Perlin2D,
		Perlin3D
	};
	
	public static NoiseMethod[][] noiseMethods = {
		valueMethods,
		perlinMethods
	};


	public delegate float NoiseMethod (Vector3 point, float frequency);
	public static NoiseMethod[] valueMethods = {
		Value1D,
		Value2D,
		Value3D
	};
	
	private const int hashMask = 255;

	private static float Smooth (float t) {
		return t * t * t * (t * (t * 6f - 15f) + 10f);
	}
	
	public static float Perlin1D (Vector3 point, float frequency) {
		point *= frequency;
		Vector3 i0 = new Vector3 (Mathf.FloorToInt (point.x),Mathf.FloorToInt (point.y),Mathf.FloorToInt (point.z) );
		Vector3 t0 = point - i0,
				t1 = t0 - Vector3.one;
		//t0 = new Vector3 (Smooth(t0.x), Smooth(t0.y),Smooth(t0.z));
		i0 = new Vector3 (((int)i0.x) & hashMask,(int)i0.y & hashMask,(int)i0.z & hashMask);
		Vector3 i1 = i0 + new Vector3 (1, 1, 1);
		
		int[] hX = 		new int[]{hash[(int)i0.x] , hash[(int)i1.x] }; // 2
		
		float vX = Mathf.Lerp (hX [0], hX [1], t0.x) / (float)hashMask;

		float g0 = gradients1D[hash[(int)i0.x] & gradientsMask1D];
		float g1 = gradients1D[hash[(int)i1.x] & gradientsMask1D];
		
		float v0 = g0 * t0.x;
		float v1 = g1 * t1.x;


		//return t0.x;
		//return Smooth (t0.x);
		//return Mathf.Lerp (hX [0], hX [1], Smooth(t0.x ) ) / (float)hashMask;
		//return g1;
		return .5f + Mathf.Lerp (v0, v1, Smooth(t0.x ));//*2.0f;
		//return (.5f	+(Mathf.Lerp(v0 , v1, Smooth(t0.x) )*2.0f) *.5f);
	}
	
	public static float Perlin2D (Vector3 point, float frequency) {
		return 0;
	}
	
	public static float Perlin3D (Vector3 point, float frequency) {
		return 0;
	}

	// t = new Vector3 (Smooth(t.x), Smooth(t.y),Smooth(t.z));
	public static float Value1D(Vector3 point,float frequency){
		point *= frequency;
		Vector3 i0 = new Vector3 (Mathf.FloorToInt (point.x),Mathf.FloorToInt (point.y),Mathf.FloorToInt (point.z) );
		Vector3 t = point - i0;
		t = new Vector3 (Smooth(t.x), Smooth(t.y),Smooth(t.z));
		i0 = new Vector3 (((int)i0.x) & hashMask,(int)i0.y & hashMask,(int)i0.z & hashMask);
		Vector3 i1 = i0 + new Vector3 (1, 1, 1);
		
		int[] hX = 		new int[]{hash[(int)i0.x] , hash[(int)i1.x] }; // 2

		float vX = Mathf.Lerp (hX [0], hX [1], t.x) / (float)hashMask;
		return vX;
	}
	public static float Value2D(Vector3 point,float frequency){
		point *= frequency;
		Vector3 i0 = new Vector3 (Mathf.FloorToInt (point.x),Mathf.FloorToInt (point.y),Mathf.FloorToInt (point.z) );
		Vector3 t = point - i0;
		t = new Vector3 (Smooth(t.x), Smooth(t.y),Smooth(t.z));
		i0 = new Vector3 (((int)i0.x) & hashMask,(int)i0.y & hashMask,(int)i0.z & hashMask);
		Vector3 i1 = i0 + new Vector3 (1, 1, 1);
		
		int[] hX = 		new int[]{hash[(int)i0.x] , hash[(int)i1.x] }; // 2
		int[] hXY0 =	new int[]{hash[hX[0] + (int)i0.y],hash[hX[1] + (int)i0.y]  }; // 2
		int[] hXY1 =	new int[]{hash[hX[0] + (int)i1.y],hash[hX[1] + (int)i1.y]  }; // 4
		
		float vX = Mathf.Lerp (hX [0], hX [1], t.x) / (float)hashMask;
		float vXY = Mathf.Lerp(Mathf.Lerp (hXY0 [0], hXY0 [1], t.x), 
		                       Mathf.Lerp (hXY1 [0], hXY1 [1], t.x),
		                       t.y) / (float)hashMask;
		return vXY;
	}
	public static float Value3D (Vector3 point, float frequency) {
		point *= frequency;
		Vector3 i0 = new Vector3 (Mathf.FloorToInt (point.x),Mathf.FloorToInt (point.y),Mathf.FloorToInt (point.z) );
		Vector3 t = point - i0;
		t = new Vector3 (Smooth(t.x), Smooth(t.y),Smooth(t.z));
		i0 = new Vector3 (((int)i0.x) & hashMask,(int)i0.y & hashMask,(int)i0.z & hashMask);
		Vector3 i1 = i0 + new Vector3 (1, 1, 1);
		
		int[] hX = 		new int[]{hash[(int)i0.x] , hash[(int)i1.x] }; // 2
		int[] hXY0 =	new int[]{hash[hX[0] + (int)i0.y],hash[hX[1] + (int)i0.y]  }; // 2
		int[] hXY1 =	new int[]{hash[hX[0] + (int)i1.y],hash[hX[1] + (int)i1.y]  }; // 4
		int[] hXYZ00 =	new int[]{hash[hXY0[0]+(int)i0.z]  ,hash[hXY0[1] +(int)i0.z]};
		int[] hXYZ01 =	new int[]{hash[hXY1[0]+(int)i0.z]  ,hash[hXY1[1] +(int)i0.z]};
		int[] hXYZ10 =	new int[]{hash[hXY0[0]+(int)i1.z]  ,hash[hXY0[1] +(int)i1.z]};
		int[] hXYZ11 =	new int[]{hash[hXY1[0]+(int)i1.z]  ,hash[hXY1[1] +(int)i1.z]}; // 8
		
		float vX = Mathf.Lerp (hX [0], hX [1], t.x) / (float)hashMask;
		float vXY = 
			Mathf.Lerp (
				Mathf.Lerp (	Mathf.Lerp (hXYZ00 [0], hXYZ00 [1], t.x), 
		                   		Mathf.Lerp (hXYZ01 [0], hXYZ01 [1], t.x), t.y),
				Mathf.Lerp (	Mathf.Lerp (hXYZ10 [0], hXYZ10 [1], t.x), 
			         			Mathf.Lerp (hXYZ11 [0], hXYZ11 [1], t.x), t.y),
				t.z) / (float)hashMask;;

		return vXY;
	}


}

