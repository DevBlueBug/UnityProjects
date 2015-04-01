using UnityEngine;

namespace AStar
{
	public class Node
	{
		public Node nodePrevious;
		public int x,y;
		public bool 
			isAdded = false,
			isAlive = true;
		public float
				valueInternal = 10,
				value = 10,//attraction point, higher the bad
				valueAccumulated =0;

		public Node (int x, int  y)
		{
			this.x = x;
			this.y = y;
		}
		public void Reset(){
			nodePrevious = null;
			isAdded = false;
			valueAccumulated = 0;
			value = 0;
		}
	}
}

