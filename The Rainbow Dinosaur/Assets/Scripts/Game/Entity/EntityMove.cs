using UnityEngine;
using System.Collections;

public class EntityMove : Entity
{
	public Rigidbody2D body;
	public float friction;	
	Vector3 velocity;

	bool moveChanged = false;

	public void Move(Vector3 velocity){
		moveChanged = true;
		this.velocity = velocity;
	}
	public override void KUpdate (Room room)
	{
		body.velocity = velocity;
		if (!moveChanged) {
			UpdateFriction();
		} else {
			moveChanged = false;
		}
		base.KUpdate (room);
	}
	public void UpdateFriction(){
		velocity.Scale (Vector3.one * (1- friction * Time.deltaTime ) );
		if (velocity.sqrMagnitude < .1f)
			velocity = Vector3.zero;
	}


}

