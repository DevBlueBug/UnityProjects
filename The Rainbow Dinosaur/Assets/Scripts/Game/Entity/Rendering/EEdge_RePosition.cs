using UnityEngine;
using System.Collections.Generic;

public class EEdge_RePosition : MonoBehaviour
{
	float[] edgeRotations = new float[]{0,90,180,270};
	public Entity entity;
	public SpriteRenderer sprRendererBackground;
	public Sprite sprCorner;
	public List<Sprite> sprNormals;
	void Awake(){
		this.entity.E_Refreshed += H_Refresh;
	}
	void H_Refresh(Entity entity, Room room){

		float[][] edgePositions = new float[][]{
			new float[]{0,room.height-1},
			new float[]{0,0},
			new float[]{room.width-1,0},
			new float[]{room.width-1,room.height-1}
		};
		for (int i = 0; i < 4; i++) {
			var edgePosition = edgePositions[i];
			if(entity.posX == edgePosition[0] && 
			   entity.posY == edgePosition[1]){
				entity.transform.localRotation = Quaternion.Euler(0,0,edgeRotations[i]);
				if(sprCorner != null)sprRendererBackground.sprite = sprCorner;
				sprRendererBackground.sortingOrder = 100;
				return;
			}
		}
		//Debug.Log (this.gameObject.transform.parent.gameObject.name);
		if(sprNormals.Count !=0) sprRendererBackground.sprite = sprNormals[Random.Range(0,sprNormals.Count)];
		if (entity.posY == room.height-1)
			entity.transform.localRotation = Quaternion.Euler (0, 0, 0);
		else if(entity.posY == 0)
			entity.transform.localRotation = Quaternion.Euler (0, 0, 180);
		else if(entity.posX == 0)
			entity.transform.localRotation = Quaternion.Euler (0, 0, 90);
		else if(entity.posX == room.width-1)
			entity.transform.localRotation = Quaternion.Euler (0, 0, 270);

		
	}
}

