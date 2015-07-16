using UnityEngine;
using System.Collections.Generic;

public class EBitmask : MonoBehaviour
{
	public Entity entity;
	public SpriteRenderer sprRenderer;
	public List<Sprite> sprites;
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
			if(entityNew.id == entity.id) 
				score += socreAdded[i]; 
		}
		sprRenderer.sprite = sprites [score];

	}
}

