using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Data;

namespace GameEditor{
	public class EditorTile : MonoBehaviour
	{
		public Sprite 
			SPR_Ground,
			SPR_GroundAir,
			SPR_Wall_Hard,
			SPR_Wall_Soft,
			SPR_Wall_Solid,
			SPR_Trap,
			SPR_Enemy,
			SPR_Item;
		public RectTransform rect;
		public Image img;

		public DRoomLayout.TileType myType;

		public void SetType(DRoomLayout.TileType type){
			this.myType = type;
			if (type == DRoomLayout.TileType.Ground)
				img.sprite = SPR_Ground;
			else if (type == DRoomLayout.TileType.Air)
				img.sprite = SPR_GroundAir;
			else if (type == DRoomLayout.TileType.Wall_Hard)
				img.sprite = SPR_Wall_Hard;
			else if (type == DRoomLayout.TileType.Wall_Soft)
				img.sprite = SPR_Wall_Soft;
			else if (type == DRoomLayout.TileType.Wall_Solid)
				img.sprite = SPR_Wall_Solid;
			else if (type == DRoomLayout.TileType.Trap)
				img.sprite = SPR_Trap;
			else if (type == DRoomLayout.TileType.Enemy)
				img.sprite = SPR_Enemy;
			else if (type == DRoomLayout.TileType.Item)
				img.sprite = SPR_Item;

		}

	}
}

