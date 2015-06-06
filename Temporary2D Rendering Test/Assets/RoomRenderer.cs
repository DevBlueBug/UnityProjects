using UnityEngine;
using System.Collections.Generic;

public class RoomRenderer : MonoBehaviour {
	
	int width = 15, height = 9;
	public Sprite SPR_Floor;
	public RoomDecorator testDecorator;

	List<RoomDecorator> decorators = new List<RoomDecorator>();
	// Use this for initialization
	void Start () {
		for(int i = 0; i < width ; i++)for(int j = 0;  j  < height;j++){
			GameObject obj = new GameObject("Floor " + i +  " " + j);
			obj.transform.parent = this.transform;
			obj.transform.localPosition = new Vector3(i,j,0);
			obj.AddComponent<SpriteRenderer>().sprite = SPR_Floor;
		}
		for (int i = 0; i < 2; i++) {
			var d = Instantiate(testDecorator);
			decorators.Add(d);
			d.transform.parent = this.transform;
			d.transform.localPosition =
				new Vector3(Random.Range(1,width-1), Random.Range(1,height-1),0);
			//Random.seed = (int)(Time.time*10.0f);
		}
		ResolveCollision (decorators);

		//Destroy (this);
	
	}
	void ResolveCollision(List<RoomDecorator> decorators){
		bool isContinue = true;
		int count = 0;
		while (count++<  100 && isContinue) {
			isContinue = false;
			for (int i = 0; i< decorators.Count; i++) {
				for (int j = i+1; j< decorators.Count; j++) {
					if( Resolve (decorators [i], decorators [j]))
						isContinue = true;
				}
			}
		}
		Debug.Log("CYCLED"  +(count-1));
	}
	bool Resolve(RoomDecorator a, RoomDecorator b){
		var minDistance = a.Bound+ b.Bound;
		var distance = a.transform.localPosition - b.transform.localPosition;
		var distanceOverlapping = new Vector3 (minDistance.x - Mathf.Abs(distance.x),
		                                       minDistance.y - Mathf.Abs(distance.y),
		                                       0);
		bool 
			isX = distanceOverlapping.x >0, isY = distanceOverlapping.y>0;
		if (!isX && !isY) return false;
		if (isX && isY) {
			
			if( distanceOverlapping.x < distanceOverlapping.y ){
				distanceOverlapping.x = 0;
			}
			else {
				distanceOverlapping.y = 0;
			}
		}
		if (isX && isY) {
			var distanceMove = new Vector3 ((isX)? 
			                                distanceOverlapping.x/2 
			                                * ((a.transform.localPosition.x>b.transform.localPosition.x)? 1:-1 ) :0,
			                                (isY)? distanceOverlapping.y/2
			                                * ((a.transform.localPosition.y>b.transform.localPosition.y)? 1:-1 ) :0,

			                                0);
			a.transform.localPosition += distanceMove;
			b.transform.localPosition -= distanceMove;
		}
		return isX && isY;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("WTF" + Input.GetKey (KeyCode.W));
		if (Input.GetKeyDown (KeyCode.W)) {
			Debug.Log("DOWN");
			ResolveCollision(this.decorators);
		}
	
	}
}
