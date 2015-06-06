using UnityEngine;
using System.Collections.Generic;
namespace NBehaviour
{
	public class BhvAvoidBulletsForce :NBehaviour.Behaviour
	{
		float distance,distanceSqr, force;
		public BhvAvoidBulletsForce(Entity Entity, float minDistance, float force):base(Entity){
			this.distanceSqr = minDistance*minDistance;
			this.force = force;
			this.distance = minDistance;
		}
		float GetRatio(Vector3 dis){
			//var dis = other.transform.localPosition - me.transform.localPosition;
			var disSqrt = .001f+dis.sqrMagnitude;
			if (disSqrt > distanceSqr) return 0;
			float ratio = ( distance - Mathf.Sqrt(disSqrt))/distance;
			return ratio*ratio;
		}
		
		public override int Process (Entity entity, Room room)
		{
			int result = 0;
			for (int i = 0; i < room.entities.Count; i++) {
				if(room.entities[i] != entity && room.entities[i].meType == Entity.KType.Bullet)

				if(Process(entity,room.entities[i])) result = 1;

			}
			return result;
		}
		bool Process(Entity me, Entity other){
			var dir = me.transform.localPosition-other.transform.localPosition;
			var ratio = GetRatio (dir);
			if (ratio == 0) return false;
			dir.Normalize ();
			//Debug.Log (ratio);
			try{
				var velocity = ((EBullet)other).direction;
				var dot = Vector2.Dot(new Vector2(velocity.x,velocity.y), new Vector2(dir.x,dir.y) );
				if(dot > 0.5f){
					//Debug.Log("New dir " + dir  + " " + new Vector3(dir.y,-dir.x,dir.z));
					float 
						x = (dir.x >0)? Mathf.Abs(dir.y):-Mathf.Abs(dir.y),
						y = (dir.y >0)? Mathf.Abs(dir.x):-Mathf.Abs(dir.x);
					dir = new Vector3(x,y,0);

				}
				//var crossproduct = Vector3.Cross(new Vector3(velocity.x,velocity.y,0),new Vector3(dir.x,dir.y,0));
				//Debug.Log(dot);
			}
			catch{Debug.Log("failed");}
			
			if (dir.y < 0 && me.transform.localPosition.y < 1.2f) {
				dir.y *= -1;
			} else if (dir.y > 0 && me.transform.localPosition.y > 6.8) {
				dir.y *= -1;
			}
			if (dir.x < 0 && me.transform.localPosition.x < 1.2f) {
				dir.x *= -1;
			} else if (dir.x > 0 && me.transform.localPosition.x > 12.8) {
				dir.x *=-1;
			}

			//Debug.Log (me.transform.localPosition + " , " + other.transform.localPosition);
			//Debug.Log (dir+ " " + ratio+ " " + dir * ratio * force * Time.deltaTime);
			((EntityMove)me).AddForceVelocity(dir*ratio*force * Time.deltaTime);
			//((EntityMove)me).SetVelocity(dir*force);
			return true;

		}
	}
}

