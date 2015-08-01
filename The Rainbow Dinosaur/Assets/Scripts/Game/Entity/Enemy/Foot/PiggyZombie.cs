using UnityEngine;
using System.Collections;
using NBehaviour;
using NBehaviour.NCondition;

public class PiggyZombie : Enemy
{
	public enum State{Idl, FollowingPlayer};
	public delegate void D_Me_State (Entity me,State state);
	public D_Me_State E_Me_Sate = delegate {};
	State stateMe = State.Idl;

	public override void Awake ()
	{
		var bhvIdl = new NBehaviour.BhvCustomize ();
		var bhvState_FollowingPlayer = new NBehaviour.BhvCustomize ().SetMethod(H_NewState_FollowingPlayer);
		var bhvFollowPlayer = new NBehaviour.BhvFollowPlayer (this);
		var bhvState_Idl = new NBehaviour.BhvCustomize ().SetMethod(H_NewState_Idl);

		bhvIdl.bhvSuccess = bhvState_FollowingPlayer;
		bhvState_FollowingPlayer.bhvFail = bhvState_Idl;
		bhvState_Idl.bhvSuccess = bhvIdl;

		bhvIdl.condition = new CdtPathToPlayerAvailable (this);
		bhvState_FollowingPlayer.condition = new CdtPreDefined (this, 1);
		bhvFollowPlayer.condition = new CdtBhvResult (this);
		bhvState_Idl.condition = new CdtPreDefined (this, 1);


		//var cdtFindPlayer = new NBehaviour.NCondition.CdtPathToPlayerAvailable (this);
		//var cdtFindPlayer_After = new NBehaviour.NCondition.CdtPreDefined (this, 1); //success all the time

		bhvs.Add (bhvIdl);
		base.Awake ();
	}
	int H_NewState_Idl(Entity entity, Room room){
		this.stateMe = State.Idl;
		E_Me_Sate (entity, stateMe);
		return 1;
	}
	
	int H_NewState_FollowingPlayer(Entity entity, Room room){
		this.stateMe = State.FollowingPlayer;
		E_Me_Sate (entity, stateMe);
		return 1;
	}
}

