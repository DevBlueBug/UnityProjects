using UnityEngine;
using System.Collections.Generic;

public class EdgeReposition : MonoBehaviour
{
	float[] edgeRotations = new float[]{0,90,180,270};
	public Entity entity;
	public SpriteRenderer sprRenderer;

	public List<Sprite>
		sprWalls,
		sprCorners;
	void Awake(){
		this.entity.E_Refreshed += H_Refresh;
	}
	void WallTransition(float width, float height){
		if (entity.posY == height - 1) {
			entity.transform.localRotation = Quaternion.Euler (0, 0, 0);
			sprRenderer.sprite = sprWalls [0];
		}
		else if (entity.posX == width - 1) {
			entity.transform.localRotation = Quaternion.Euler (0, 0, 270);
			sprRenderer.sprite = sprWalls [1];
		} 
		else if (entity.posY == 0) {
			entity.transform.localRotation = Quaternion.Euler (0, 0, 180);
			sprRenderer.sprite = sprWalls [2];
		} else if (entity.posX == 0) {
			entity.transform.localRotation = Quaternion.Euler (0, 0, 90);
			sprRenderer.sprite = sprWalls [3];
		} 
	}
	void EdgeTransition(int index){
		
		sprRenderer.sprite = sprCorners[index];
		//is edge
		entity.transform.localRotation = Quaternion.Euler(0,0,edgeRotations[index]);
		//switch to edge sprite
		sprRenderer.sprite = sprCorners[index];
		sprRenderer.sortingOrder = 100;
		return;

	}
	void H_Refresh(Entity entity, Room room){

		float[][] edgePositions = new float[][]{
			new float[]{0,room.height-1},
			new float[]{room.width-1,room.height-1},
			new float[]{room.width-1,0},
			new float[]{0,0}
		};
		int edgeIndex = -1;
		for (int i = 0; i < 4; i++) {
			var edgePosition = edgePositions[i];
			if(entity.posX == edgePosition[0] && 
			   entity.posY == edgePosition[1]){
				edgeIndex = i;
				break;
			}
		}
		if(edgeIndex != -1) EdgeTransition(edgeIndex);
		else WallTransition(room.width,room.height);
		sprRenderer.transform.rotation = Quaternion.Euler(Vector3.zero);

		
	}
}

