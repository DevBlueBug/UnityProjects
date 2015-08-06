using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NEntity.NNPC{
	
	public class NPC : Entity
	{

		List<KEnums.TypeTask> taskHandled =new List<KEnums.TypeTask>();
		public virtual NTask.Task GetTask_GiveMe(Entity entity, NItem.ItemChecker check){
			return null;
		}
	}
	

}