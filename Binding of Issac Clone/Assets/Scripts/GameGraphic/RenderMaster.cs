using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GameRender{
	public class RenderMaster : MonoBehaviour
	{

		public int RendererChosen;
		public List<Room> RenderersRoom;
		Room roomLastRendered;
		void Start ()
		{
		
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}

		public void E_NewRoom (GameLogic.DataRoom room)
		{
			if (roomLastRendered != null)
				roomLastRendered.Destroy ();
			roomLastRendered = Instantiate(RenderersRoom [RendererChosen]);
			roomLastRendered.Display (room);
		}
	}

}