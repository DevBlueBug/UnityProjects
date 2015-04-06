using UnityEngine;
using System.Collections.Generic;



namespace Game.Entity.Behavior{
	
	public class GBhvShoot : GBehavior
	{
		public GEntity P_Entity; // an entity that will be the base model
		public List<GBehavior> behaviorsToAdd; //behaviors that will be added to the entity
		//create the entity
		//copy then add all the behavior to the new bullet model
		public float force;
		public GameObject posFrom;


		public override void Start ()
		{
			base.Start ();
		}
		public override void Init (GEntity entity)
		{
			//Debug.Log ("Init");
			entity.E_Attack += Hdr_Attack;
			base.Init (entity);
		}
		public void Hdr_Attack(GEntity entity, int n){
			var obj = GetEntity(P_Entity, behaviorsToAdd);
			obj.rotation = entity.rotation;
			obj.transform.position = posFrom.transform.position;
			obj.body.AddForce ((posFrom.transform.position - entity.transform.position) * force);
			entity.E_Birth (entity, obj);
		}
		GEntity GetEntity(GEntity Prefab, List<GBehavior> bhvs){
			var entity = Instantiate (Prefab);
			foreach (var b in bhvs) {
				var bCopy = Instantiate(b);
				entity.AddBehavior(bCopy);
			}
			return entity;
		
		}

	}
	

}