using UnityEngine;
using System.Collections.Generic;

public class DC_Flower : MonoBehaviour
{
	public int 
		layerNum,sortingNum,
		flowerNumber,leavesNumber;
	public float distanceFlower,distanceLeaves;

	public List<Sprite> flowers,leaves; 
	// Use this for initialization
	void Start ()
	{
		//get random positions from distance flowers.
		List<Vector3> posFlowers = new List<Vector3> (){Vector3.zero};
		RenderFlowerHere (0,0);
		//flowerNumber--;

		float radius = Random.Range(0,6.3f);
		for (int i = 0; i < flowerNumber-1; i++) {
			radius += 3.14f/180 * Random.Range(30,90);
			Debug.Log("ANGLE "  + radius);
			Vector3 direction = new Vector3(Mathf.Cos(radius),Mathf.Sin(radius),0);
			Vector3 position = direction *( distanceFlower*Random.Range(.8f,1.0f));
			RenderFlowerHere(radius*(180/3.14f),distanceFlower*Random.Range(.8f,1.0f) );
		}
	
	}
	void RenderFlowerHere(float angle,float distance){
		float radius = (3.14f/180) * angle;
		Vector3 flowerPosition = Utility.EasyUnity.Vector3Direction (radius) * distance;
		Render(leaves,distanceLeaves,flowerPosition,layerNum,sortingNum,leavesNumber);
		var flower = new GameObject("flower " + angle).AddComponent<SpriteRenderer>();
		flower.gameObject.layer = layerNum;
		flower.sortingOrder = sortingNum+1;

		flower.sprite = flowers[Random.Range(0,flowers.Count)];
		flower.transform.parent= this.transform;
		flower.transform.localPosition = flowerPosition;
		flower.transform.localRotation = Quaternion.Euler(0,0,	Vector3.Angle (Vector3.right,flowerPosition)-90);// (flowerPosition, new Vector3(0,-1,0));//(0,0,angle);


		Vector3 direction = new Vector3(Mathf.Cos(radius),Mathf.Sin(radius),0),
		posLeaves = direction *( distanceFlower*Random.Range(.5f,1.0f));
	}
	void Render(List<Sprite> sprites, float distanceMax, Vector3 origin, int layer, int sorting, int count){
		float radius = Random.Range(0,6.3f);
		for (int i = 0; i < count; i++) {
			radius +=  Random.Range(30,90)*3.14f/180;
			var newPosition = Utility.EasyUnity.Vector3Direction(radius) * (distanceMax * Random.Range(.5f,1.0f));
			var obj = new GameObject("Render Object").AddComponent<SpriteRenderer>();
			obj.gameObject.layer = layer;
			obj.sortingOrder = sorting;

			obj.sprite = sprites[Random.Range(0,sprites.Count)];
			obj.transform.parent= this.transform;
			obj.transform.localPosition = origin +newPosition;
			obj.transform.localRotation = Quaternion.Euler(0,0,	Vector3.Angle (Vector3.right,newPosition)-90);//Quaternion.Euler(0,0,radius*(180/3.14f));

		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

