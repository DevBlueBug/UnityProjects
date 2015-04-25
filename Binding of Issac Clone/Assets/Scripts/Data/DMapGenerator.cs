using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace  Data {

	public class DMapGenerator
	{
		public DMap GenerateMap(int width, int height, int count){
			DMap map = new DMap(width,height);
			map[width/2,height/2]=new DRoom(new Vector2(width/2,height/2));
			map.roomInit = map[width/2,height/2];
			
			DMapPopulator pop00 = 
				new DMapPopulator (width, height,new Vector2(width/2+1,height/2) );
			DMapPopulator pop01 = 
				new DMapPopulator (width, height,new Vector2(width/2,height/2+1) );
			for (int i = 0; i < 3; i++) {
				pop00.Iterate(map);
				pop01.Iterate(map);
			}
			for (int i = 0; i < 10; i++) {
				pop01.Iterate(map);
			}
			var decorator = new DMapDecorator ();
			decorator.Init (pop00.roomsCreated);
			decorator.Init (pop01.roomsCreated);

			return map;
		}
	}
}
