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

}

