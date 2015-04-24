using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

		public KType myType;

		public enum KType
		{
			Ground,
			Air,
			Wall_Hard,
			Wall_Soft,
			Wall_Solid,
			Trap,
			Enemy,
			Item
		}
		public void SetType(KType state){
			if (state == KType.Ground)
				img.sprite = SPR_Ground;
			else if (state == KType.Air)
				img.sprite = SPR_GroundAir;
			else if (state == KType.Wall_Hard)
				img.sprite = SPR_Wall_Hard;
			else if (state == KType.Wall_Soft)
				img.sprite = SPR_Wall_Soft;
			else if (state == KType.Wall_Solid)
				img.sprite = SPR_Wall_Solid;
			else if (state == KType.Trap)
				img.sprite = SPR_Trap;
			else if (state == KType.Enemy)
				img.sprite = SPR_Enemy;
			else if (state == KType.Item)
				img.sprite = SPR_Item;

		}

	}
}

