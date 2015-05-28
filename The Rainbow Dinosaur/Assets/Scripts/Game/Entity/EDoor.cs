using UnityEngine;
using System.Collections;

public class EDoor : Entity
{
	public delegate void D_OpenClose (bool isOpen);
	public D_OpenClose E_OpenClose = delegate {};
	public Collider2D meCollider;

	public void SetOpen(bool isOpen){
		E_OpenClose (isOpen);
		meCollider.gameObject.SetActive (!isOpen);
	}
}

