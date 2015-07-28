using UnityEngine;
using System.Collections.Generic;


public class Entity : MonoBehaviour
{
	public enum KType{Nothing =-1, World=0,Player=1,Enemy=2,Bullet=3};
	public enum HpChangeType{Absolute, Bullet,Bomb,Contact};
	public delegate void D_Me(Entity me);
	public delegate void D_MeOther(Entity me, Entity other);
	public delegate void D_Attacked(Entity me, float hpChange);
	public delegate void D_MeRoom(Entity me, Room room);
	public delegate void D_MeItem(NItem.Item item);
	public delegate int D_TriggerTarget(Entity me, Entity other, Collider2D collider); // continue on true, stop testing on false

	public D_Me E_Kill = delegate {};
	public D_MeRoom E_Refreshed = delegate {};
	public D_MeRoom E_Updated = delegate {};
	public D_Attacked E_Attacked = delegate{};
	public D_TriggerTarget E_TriggerTarget = delegate {return 1;};
	public D_MeOther E_NewEntity = delegate { };

	public Data.Piece.KId id;
	public KType meType;
	public List<Entity.KType> targets;
	public float hp = 10;
	public bool
		isAttackedBullet = true,
		isAttackedBomb = true,
		isAttackedContact = true;


	internal bool 
		isContinueTriggerCheck = true,
		isAlive = true;
	NItem.Inventory inventory = new NItem.Inventory();
	internal NItem.NWeapon.Weapon weapon = new NItem.NWeapon.Weapon();
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
		for(int i = 0;  i < bhvs.Count;i++){
			UpdateBHV(room, i);
			/**
			var currentBhv = bhvs[i].Update(this,room);
			if(currentBhv != null) {
				bhvs[i] = currentBhv;
			}
			if(!bhvs[i].isAlive){
				bhvs.RemoveAt(i);
				continue;
			}
			**/
		}
		inventory.KUpdate (this, room);
		this.transform.position = new Vector3 (transform.position.x,transform.position.y,transform.position.y);
		E_Updated (this, room);
	}
	void UpdateBHV(Room room, int i){
		var currentBhv = bhvs[i].Update(this,room);
		if(currentBhv != null) {
			bhvs[i] = currentBhv;
			UpdateBHV(room,i);
		}
		if(!bhvs[i].isAlive){
			bhvs.RemoveAt(i);
			return;
		}
	}
	
	public NItem.Inventory GetInventory(){
		return this.inventory;
	}
	// Update is called once per frame
	void H_TriggerEnter (Entity me, Entity other)
	{	//Debug.Log ("COUNTER " +other.meType);

		
	}

	public virtual void Refresh(Room room){
		E_Refreshed (this, room);
	}

	public void HpChange(HpChangeType type, float change){
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
		if (!isAlive)
			return;
		E_Kill (this);
		this.isAlive = false;

	}
	public void Terminate(){
		Destroy (this.gameObject);
	}
	//gets called when a room enters
	public virtual void Init(){
	}

	internal bool helperIsMyTarget(KType type){
		for (int i = 0; i < targets.Count; i++) {
			if(type == targets[i]){
				return true;
			}
		}
		return false;
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if (!isContinueTriggerCheck) return;
		//Debug.Log ("ETRIGGER ENTER");
		var other = collider.GetComponent<PointerEntity> ();
		if (other == null) return;
		var otherEntity = other.entity;
		//Debug.Log ("ETRIGGER ENTER CALLING");
		if (helperIsMyTarget (otherEntity.meType)) {
			
			int result = E_TriggerTarget(this,otherEntity, collider);
			if(result == 1) isContinueTriggerCheck = true;
			else if(result == 0) return;
			else if(result == -1) isContinueTriggerCheck = false;
			//Debug.Log("FOUND IT "+this.gameObject.name );
			//isContinueTriggerCheck = E_TriggerTarget (this, otherEntity);
			if(!isContinueTriggerCheck) return;

		}
		
	}

}

