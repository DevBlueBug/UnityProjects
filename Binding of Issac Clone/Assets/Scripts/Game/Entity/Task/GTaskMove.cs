using UnityEngine;
namespace Game.Entity.Task
{
	public class GTaskMove :GTask
	{
		public static float MinDistance = .01f;
		Vector3 position;
		public GTaskMove (Vector3 position)
		{
			this.position = position;
		}
		
		public override void KFixedUpdate (GEntity entity, GRoom room)
		{
			base.KFixedUpdate (entity, room);
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
			return (position - entity.position).sqrMagnitude > MinDistance;
		}
		public void Move(GEntity entity, GRoom room){
			if (!entity.isAlive) return;
			entity.body.AddForce (
				(position - entity.position).normalized 
			                      * entity.velo);
		}
	}
}

