using UnityEngine;
using System.Collections;

public class EDoor : Entity
{
	public D_TriggerTarget 
		E_TriggerFront = delegate {return 1;},
		E_TriggerBack = delegate {return 1;};

	public delegate void D_OpenClose (bool isOpen);
	public D_OpenClose E_OpenClose = delegate {};
	public Collider2D 
		meCollider,
		triggerFront,
		triggerBack;

	public Data.DDoor.Types type;
	public int keyRequired = 0;

	public override void Awake ()
	{
		base.Awake ();
		E_TriggerTarget += H_TriggerEnter;
		switch (type) {
		case Data.DDoor.Types.Treausre:
		case Data.DDoor.Types.Secret:
			keyRequired++;
			break;
		}
	}
	int H_TriggerEnter(Entity me,Entity other, Collider2D collider){
		
		if (triggerBack.bounds.Intersects (collider.bounds)) {
			return E_TriggerBack(me,other,collider);
		}
		if (triggerFront.bounds.Intersects (collider.bounds)){
			return E_TriggerFront (me, other, collider);
		}
		return 1;
	}

	public void SetOpen(bool isOpen){

		if (isOpen && keyRequired != 0) return;
		E_OpenClose (isOpen);
		meCollider.gameObject.SetActive (!isOpen);
	}
}

