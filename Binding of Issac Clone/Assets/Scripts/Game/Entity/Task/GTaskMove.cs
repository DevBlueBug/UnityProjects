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
		public override bool TestIsAlive (GEntity entity, GRoom room)
		{
			return (position - entity.position).sqrMagnitude > MinDistance;
		}
		public override void Do (GEntity entity, GRoom room)
		{
			var distance = position - entity.position;
			if (!isAlive) return;
			entity.AddForce (distance.normalized * entity.velo);
		}
	}
}

