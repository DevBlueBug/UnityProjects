using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	public delegate void D_WorldCreated(NWorld.World world);
	public D_WorldCreated E_WorldCreated = delegate {};
	NWorld.World world;
	public void Start () {
		world = new NWorld.World (100, 100);
		E_WorldCreated (world);
		var character = new NWorld.NUnit.Unit (0, 0);
		//character.AddSkill (new NWorld.NUnit.NSkill.Walk ());
		//character.AddSkill (new NWorld.NUnit.NSkill.Get ());

		character.AddNeed (new NWorld.NUnit.NNeed.Treasure (10,100 ));
		world.AddTreasure (new NWorld.NTreasure.Treasure (10,10,90,100));

		
		world.AddUnit (character);
		world.AddUnit (new NWorld.NUnit.Unit (10,0));
		world.AddUnit (new NWorld.NUnit.Unit (0,10));
	
	}
	void Update(){
		if (world == null) return;
		if(Input.GetKeyDown(KeyCode.Space) ){
			world.Update();
		}
	}
}
