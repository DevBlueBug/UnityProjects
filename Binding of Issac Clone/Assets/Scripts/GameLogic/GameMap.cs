using UnityEngine;

namespace GameLogic
{
	public class GameMap
	{
		public int width, height;
		public DataRoom roomAt;
		public DataRoom[,] rooms;

		public GameMap (int w, int h)
		{
			this.width = w;
			this.height = h;
			rooms = new DataRoom[width,height];
		}
		public bool Move(Vector2 direction){
			var newId = roomAt.id + direction;
			if (newId.x < 0 || newId.y < 0 || newId.x >= width || newId.y >= height)
				return false;
			var roomNew = rooms [(int)newId.x, (int)newId.y];
			if (roomNew == null)
				return false;
			roomAt = roomNew;
			return true;
		}
	}
}

