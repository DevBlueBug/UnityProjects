using UnityEngine;
using System.Collections;

public class RenderEyeball : MonoBehaviour
{
	public GameObject origin, eyeBall;
	public float stretchDistance,speed;



	Vector3 positionEyeball;
	float stretchDistanceSqr;


	// Use this for initialization
	void Start ()
	{
		stretchDistanceSqr = stretchDistance * stretchDistance;
		positionEyeball = eyeBall.transform.position;
	
	}
	void Update(){
		UpdateEyeballStretch ();
	}
	void UpdateEyeballStretch ()
	{
		var disPlayer = PlayerManager.PlayerPosFloat - positionEyeball;
		positionEyeball += disPlayer.normalized * speed * Time.deltaTime;


		var dis = (positionEyeball - origin.transform.position);
		if(dis.sqrMagnitude > stretchDistanceSqr){
			var dir = dis.normalized;
			positionEyeball =  origin.transform.position +   dir * stretchDistance;

		}
		eyeBall.transform.position = new Vector3(positionEyeball.x,positionEyeball.y,eyeBall.transform.position.z);
	
	}
}

