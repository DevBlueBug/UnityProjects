using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Game.Entity;

namespace Game{
	
	public class GEntityPack : MonoBehaviour
	{
		public List<GEntity> world, enemies;

		// Use this for initialization
		public GEntity UnWrap(Data.DEntity data){
			Dictionary<Data.DEntity.MyType,List<GEntity>>
			dirEntities = new Dictionary<Data.DEntity.MyType, List<GEntity>> (){
				{Data.DEntity.MyType.World,world},
				{Data.DEntity.MyType.Enemy,enemies},
			};
			return dirEntities[data.myType][data.id];
		}
	}
	

}