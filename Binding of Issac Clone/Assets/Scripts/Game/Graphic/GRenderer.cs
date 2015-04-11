using UnityEngine;
using System.Collections.Generic;

using Utility;
using Game.Entity;
using Game.Graphic.Effect;


namespace Game.Graphic{
	
	public class GRenderer : MonoBehaviour
	{
		public bool isDebug;
		public Game.Entity.GEntity myEntity;
		public Animator animator;
		public List<MeshRenderer> meshes;
		public List<GRendererEffect> effects;
		Vector3 positionLast = new Vector3();
		int countFrame;

		void Awake(){
			myEntity.E_HpChange += Hdr_HpChange;
		}
		void Hdr_HpChange(GEntity entity, float hpChange){
			if (hpChange >= 0)
				return;
			animator.SetTrigger ("trgAttacked");
			//animator.SetBool("isAttacked",true);

		}
		bool isMove;
		void Update(){
		//	if(isDebug && !myEntity.isMoved)Debug.Log ("GRENDERER " + gameObject.name + " "+ myEntity.isMoved);
			animator.SetBool ("isMoving", myEntity.isMove	);
			
			if (isMove && !myEntity.isMove) {
				isMove = false;
				animator.speed = 1;
			}
			if (myEntity.isMove) {
				isMove = true;
				animator.speed = .8f + myEntity.velo*.2f;
			}

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
