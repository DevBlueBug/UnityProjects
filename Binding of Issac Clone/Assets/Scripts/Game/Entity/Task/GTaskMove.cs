using UnityEngine;
namespace Game.Entity.Task
{
	public class GTaskMove :GTask
	{
		public static float MinDistance = .01f;
		internal Vector3 position;
		public GTaskMove (Vector3 position)
		{
			this.position = position;
		}
		public override void Do (GEntity entity, GRoom room)
		{
			base.Do (entity, room);
			Move (entity, room);
		}


		public override void Init (GEntity entity)
		{
			base.Init (entity);
		}
		public override void Kill (GEntity entity)
		{
			base.Kill (entity);
		}
		public override bool TestIsAlive (GEntity entity, GRoom room)
		{
			float 
				x = position.x - entity.position.x, 
				y = position.z - entity.position.z;
			return  Mathf.Sqrt(x*x+ y*y) > MinDistance;
		}
		public void Move(GEntity entity, GRoom room){
			var dis = position - entity.position;
			var dir = new Vector3 (dis.x, 0, dis.z).normalized;
			var mag = entity.body.velocity.magnitude;
			var ratio =  1 - Vector3.Dot (dir, entity.body.velocity.normalized);

			
			
			/**
			Debug.Log ("VELO " +entity.body.velocity + " , RATIO " +ratio + 
			           " , VELO "+ entity.body.velocity	);
			var force = Vector3.zero; // normal constant force;

			if (ratio > .1f || mag < entity.velo) {
				Debug.Log("ADDING CONSTANT FORCE" );
				force += dir * entity.velo;
			}
			if (ratio > .1f && mag < entity.velo) {
				Debug.Log("ADDING ADDITIONAL FORCE" );
				force += dir * entity.velo * ratio*(1/Time.fixedDeltaTime);
				
			}
			**/
			//Debug.Log(entity.id + " MOVE TO " + this.position);
			entity.position += dir * entity.velo * Time.deltaTime;
			//entity.body.AddForce (force);
			//Debug.Log ("ADDING " + forceAdd);
		}
	}
}

