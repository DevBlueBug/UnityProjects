using UnityEngine;
using System.Collections;

public class RenderUnit : RenderEntity
{
	
	new public delegate void D_ME(RenderUnit me);
	new public D_ME E_Clicked =delegate {};


	public NWorld.NEntity.NUnit.Unit Unit {
		get{
			return (NWorld.NEntity.NUnit.Unit) this.entity;
		}
	}
	new public RenderUnit Init(NWorld.NEntity.Entity entity){
		base.Init (entity);
		return this;
	}
	
	void OnMouseDown(){
		E_Clicked (this);
		Debug.Log (this.gameObject.name + " MOUSE DOWN ");
	}
}

