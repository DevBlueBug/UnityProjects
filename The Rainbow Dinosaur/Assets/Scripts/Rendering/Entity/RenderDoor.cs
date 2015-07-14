using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RenderDoor : MonoBehaviour
{
	public EDoor door;
	public Animator ani;
	public SpriteRenderer sprRenderer;
	public Sprite 
		sprNormal,
		sprTreausre,
		sprSecret,
		sprBoss;

	void Awake(){
		door.E_Refreshed += H_Refreshed;
		door.E_OpenClose += H_OpenClose;
	}
	void Start ()
	{

	
	}
	
	// Update is called once per frame
	void H_OpenClose (bool openClose)
	{
		//Debug.Log ("oepnClose " + openClose);
		ani.SetInteger("state" ,(openClose)?1:0);
	}
	// Update is called once per frame
	void H_Refreshed (Entity entity,Room room)
	{
		if (entity.transform.localPosition.y == room.height - 1) {
			this.transform.localRotation =Quaternion.Euler(0,0,0);
			//top
		} else if (entity.transform.localPosition.x == 0) {
			this.transform.localRotation =Quaternion.Euler(0,0,90);
			//90 degree left
		} else if (entity.transform.localPosition.y == 0) {
			this.transform.localRotation =Quaternion.Euler(0,0,180);
			//bottom
		}
		else if (entity.transform.localPosition.x == room.width - 1) {
			this.transform.localRotation =Quaternion.Euler(0,0,270);
			//bottom
		}
		return;
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

