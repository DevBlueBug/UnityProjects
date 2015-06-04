using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour
{
	public PlayerManager manager;
	public UnityEngine.UI.Text 
		txtHealth,
		txtMoney,
		txtBomb;
	public int health,money,bomb;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		txtHealth.text = "HEALTH " + manager.player.entity.hp;
		txtMoney.text = "MONEY " + manager.player.entity.inventory.Get(NItem.Item.KType.Money);
		txtBomb.text = "BOMB " + manager.player.entity.inventory.Get(NItem.Item.KType.Bomb);
	
	}
}

