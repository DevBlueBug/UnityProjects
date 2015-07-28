using UnityEngine;
using System.Collections;

namespace NBehaviour.NCondition
{

	public class CdtPathToPlayerAvailable : Cdt
	{
		public CdtPathToPlayerAvailable(Entity entity):base(entity){
		}

		public override int Process (Entity entity, Room room, int bhvProcessResult)
		{
			var path = room.aStarMap.GetPath (
				new Vector2 (
				Mathf.Round (entity.transform.localPosition.x), 
				Mathf.Round (entity.transform.localPosition.y))
				,
				new Vector2 (
				Mathf.Round (PlayerManager.PlayerPosInt.x), 
				Mathf.Round (PlayerManager.PlayerPosInt.y))
				);
			Debug.Log (path + " ");
			return -1;
			//return base.Process (entity, room, bhvProcessResult);
		}
	}

}