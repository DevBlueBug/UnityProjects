using UnityEngine;
using System.Collections.Generic;

public class RenderRoomWalls : MonoBehaviour
{
	[System.Serializable]
	public class WallPieces
	{
		public Sprite up, down, left, right;	
		public List<Sprite> shadows;

	}
	public List<WallPieces> examples;

	public void Init(Transform parent,  int wallLayer, int wallorder, int shadowLayer, int shadowOrder,float w, float h){
		var me = new GameObject ("RenderRoomWalls");
		me.transform.parent = parent;
		me.transform.localPosition = Vector3.zero;
		me.transform.rotation = Quaternion.identity;

		var theme = examples [Random.Range (0, examples.Count)];
		InitWalls (theme, me.transform, w, h, wallLayer, wallorder);
		InitShadows (theme, me.transform, w, h, shadowLayer, shadowOrder);
	}
	 void InitWalls(WallPieces theme, Transform parent, float w, float h, int layer, int order){
		helperConvert (parent,w,h,layer,order, 0, theme.up);
		helperConvert (parent,w,h,layer,order, 1, theme.right);
		helperConvert (parent,w,h,layer,order, 2, theme.down);
		helperConvert (parent,w,h,layer,order, 3, theme.left);
	}
	 void InitShadows(WallPieces theme, Transform parent, float w, float h, int layer, int order){
		for (int y = 0; y<h; y++) {
			helperSprtieRenderer(parent,w,h,layer,order,theme.shadows[Random.Range(0,theme.shadows.Count)], 0,y);
			helperSprtieRenderer(parent,w,h,layer,order,theme.shadows[Random.Range(0,theme.shadows.Count)], w-1,y);
			
		}
		for (int x = 0; x<w; x++) {
			helperSprtieRenderer(parent,w,h,layer,order,theme.shadows[Random.Range(0,theme.shadows.Count)], x,0);
			helperSprtieRenderer(parent,w,h,layer,order,theme.shadows[Random.Range(0,theme.shadows.Count)], x,h-1);
			
		}

	}
	
	SpriteRenderer helperConvert(Transform parent,float width, float height, 
	                             int layer, int order, 
	                             int direction, Sprite spr){
		var obj = new GameObject ("Sprite").AddComponent<SpriteRenderer> ();
		obj.sprite = spr;
		obj.transform.parent = parent;
		obj.gameObject.layer = layer;
		obj.sortingOrder = order;
		obj.transform.rotation = Quaternion.Euler (0, 0, Utility.EasyUnity.angleFour [direction]);
		
		switch (direction) {
		case 0: obj.transform.localPosition = new Vector3 (width *.5f -.5f, height-.5f,0);break;
		case 1: obj.transform.localPosition = new Vector3 (width -.5f, height * .5f-.5f ,0);break;
		case 2: obj.transform.localPosition = new Vector3 (width *.5f -.5f, -.5f,0);break;
		case 3: obj.transform.localPosition = new Vector3 (-.5f, height*.5f-.5f,0);break;
		}
		return obj;
	}
	SpriteRenderer helperSprtieRenderer(Transform parent, float width,float height, int layer, int order, Sprite spr, float x, float y){
		int direction = 0;
		var obj = new GameObject ("Sprite").AddComponent<SpriteRenderer> ();
		obj.sprite = spr;
		obj.transform.parent = parent;
		obj.gameObject.layer = layer;
		obj.color = new Color (.5f, .5f, .5f);
		obj.sortingOrder = order;
		if (x == 0) {
			x -= (spr.bounds.extents.y - .5f);
			direction = 3;
		}
		else if (x == width - 1) {
			x += (spr.bounds.extents.y - .5f);
			direction = 1;
		}
		if (y == 0) {
			y -= (spr.bounds.extents.y - .5f);
			direction = 2;
		}
		else if (y == height -1 ) {
			y += (spr.bounds.extents.y - .5f);
			direction = 0;
		}
		obj.transform.localPosition = new Vector3 (x, y, 0);
		obj.transform.rotation = Quaternion.Euler(0, 180* Random.Range(0,2)  ,0)* Quaternion.Euler (0, 0, Utility.EasyUnity.angleFour [direction]);
		return obj;
	}
}

