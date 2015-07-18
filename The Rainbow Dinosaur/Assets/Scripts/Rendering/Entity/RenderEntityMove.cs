using UnityEngine;
using System.Collections.Generic;

public class RenderEntityMove : RenderEntity
{
	public List<RenderEntityMovePart> parts;
	public EntityMove GetEntity(){
		return (EntityMove)this.entity;
	}
	public void SetTrigger(string name){
		for(int i = 0 ; i < parts.Count;i++){
			parts[i].SetTrigger(name);
		}
	}
	public void SetAniSpeed(float speed){
		for(int i = 0 ; i < parts.Count;i++){
			for(int j = 0 ; j < parts[i].animators.Count;j++){
				parts[i].animators[j].speed = speed;
			}
		}

	}
	public void AddPart(RenderEntityMovePart part){
		this.parts.Add (part);
		this.renderSprites.Add (part.renderSPR);
	}
	public void RemovePart(RenderEntityMovePart part){
		this.parts.Remove (part);
		this.renderSprites.Remove (part.renderSPR);
	}

}

