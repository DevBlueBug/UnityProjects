using UnityEngine;
using System.Collections.Generic;

namespace Game.Entity.Behavior
{
	
	public class GBhvOnHitbox : GBehavior
	{
		public bool 
			isCollision,
			isTrigger,

			isHitWorld,
			isHitPlayer, 
			isHitEnemy;
		public List<GBehavior> onBehaviorsOther;
		public List<GBehavior> onBehaviorsMe;

		List<string> tagsToCheck  = new List<string>();
		List<GEntity> entititesCollided = new List<GEntity> ();

		
		public override void Init (GEntity entity)
		{
			base.Init (entity);
			foreach (var b in onBehaviorsMe) b.Init (entity);
			foreach (var b in onBehaviorsOther) b.Init (entity);
			if (isCollision)
				entity.E_OnCollisionEnter += Hdr_OnCollisionEnter;
			if (isTrigger) {
				entity.E_OnTriggerEnter += Hdr_OnTriggerEnter;
			}

			if (isHitWorld)
				tagsToCheck.Add (GameTags.World);
			if (isHitPlayer)
				tagsToCheck.Add (GameTags.Player);
			if (isHitEnemy)
				tagsToCheck.Add (GameTags.Enemy);

		}

		public override void Start ()
		{
			base.Start ();
		}
		public bool IsTagCorrect(GameObject o){

			foreach (var t in tagsToCheck)
			if (o.tag == t) {
					return true;
				}

			return false;
		}
		public void Hdr_OnCollisionEnter(GEntity entity, Collision c){
			Hdr_Collision (entity, c.gameObject);
		}
		public void Hdr_OnTriggerEnter(GEntity entity, Collider c){
			Hdr_Collision (entity, c.gameObject);
		}
		public void Hdr_Collision(GEntity me, GameObject other){
			if (IsTagCorrect (other)) {
				var entity = other.transform.parent.GetComponent<GEntity>();
				if(entity ==null)return;
				entititesCollided.Add(entity);			
			}
		}
		public override void Do (GEntity entity, GRoom room)
		{
			base.Do (entity, room);
			if (entititesCollided.Count != 0) {
					foreach(var e in entititesCollided){
					UpdateTheRest(onBehaviorsMe, entity,room);

					foreach(var b in onBehaviorsOther){
						b.Init(e);
						b.KUpdate(e,room);
					}
				}
				entititesCollided = new List<GEntity>();
			}
		}
	}
	

}