using UnityEngine;
using System.Collections;

public class EffectKilled : MonoBehaviour
{

	public GameObject P_EFFECT;
	public Color	 color;
	// Use this for initialization
	void Awake(){
		var entity = this.gameObject.GetComponent<Entity> ();
		if(entity ==null) return;
		entity.E_Kill += H_Kill;
	}
	void H_Kill (Entity entity)
	{
		var effect = Instantiate (P_EFFECT);
		effect.transform.parent = entity.transform.parent;
		effect.transform.localPosition = entity.transform.localPosition;
		try{
			
			effect.GetComponent<SpriteRenderer> ().material.color = this.color;

		}
		catch{
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

