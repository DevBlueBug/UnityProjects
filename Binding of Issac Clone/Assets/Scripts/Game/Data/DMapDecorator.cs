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
			int n =  UnityEngine.Random.Range (1, 10);
			//int n =  1;
			for (int i = 0; i< n; i++) {
				var e = new DEntity ();
				e.myType = (DEntity.MyType)UnityEngine.Random.Range(0,2);
				e.x = UnityEngine.Random.Range(1,room.width-1);
				e.y = UnityEngine.Random.Range(1,room.height-1);
				e.id = 0;
				room.entities.Add(e);
			}

		}
	}
}

