using System;
using System.Collections.Generic;
namespace Game.Data
{
	public class DMapDecorator
	{
		public DMapDecorator ()
		{
		}
		public void Init(List<DRoom> rooms){
			foreach (var r in rooms)
				Decorate (r);
		}
		void Decorate(DRoom room){
			var e = new DEntity ();
			e.myType = (DEntity.MyType)UnityEngine.Random.Range(0,2);
			e.x = 2+UnityEngine.Random.Range(0,3);
			e.y = 2+UnityEngine.Random.Range(0,3);
			e.id = 0;
			room.entities.Add(e);

		}
	}
}

