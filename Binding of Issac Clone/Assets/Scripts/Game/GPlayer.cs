//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using Game.Entity;
using Game.Entity.Behavior;
namespace Game
{
	public class GPlayer : MonoBehaviour
	{
		public static GEntity PlayerEntity;
		public static Vector3 PlayerPosition;

		public GEntity P_PlayerBase;
		public GBehavior behaviorPlayer;
		public PlayerController playerController;

		GEntity myEntitiy;

		bool isPlayerAlive = false;
		GRoom roomLastLinked = null;

		void Awake(){

		}

		public void E_NewPlayer(){
			myEntitiy = Instantiate (P_PlayerBase);
			myEntitiy.AddBehavior (Instantiate(behaviorPlayer));
			myEntitiy.E_Killed += delegate { isPlayerAlive = false;};
			GPlayer.PlayerEntity = myEntitiy;
			playerController.SetEntity(myEntitiy);
			isPlayerAlive = true;
		}
		public void E_NewRoom(GRoom room, int enteredDoorNumber){
			if (!isPlayerAlive)
				return;
			if (roomLastLinked != null)
				roomLastLinked.UnLinkEntity (myEntitiy);
			room.LinkEntity (myEntitiy);
			room.AddPlayer (myEntitiy,(enteredDoorNumber == -1)? -1:(enteredDoorNumber+2)%4);
			roomLastLinked = room;
		}

		public void KFixedUpdate (GRoom roomActive)
		{
			if (!isPlayerAlive)	return;

			myEntitiy.KFixedUpdate (roomActive);
			playerController.KFixedUpdate ();
		}

		public void KUpdate(GRoom room){
			if (!isPlayerAlive)	return;


			myEntitiy.KUpdate (room);
			GPlayer.PlayerPosition = myEntitiy.position;

			playerController.KUpdate ();

		}

	}
}

