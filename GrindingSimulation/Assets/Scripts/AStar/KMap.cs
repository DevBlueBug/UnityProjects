using UnityEngine;
using System.Collections.Generic;

namespace AStar{
	public class KMap{
		/**
		int[][] dicAround = new int[][]{
			new int[]{0,1},new int[]{1,1},new int[]{1,0},new int[]{1,-1},
			new int[]{0,-1},new int[]{-1,-1},new int[]{-1,0},new int[]{-1,1}
		};
		**/
		int[][] dicAround = new int[][]{
			new int[]{0,1},new int[]{1,0},
			new int[]{0,-1},new int[]{-1,0}
		};
		int width, height;
		Node nodeFinal;
		Node[,] mapNodes;
		List<Node> nodesFree = new List<Node> ();
		Vector2 from, to;
		public Node this[int x, int y]{
			get{ return mapNodes [x, y];}
		}
		public KMap(int w, int h){
			this.width = w;
			this.height = h;
			mapNodes = new Node[w, h];
			for (int i = 0; i < w; i++)
				for (int j = 0; j < h; j++)
					mapNodes [i, j] = new Node (i,j);
		}

		public List<Node> GetPath(Vector2 from, Vector2 to){
			this.nodeFinal = null;
			this.from = from;
			this.to = to;
			PrepareForNewSearch ();
			for (int i = 0; i < 30; i++)
				if (Iterate ())
					break;
			return ToList(nodeFinal);
		}
		public void Reset(int x, int y){
			this.mapNodes [x, y] = new Node (x, y);
		}
		List<Node> ToList(Node node){
			if (node == null) return null;
			List<Node> list = new List<Node> ();
			ToList_Iterate (list, node);
			return list;

		}
		void ToList_Iterate(List<Node> list, Node node){
			if (node.nodePrevious != null) {
				ToList_Iterate (list, node.nodePrevious);
				list.Add (node);
			}


		}
		void PrepareForNewSearch(){
			nodesFree = new List<Node> ();
			for (int i = 0; i < width; i++)
				for (int j = 0; j< height; j++)
					mapNodes [i, j].Reset ();

			mapNodes [(int)from.x, (int)from.y].isAdded = true;
			nodesFree.Add(mapNodes[(int)from.x,(int)from.y] );

		}
		bool Iterate(){
			var nodeNow = GetLowestNode (nodesFree);
			//Debug.Log(nodeNow.x + " " + nodeNow.y);
			if (nodeNow.x == (int)to.x && nodeNow.y == (int)to.y) {
				nodeFinal = nodeNow;
				return true;
			}
			AddNodesAround (nodeNow, mapNodes, nodesFree);

			return false;//failed to find the target
		}
		Node GetLowestNode(List<Node> list){
			var priceOld = 99999.0f;
			Node n = null;
			int index = -1;
			for (int i = 0; i<list.Count; i++) {
				var priceNew= list[i].value + list[i].valueAccumulated;
				if(priceNew < priceOld){
					index = i;
					priceOld = priceNew;
					n = list[i];
				}
			}
			if (index != -1) list.RemoveAt (index);
			return n;
		}
		void AddNodesAround(Node n, Node[,] map, List<Node> nodesFree){
			for (int i = 0; i < dicAround.Length; i++) {
				int xNew = n.x + dicAround[i][0],
					yNew = n.y + dicAround[i][1];
				if(xNew<0 ||yNew<0 || xNew >= width ||yNew >= height) continue;
				var nodeNew = map[xNew,yNew];
				if(!nodeNew.isAlive || nodeNew.isAdded ) continue;
				nodeNew.isAdded = true;
				nodeNew.nodePrevious = n;
				CalculateValue(nodeNew,n);
				nodesFree.Add(nodeNew);
			}
		}
		void CalculateValue(Node nBefore, Node nAfter){
			nBefore.valueAccumulated = nAfter.valueAccumulated 
					+ Mathf.Abs(nAfter.x - nBefore.x)
					+ Mathf.Abs(nAfter.y - nBefore.y);
			nBefore.value = 
				nBefore.valueInternal 
				+ (to - new Vector2(nBefore.x,nBefore.y)).sqrMagnitude;
		}


	}
}