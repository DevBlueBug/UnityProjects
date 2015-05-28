using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public EntityMove entity;
	public Inventory inventory;
	public PlayerInformation information;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	public void SetEntity(EntityMove entity){
		inventory.Reset ();
		information = new PlayerInformation ().Reset (entity);
	}
	public void SetInvinsibility(bool on){
		entity.isAttackedBomb = on;
		entity.isAttackedBullet = on;
		entity.isAttackedContact = on;
	}
}

