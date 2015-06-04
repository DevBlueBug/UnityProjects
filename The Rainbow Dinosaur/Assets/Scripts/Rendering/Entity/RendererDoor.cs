using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RendererDoor : MonoBehaviour
{
	public EDoor door;
	public SpriteRenderer sprRenderer;
	public Sprite 
		sprNormal,
		sprTreausre,
		sprSecret,
		sprBoss;

	void Awake(){
		door.E_Refreshed += H_Refreshed;
	}
	void Start ()
	{

	
	}
	
	// Update is called once per frame
	void H_Refreshed (Entity entity,Room room)
	{
		Dictionary<Data.DDoor.Types,Sprite> dic = new Dictionary<Data.DDoor.Types, Sprite> (){
			{Data.DDoor.Types.Normal,sprNormal},
			{Data.DDoor.Types.Treausre,sprTreausre},
			{Data.DDoor.Types.Secret,sprSecret},
			{Data.DDoor.Types.Boss,sprBoss}
		};
		sprRenderer.sprite = dic [door.type];
		Destroy (this);
	
	}
}

