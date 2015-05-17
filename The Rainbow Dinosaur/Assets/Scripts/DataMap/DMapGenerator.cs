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
			List<Vector2> fourDirections = new List<Vector2>(){
				new Vector2(width/2,height/2+1),
				new Vector2(width/2+1,height/2),
				new Vector2(width/2,height/2-1),
				new Vector2(width/2-1,height/2)
			};
			List<DMapPopulator> branches = new List<DMapPopulator> ();
			int branchNum = Random.Range (2, 5);
			for (int i = 0; i < branchNum; i++) {
				int directionNum = Random.Range(0,100)%fourDirections.Count;
				var directionChosen = fourDirections[directionNum];
				fourDirections.RemoveAt(directionNum);
				branches.Add(new DMapPopulator(width, height, directionChosen ));
			}

			for (int i = 0; i < count*.7f; i++) {
				for(int j = 0 ; j < branches.Count;j++)
					branches[j].Iterate(map);
			}
			for (int i = 0; i < count; i++) {
				branches[0].Iterate(map);
			}

			return map;
		}
	}
}
