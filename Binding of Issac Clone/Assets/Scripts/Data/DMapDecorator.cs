using UnityEngine;
using System.Collections.Generic;
namespace Data
{
	public class DMapAccessory{
		public float prefearedApproximity; // how close is the room 
		public int doorNumber; // number of door that will be needed
		public DRoom.KType type;
		
	}
	public class DMapDecorator
	{
		List<DMapAccessory> listAcsry = new List<DMapAccessory>();

		public DMapDecorator ()
		{
		}
		public void Add(DMapAccessory acs){
			listAcsry.Add (acs);
		}
		public void Init(List<DRoom> rooms){
			foreach (var r in rooms)
				Decorate (r);
		}
		void Decorate(DRoom room){
			///return;
			int n = 7;
			int type = 1;
			//int n =  UnityEngine.Random.Range (10, 20);
			//int n =  1;
			for (int i = 0; i< n; i++) {
				var e = new DEntity ();
				e.myType = (DEntity.MyType)(type++ % 2);
				e.x = UnityEngine.Random.Range(2,room.width-2);
				e.y = UnityEngine.Random.Range(2,room.height-2);
				e.id = 0;
				room.entities.Add(e);
			}

		}
		void Iterate(DMap map){
		}
		void Finish(){
		}
	}
}

