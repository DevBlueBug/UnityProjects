using UnityEngine;
using System.Collections;

public class EBitamsk_Simple : MonoBehaviour
{
	static int[] SCORES_SPR01 = new int[]{
		0,1,2,3,8,9,10,11};
	public Entity entity;
	public SpriteRenderer sprRenderer;
	public Sprite spr00,spr01;
	void Awake(){
		this.entity.E_Refreshed += H_Refresh;
	}
	void H_Refresh(Entity entity, Room room){
		int score = 0;
		int[] socreAdded = new int[]{1,2,4,8};
		for (int i= 0; i <  4; i++) {
			var posNew = entity.transform.localPosition + 
					Utility.EasyUnity.dirFour3[i];
			if(posNew.x < 0 || posNew.y< 0 
			   || posNew.x >= room.width||posNew.y >= room.height) continue;

			var entityNew = room.entitiesWorldIndex[(int)posNew.x,(int)posNew.y];
			if(entityNew == null) continue;
			if(entityNew.meType == entity.meType) 
				score += socreAdded[i]; 
		}
		bool isSpir00 = true;
		foreach (var scoreCompare in SCORES_SPR01) {
			if(scoreCompare == score){
				isSpir00 = false;
				break;
			}
		}
		this.sprRenderer.sprite = (isSpir00) ? spr00 : spr01;

	}
}

