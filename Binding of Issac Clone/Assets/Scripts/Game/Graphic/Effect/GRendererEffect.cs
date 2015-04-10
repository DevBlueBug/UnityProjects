using UnityEngine;
using System.Collections.Generic;


namespace Game.Graphic.Effect{
	
	public class GRendererEffect : MonoBehaviour
	{
		public bool isAlive = true;

		public virtual void Init(GRenderer gRen){

		}
		public void kUpdate(GRenderer gRen, List<MeshRenderer> meshes){

		}
	}
}

