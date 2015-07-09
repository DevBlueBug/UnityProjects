using UnityEngine;
using System.Collections.Generic;

public class RenderRoomFloor : MonoBehaviour {

	//public DC_Flower DC_Flower;
	
	
	public Color 
		colorBackground,
		colorBackgroundTexture,
		colorFrontground,
		colorShadow;
	public SpriteRenderer P_Background,P_Frontground;
	public List<Sprite>
		shadowsEdge,
		shadowsFloor,
		sprsEdges,
		SprsBig,
		sprsSmall;

	List<SpriteRenderer> decorators = new List<SpriteRenderer>();
	// Use this for initialization
	public void Init(Transform parent, int layer, int sorting, int shadowLayer, int shadowSorting, float width, float height	){
		InitBackgroundForeground (parent, layer, sorting,sorting +3, width, height);
		InitShadows(parent, layer, sorting+1, width, height, colorBackgroundTexture );
	}
	void InitBackgroundForeground(Transform parent, int layer, int sortingBackground,int sortingFrontground, float width, float height){
		var background = Instantiate (P_Background);
		var frontground = Instantiate (P_Frontground);
		var pos = new Vector3 ((width-1)/2,(height-1)/2,0);
		var scale = new Vector3 (width-2,height-2,1);
		background.gameObject.layer = layer;
		background.color = colorBackground;
		background.transform.parent = parent;
		background.sortingOrder = sortingBackground;
		background.transform.localPosition = pos;
		background.transform.localScale = scale;
		
		frontground.gameObject.layer = layer;
		frontground.color = colorFrontground;
		frontground.transform.parent = parent;
		frontground.sortingOrder = sortingFrontground ;
		frontground.transform.localPosition = pos;
		frontground.transform.localScale = scale;

	}
	void InitShadows(Transform parent, int layer, int sorting, float width, float height, Color color){

		for(int i = 1; i < width - 2 ; i++){
			helperGetSpriteRenderer(parent, layer,sorting, color, 
			                        new Vector3(i+.5f, -.5f +(height -2 ),0) , 
			                        Quaternion.Euler(0,180f * Random.Range(0,2),0.0f),
			                        shadowsEdge[Random.Range(0, shadowsEdge.Count) ]) ;
			
			helperGetSpriteRenderer(parent, layer,sorting, color, 
			                        new Vector3(i+.5f, 1.5f,0) , 
			                        Quaternion.Euler(0,0,180 )*
			                        Quaternion.Euler(0,180f * Random.Range(0,2),0),
			                        shadowsEdge[Random.Range(0, shadowsEdge.Count) ]) ;

		}
		for(int j = 1 ; j < height -2 ; j++){
			helperGetSpriteRenderer(parent, layer,sorting, color, 
			                        new Vector3(1.5f, j + .5f,0) , 
			                        Quaternion.Euler(0,0,90 )*
			                        Quaternion.Euler(0,180f * Random.Range(0,2),0),
			                        shadowsEdge[Random.Range(0, shadowsEdge.Count) ]) ;
			helperGetSpriteRenderer(parent, layer,sorting, color, 
			                        new Vector3(width - 2.5f, j + .5f,0) , 
			                        Quaternion.Euler(0,0,270 )*
			                        Quaternion.Euler(0,180f * Random.Range(0,2),0),
			                        shadowsEdge[Random.Range(0, shadowsEdge.Count) ]) ;
		}
		for (int i = 2; i < width - 3; i++) {
			for (int j = 2; j < height -3; j++) {
				helperGetSpriteRenderer (parent, layer, sorting, color, 
				                        new Vector3 ( i + .5f, j + .5f, 0), 
				                         Quaternion.Euler (180f * Random.Range(0,2), 180f * Random.Range(0,2), Random.Range(0,360) ),
				                         shadowsFloor [Random.Range (0, shadowsFloor.Count)]);
			}
		}

	}
	SpriteRenderer helperGetSpriteRenderer(Transform parent, int layer, int sortingOrder, Color color, Vector3 pos, Quaternion rotation, Sprite sprite ){
		var obj = new GameObject("Shadow").AddComponent<SpriteRenderer>();
		obj.transform.parent = parent;
		obj.gameObject.layer = layer;
		obj.sprite = sprite;
		obj.color = color;
		obj.sortingOrder = sortingOrder;
		obj.transform.localPosition = pos;
		obj.transform.localRotation = rotation;
		return obj;
	}
	void Starst () {
		/**
		int nLarge = Random.Range (3, 4);
		for (int i = 90; i< 2; i++) {
			var spr = HelperToSprite(sprsEdges[Random.Range(0,sprsEdges.Count)],layerInt,sprOrder,
			                         new Vector3(Random.Range(3,12),Random.Range(2,7), 0));
			spr.transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
			spr.transform.localScale = new Vector3(Random.Range(11.5f,15.0f),Random.Range(3.5f,5.0f),1); 
			spr.material.color = new Color(.5f,.5f,.5f);
			                       
		
		}
		RenderEdges (sprsEdges, layerInt, sprOrder+3, new Color(.5f,.5f,.5f));
		//RenderEdgesInner(SprsBig, layerInt, sprOrder+1,new Color(.4f,.4f,.4f) );
		return;
		RenderEdgesInside(sprsSmall, layerInt, sprOrder+2);
		return;
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
		**/
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
	
	List<SpriteRenderer> RenderEdges(List<Sprite> sprites, int layerInt, int sprOrder, Color color){
		List<SpriteRenderer> renderers = new List<SpriteRenderer>();
		for (int y = 0; y < 9; y++) {
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(-.5f,y,0)));
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(14.5f,y,0)));
		}
		for (int x = 0; x < 15; x++) {
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,-.5f,0)));
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,8.5f,0)));
		}
		for (int i = 0; i < renderers.Count; i++) {
			renderers[i].color = color;
		}
		
		return renderers;
	}
	
	List<SpriteRenderer> RenderEdgesInner(List<Sprite> sprites, int layerInt, int sprOrder, Color color){
		List<SpriteRenderer> renderers = new List<SpriteRenderer>();
		for (int y = 3; y < 6; y++) {
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(1.5f,y,0)));
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(12.5f,y,0)));
		}
		for (int x = 1; x < 13; x++) {
			
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,1.5f,0)));
			renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,sprOrder,new Vector3(x,6.5f,0)));
		}
		for (int i = 0; i < renderers.Count; i++) {
			renderers[i].color = color;
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
		//List<SpriteRenderer> renderers = new List<SpriteRenderer> ();
		//for (int i = 0; i< count; i++) {
		//	var pos = new Vector3(Random.Range(1,width-1), Random.Range(1,height-1),0);
		//	renderers.Add(HelperToSprite(sprites[Random.Range(0,sprites.Count) ],layerInt,order,pos));
		//}
		//return renderers;
		return null;
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
		/**
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
		**/
	}

	// Update is called once per frame
	void Update () {
		var dis = PlayerManager.PlayerPosFloat - new Vector3(7,4,0);
		//this.transform.position = dis * -.02f;
		//Debug.Log ("WTF" + Input.GetKey (KeyCode.W));
		if (Input.GetKeyDown (KeyCode.W)) {
			//Debug.Log("DOWN");
			//ResolveCollision(this.decorators);
		}
	
	}
}
