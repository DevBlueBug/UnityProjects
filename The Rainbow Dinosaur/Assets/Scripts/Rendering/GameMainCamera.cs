using UnityEngine;
using System.Collections;

public class GameMainCamera : MonoBehaviour
{
	public Camera cam;
	//float unitPerPixel = 1/50.0f;
	Vector3 center = new Vector3 (7,4,0);
	// Use this for initialization
	void Awake ()
	{
		PlayerManager.E_PlayerPositionChanged += PlayerPositionChanged;
		cam.orthographicSize = 640 / (50.0f*2); // height / pixels*2
	}
	public static float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
	{
		float valueInPixels = (640 / (viewingCamera.orthographicSize * 2)) * unityUnits;
		valueInPixels = Mathf.Round(valueInPixels);
		float adjustedUnityUnits = valueInPixels / (640 / (viewingCamera.orthographicSize * 2));
		return adjustedUnityUnits;
	}

	void PlayerPositionChanged(Vector3 pos){
		Vector3 dis = pos - center;
		var position = center + dis * .05f;


		//var posNew = new Vector3 (position.x,position.y,this.transform.localPosition.z);

		this.transform.localPosition = new Vector3 (RoundToNearestPixel(position.x, cam),
		                                            RoundToNearestPixel(position.y, cam	),
		                                            this.transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

