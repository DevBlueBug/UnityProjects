using UnityEngine;
using System.Collections;

using NWorld.NEntity.NNPC;
using NWorld.NItem.NEquip;

public class Main : MonoBehaviour {
	public delegate void D_WorldCreated(NWorld.World world);
	public D_WorldCreated E_WorldCreated = delegate {};
	NWorld.World world;
	float timeElapsed = 0;
	public void Start () {
		world = new NWorld.World (100, 100);
		E_WorldCreated (world);
		var character = new NWorld.NEntity.NUnit.Unit (0, 0);
		character.AddNeed (new NWorld.NEntity.NUnit.NNeed.Treasure (10,100 ));
		character.AddNeed (new NWorld.NEntity.NUnit.NNeed.Gear (10,100 ));
		//character.AddSkill (new NWorld.NUnit.NSkill.Walk ());
		//character.AddSkill (new NWorld.NUnit.NSkill.Get ());
		AddMerchants ();
		world.AddTreasure (new NWorld.NEntity.NTreasure.Treasure (3,3,90,100));

		
		world.AddUnit (character);
		world.AddUnit (new NWorld.NEntity.NUnit.Unit (10,0));
		world.AddUnit (new NWorld.NEntity.NUnit.Unit (0,10));
	
	}
	void AddMerchants(){
		var merchantChest = new Merchant ();
		var merchantWeapon = new Merchant ();

		var chest00 = (Equipment)new Equipment ().SetType(NWorld.KEnums.TypeItemEquip.Chest);
		var chest01 = (Equipment)new Equipment ().SetType(NWorld.KEnums.TypeItemEquip.Chest);
		var chest02 = (Equipment)new Equipment ().SetType(NWorld.KEnums.TypeItemEquip.Chest);
		chest00.stats.armor = 1;
		chest01.stats.armor = 2;
		chest02.stats.armor = 3;

		chest00.price.Add (new NWorld.NItem.Price.Pair (NWorld.KEnums.IdItem.Jewerly_Green,1 ));
		chest01.price.Add (new NWorld.NItem.Price.Pair (NWorld.KEnums.IdItem.Jewerly_Green,2 ));
		chest02.price.Add (new NWorld.NItem.Price.Pair (NWorld.KEnums.IdItem.Jewerly_Green,3 ));

		
		var weapon00 = (Equipment)new Equipment ().SetType(NWorld.KEnums.TypeItemEquip.Weapon);
		var weapon01 = (Equipment)new Equipment ().SetType(NWorld.KEnums.TypeItemEquip.Weapon);
		var weapon02 = (Equipment)new Equipment ().SetType(NWorld.KEnums.TypeItemEquip.Weapon);
		weapon00.stats.damage = 1;
		weapon01.stats.damage = 2;
		weapon02.stats.damage = 3;
		weapon00.price.Add (new NWorld.NItem.Price.Pair (NWorld.KEnums.IdItem.Jewerly_Red,1 ));
		weapon01.price.Add (new NWorld.NItem.Price.Pair (NWorld.KEnums.IdItem.Jewerly_Red,2 ));
		weapon02.price.Add (new NWorld.NItem.Price.Pair (NWorld.KEnums.IdItem.Jewerly_Red,3 ));
		
		merchantChest.inventory.Add (chest00);
		merchantChest.inventory.Add (chest01);
		merchantChest.inventory.Add (chest02);
		merchantWeapon.inventory.Add (weapon00);
		merchantWeapon.inventory.Add (weapon01);
		merchantWeapon.inventory.Add (weapon02);
		world.AddMerchant (merchantChest);
		world.AddMerchant (merchantWeapon);
	}
	void Update(){
		if (world == null) return;

		timeElapsed += Time.deltaTime;
		if (timeElapsed > .5f) {
			timeElapsed = 0;
			world.Update();
		}
		if(Input.GetKeyDown(KeyCode.Space) ){
			world.Update();
		}
	}
}
