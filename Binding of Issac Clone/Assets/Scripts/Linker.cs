using UnityEngine;
using System.Collections;

using GameLogic;
using GameRender;

public class Linker : MonoBehaviour
{
	public GameLogic.GameMasterS gMaster;
	public GameRender.RenderMaster rMaster;
	// Use this for initialization
	void Awake ()
	{
		gMaster.E_NewRoom += rMaster.E_NewRoom;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

