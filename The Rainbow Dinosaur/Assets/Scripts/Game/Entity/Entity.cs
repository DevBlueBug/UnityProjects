using UnityEngine;
using System.Collections.Generic;


public class Entity : MonoBehaviour
{
	public enum KType{World,Player,Enemy,Bullet};
	public enum HpChangeType{Absolute, Bullet,Bomb,Contact};
	public delegate void D_Kill(Entity me);
	public delegate void D_Attacked(Entity me, int hpChange);
	public delegate void D_Refreshed(Entity me, Room room);
	public delegate int D_TriggerTarget(Entity me, Entity other, Collider2D collider); // continue on true, stop testing on false

	public D_Kill E_Kill = delegate {};
	public D_Refreshed E_Refreshed = delegate {};
	public D_Attacked E_Attacked = delegate{};
	public D_TriggerTarget E_TriggerTarget = delegate {return 1;};


	public Data.Piece.KId meId;
	public KType meType;
	public List<Entity.KType> targets;
	public int hp = 10;
	public bool
		isAttackedBullet = true,
		isAttackedBomb = true,
		isAttackedContact = true;


	internal bool 
		isContinueTriggerCheck = true,
		isAlive = true;
	internal NWeapon.Weapon weapon = new NWeapon.Weapon();
	internal List<NBehaviour.Behaviour> bhvs = new List<NBehaviour.Behaviour> ();




	public virtual void Awake(){

	}
	// Use this for initialization
	public virtual void Start ()
	{
	
	}
	public float posX{get{return Mathf.Round(this.transform.localPosition.x);}}
	public float posY{get{return Mathf.Round(this.transform.localPosition.y);}}
	// Update is called once per frame
	public virtual void KUpdate (Room room)
	{
		for(int i = bhvs.Count-1;  i >= 0;i--){
			var currentBhv = bhvs[i].Update(this,room);
			if(currentBhv != null) bhvs[i] = currentBhv;
			if(!bhvs[i].isAlive){
				bhvs.RemoveAt(i);
				continue;
			}
		}
	
	}
	// Update is called once per frame
	void H_TriggerEnter (Entity me, Entity other)
	{	//Debug.Log ("COUNTER " +other.meType);

		
	}

	public void Refresh(Room room){
		E_Refreshed (this, room);
	}

	public void HpChange(HpChangeType type, int change){
		if (!isAttackedBomb && type == HpChangeType.Bomb) return;
		if (!isAttackedBullet && type == HpChangeType.Bullet) return;
		if (!isAttackedContact && type == HpChangeType.Contact)return;
		this.hp += change;
		if (change < 0) {
			E_Attacked(this,change);
		}
		if (hp <= 0) Kill ();
	}

	public void Attack(Room room, Vector3 direction){
		weapon.Attack (this, room,direction);
	}
	public void Kill(){
		//E_Kill (this, room);
		this.isAlive = false;

	}
	public void Terminate(){
		Destroy (this.gameObject);
	}
	public virtual void Init(){
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if (!isContinueTriggerCheck) return;
		//Debug.Log ("ETRIGGER ENTER");
		var other = collider.GetComponent<EntityPointer> ();
		if (other == null) return;
		var otherEntity = other.entity;
		//Debug.Log ("ETRIGGER ENTER CALLING");
		
		for(int i = 0 ; i < targets.Count;i++){
			if(otherEntity.meType == targets[i]){
				int result = E_TriggerTarget(this,otherEntity, collider);
				if(result == 1) isContinueTriggerCheck = true;
				else if(result == 0) break;
				else if(result == -1) isContinueTriggerCheck = false;
				//Debug.Log("FOUND IT "+this.gameObject.name );
				//isContinueTriggerCheck = E_TriggerTarget (this, otherEntity);
				if(!isContinueTriggerCheck) break;
			}
		}
		
	}

}

