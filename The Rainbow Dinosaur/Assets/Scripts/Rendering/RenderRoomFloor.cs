using UnityEngine;
using System.Collections.Generic;

public class RenderRoomFloor : MonoBehaviour {
	
	int width = 15, height = 9;
	public DC_Flower DC_Flower;

	public int layerInt, sprOrder;
	public List<Sprite> 
		sprsLarge,
		SprsBig,
		sprsSmall;

	List<SpriteRenderer> decorators = new List<SpriteRenderer>();
	// Use this for initialization
	void Start () {
		/**
		var large = GetRenderers (sprsLarge, layerInt, sprOrder, 25);
		var small = GetRenderers (sprsSmall, layerInt, sprOrder+1, 8);
		ResolveCollision (large);
		ResolveCollision (small);
		**/
		return;
		RenderEdges (sprsLarge, layerInt, sprOrder);
		RenderEdgesInner(SprsBig, layerInt, sprOrder+1);
		RenderEdgesInside(sprsSmall, layerInt, sprOrder+2);
		if (Random.Range (0, 3) == 0) {
			((DC_Flower)Instantiate( DC_Flower,new Vector3(Random.Range(2,3.0f),Random.Range(2,3.0f),0),Quaternion.identity)
			 ).transform.parent= this.transform;				
		}
		if (Random.Range (0, 3) == 0) {
			((DC_Flower)Instantiate( DC_Flower,new Vector3(Random.Range(2,3.0f),Random.Range(6,7.0f),0),Quaternion.identity)
			 ).transform.parent= this.transform;			
		}
		if (Random.Range (0, 3) == 0) {
			((DC_Flower)Instantiate( DC_Flower,new Vector3(Random.Range(12,13.0f),Random.Range(2,3.0f),0),Quaternion.identity)
			 ).transform.parent= this.transform;			
		}
		if (Random.Range (0, 3) == 0) {
			((DC_Flower)Instantiate( DC_Flower,new Vector3(Random.Range(12,13.0f),Random.Range(6,7.0f),0),Quaternion.identity)
			 ).transform.parent= this.transform;			
		}
		/**
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
		**/
		//ResolveCollision (decorators);

		//Destroy (this);
	}
	SpriteRenderer HelperToSprite(Sprite sprite, int layerInt, int order, Vector3 position){
		var renderer = new GameObject("Sprite").AddComponent<SpriteRenderer>();
		renderer.sortingOrder = order; 
		renderer.gameObject.layer = layerInt;
		renderer.sprite = sprite;
		renderer.transform.parent = this.transform;
		renderer.transform.localPosition = position;
		renderer.transform.rotation = Quaternion.Euler(0,0,90*	Random.Range(0,4) );
		return renderer;
	}
	
	List<SpriteRenderer> RenderEdges(List<Sprite> sprites, int layerInt, int sprOrder){
		List<SpriteRenderer> renderers = new List<SpriteRenderer>();
		for (int x = 1; x < 15; x+=2) {
			//var pos = new Vector3(x,0,0);
			
			//renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,0,0)));
			//renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,8,0)));
			
		}
		for (int y = 2; y < 7; y++) {
			//var pos = new Vector3(x,0,0);
			
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(.5f,y,0)));
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(13.5f,y,0)));
		}
		for (int x = 0; x < 15; x++) {
			
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,.5f,0)));
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,7.5f,0)));
		}
		
		return renderers;
	}
	
	List<SpriteRenderer> RenderEdgesInner(List<Sprite> sprites, int layerInt, int sprOrder){
		List<SpriteRenderer> renderers = new List<SpriteRenderer>();
		for (int x = 1; x < 15; x+=2) {
			//var pos = new Vector3(x,0,0);
			
			//renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,0,0)));
			//renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,8,0)));
			
		}
		for (int y = 3; y < 6; y++) {
			//var pos = new Vector3(x,0,0);
			
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(1.5f,y,0)));
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(12.5f,y,0)));
		}
		for (int x = 1; x < 13; x++) {
			
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,1.5f,0)));
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,6.5f,0)));
		}
		
		return renderers;
	}
	
	List<SpriteRenderer> RenderEdgesInside(List<Sprite> sprites, int layerInt, int sprOrder){
		List<SpriteRenderer> renderers = new List<SpriteRenderer>();
		for (int x = 1; x < 15; x+=2) {
			//var pos = new Vector3(x,0,0);
			
			//renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,0,0)));
			//renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,8,0)));
			
		}
		for (int y = 4; y < 5; y++) {
			//var pos = new Vector3(x,0,0);
			
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(2.5f,y,0)));
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(11.5f,y,0)));
		}
		for (int x = 2; x < 12; x++) {
			
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,2.5f,0)));
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,5.5f,0)));
		}
		
		return renderers;
	}

	public List<SpriteRenderer> GetRenderers(List<Sprite> sprites, int layerInt, int order, int count){
		List<SpriteRenderer> renderers = new List<SpriteRenderer> ();
		for (int i = 0; i< count; i++) {
			var pos = new Vector3(Random.Range(1,width-1), Random.Range(1,height-1),0);
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,order,pos));
		}
		return renderers;
	}
	
	void ResolveCollision(List<SpriteRenderer> renderers){
		bool isContinue = true;
		int count = 0;
		while (count++<  100 && isContinue) {
			isContinue = false;
			for (int i = 0; i< renderers.Count; i++) {
				for (int j = i+1; j< renderers.Count; j++) {
					if( Resolve (renderers [i], renderers [j]))
						isContinue = true;
				}
			}
			if(isContinue){
				for(int i = 0 ; i< renderers.Count;i++){
					withinBoundry(renderers[i]);
				}
			}
		}
		Debug.Log("CYCLED"  +(count-1));
	}


	
	bool Resolve(SpriteRenderer a, SpriteRenderer b){
		var minDistance = a.bounds.extents+ b.bounds.extents;
		//Debug.Log (a.bounds.extents + " " + b.bounds.extents + " " +minDistance);

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

	void withinBoundry(SpriteRenderer obj){
		Vector3 topRight = obj.transform.localPosition + obj.bounds.extents,
		bottomLeft = obj.transform.localPosition - obj.bounds.extents;
		if (topRight.y < 1) {
			//Debug.Log("Bottom");
			obj.transform.localPosition += new Vector3(0,(1-topRight.y) ,0 );
		}
		if (topRight.x < 1) {
			//Debug.Log("Bottom");
			obj.transform.localPosition += new Vector3((1-topRight.x),0,0);
		}
		if (bottomLeft.y > height-1) {
			//Debug.Log("Bottom");
			obj.transform.localPosition -= new Vector3(0,bottomLeft.y -(height-1),0);
		}
		
		if (bottomLeft.x > width-1) {
			//Debug.Log("Bottom");
			obj.transform.localPosition -= new Vector3(bottomLeft.x -(width-1),0,0);
		}
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log ("WTF" + Input.GetKey (KeyCode.W));
		if (Input.GetKeyDown (KeyCode.W)) {
			//Debug.Log("DOWN");
			ResolveCollision(this.decorators);
		}
	
	}
}
