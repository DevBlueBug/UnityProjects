using UnityEngine;
using System.Collections.Generic;

public class EntityMove : Entity
{
	public Rigidbody2D body;
	public float velo,friction;
	internal Vector3 
		velocity		= Vector3.zero,
		velocityForce	= Vector3.zero;

	bool 
		moveChanged = false,
		forceChanged = false;
	
	public void SetVelocity(Vector3 velocity){
		moveChanged = true;
		this.velocity = velocity;
	}
	public void AddForceVelocity(Vector3 velocity){
		forceChanged = true;
		this.velocityForce += velocity;
	}
	public void Move(Vector3 dir){
		moveChanged = true;
		this.velocity = dir * velo;
	}
	public override void KUpdate (Room room)
	{
		body.velocity = velocity + velocityForce;
		if (!moveChanged) {
			UpdateFriction(ref velocity);
		} else {
			moveChanged = false;
		}

		if (!forceChanged) {
			UpdateFriction(ref velocityForce);
		} else {
			forceChanged = false;
		}
		base.KUpdate (room);
	}
	public void UpdateFriction(ref Vector3 velocity ){
		velocity.Scale (Vector3.one * (1- friction * Time.deltaTime ) );
		if (velocity.sqrMagnitude < .1f)
			velocity = Vector3.zero;
	}

	internal Stack<Vector3> GetPathToPlayer(Room room){
		var destinations = new Stack<Vector3>();
		var node = room.aStarMap.GetPath(
			new Vector2(posX,posY),
			new Vector2(PlayerManager.PlayerPosInt.x,PlayerManager.PlayerPosInt.y));
		if(node== null) {
			return null;
		}
		while(node.nodePrevious != null){
			destinations.Push(new Vector3(node.x,node.y,0) );
			node = node.nodePrevious;
		}
		return destinations;
	}


}

