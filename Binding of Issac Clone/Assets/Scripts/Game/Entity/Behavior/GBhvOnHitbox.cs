using UnityEngine;
using System.Collections.Generic;

namespace Game.Entity.Behavior
{
	
	public class GBhvOnHitbox : GBehavior
	{
		public bool 
			isCollisionEnter,
			isTriggerEnter,
			isTriggerStay,

			isHitWorld,
			isHitPlayer, 
			isHitEnemy;
		public List<GBehavior> onBehaviorsOther;
		public List<GBehavior> onBehaviorsMe;
		public D_Custom_Do onDoMe = delegate {		};
		public D_Custom_Do onDoOther = delegate {		};

		List<string> tagsToCheck  = new List<string>();
		Dictionary<int,GEntity> entitiesCollidedDic = new Dictionary<int, GEntity>();
		List<GEntity> entititesCollided = new List<GEntity> ();


		public override void Init (GEntity entity)
		{
			base.Init (entity);
			foreach (var b in onBehaviorsMe) b.Init (entity);
			foreach (var b in onBehaviorsOther) b.Init (entity);
			Link (entity);

			if (isHitWorld)
				tagsToCheck.Add (GameTags.World);
			if (isHitPlayer)
				tagsToCheck.Add (GameTags.Player);
			if (isHitEnemy)
				tagsToCheck.Add (GameTags.Enemy);
		}
		public override void Kill (GEntity entity)
		{
			UnLink (entity);
			base.Kill (entity);
		}
		public void Link(GEntity entity){
			if (isCollisionEnter)
				entity.E_OnCollisionEnter += Hdr_OnCollision;
			if (isTriggerEnter) {
				entity.E_OnTriggerEnter += Hdr_OnTrigger;
			}
			if (isTriggerStay) {
				entity.E_OnTriigerStay += Hdr_OnTrigger;
			}
		}
		public void UnLink(GEntity entity){
			if (isCollisionEnter)
				entity.E_OnCollisionEnter -= Hdr_OnCollision;
			if (isTriggerEnter) {
				entity.E_OnTriggerEnter -= Hdr_OnTrigger;
			}
			if (isTriggerStay) {
				entity.E_OnTriigerStay -= Hdr_OnTrigger;
			}
		}
		public bool IsTagCorrect(GameObject o){

			foreach (var t in tagsToCheck)
			if (o.tag == t) {
					return true;
				}

			return false;
		}
		public void Hdr_OnCollision(GEntity entity, Collision2D c){
			Hdr_Collision (entity, c.gameObject);
		}
		public void Hdr_OnTrigger(GEntity entity, Collider2D c){
			Hdr_Collision (entity, c.gameObject);
		}
		public void Hdr_Collision(GEntity me, GameObject other){
			//Debug.Log ("HDR_COLLLISION " + other.gameObject.name + " " +IsTagCorrect (other) + " " + other.gameObject.tag);
			if (IsTagCorrect (other)) {
				var entity = other.transform.parent.GetComponent<GEntity>();
				if(entity ==null)return;

				GEntity tryOut;
				entitiesCollidedDic.TryGetValue(entity.id,out tryOut);
				if(tryOut!=null) return;
				entitiesCollidedDic.Add(entity.id,entity);
				//entititesCollided.Add(entity);			
			}
		}
		public override void Do (GEntity entity, GRoom room)
		{
			base.Do (entity, room);
			if (entitiesCollidedDic.Count != 0) {
				foreach(var e in entitiesCollidedDic){
					onDoMe(entity,room);
					UpdateTheRest(onBehaviorsMe, entity,room);

					foreach(var b in onBehaviorsOther){
						b.Init(e.Value);
						b.KUpdate(e.Value,room);
					}
					onDoOther(e.Value,room);
				}
				entitiesCollidedDic = new Dictionary<int, GEntity>();
			}
		}
	}
	

}