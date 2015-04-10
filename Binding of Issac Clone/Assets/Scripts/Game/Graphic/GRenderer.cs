using UnityEngine;
using System.Collections.Generic;

using Utility;
using Game.Entity;
using Game.Graphic.Effect;


namespace Game.Graphic{
	
	public class GRenderer : MonoBehaviour
	{
		public Game.Entity.GEntity myEntity;
		public Animator animator;
		public List<MeshRenderer> meshes;
		public List<GRendererEffect> effects;


		void Awake(){
			myEntity.E_HpChange += Hdr_HpChange;
		}
		void Hdr_HpChange(GEntity entity, float hpChange){
			if (hpChange >= 0)
				return;
			animator.SetTrigger ("trgAttacked");
			//animator.SetBool("isAttacked",true);

		}
		void Update(){
			/**
			var c = rendererMesh.material.color;
			this.rendererMesh.material.color = 
				new Color (
					c.r+
					Random.Range (-.05f, .050f),
					c.g+
					Random.Range (-.05f, .050f), 
					c.b+
					Random.Range (-.05f, .050f),
					1);
					**/

		}
	}

}
