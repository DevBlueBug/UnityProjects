using UnityEngine;
using System.Collections.Generic;


namespace NWorld.NEntity.NUnit{
	
	public class Unit : Entity
	{

		internal EquipmentSlots equipments = new EquipmentSlots();
		internal Stats statsAfter = new Stats();

		NTask.Task taskCurrent = null;
		List<NNeed.Need> needs = new List<NNeed.Need> ();
		Queue<NOrder.Order> orders = new Queue<NOrder.Order>();
		List<NSkill.Skill> skills = new List<NSkill.Skill>();

		public Unit(int x, int y):base(x,y){
			EntityRequestHandler.LinkBasic (this);

		}

		
		public bool AddOrder(World world, NOrder.Order order){
			var result = order.Init (world, this);
			if (result)
				orders.Enqueue (order);
			return result;
		}
		public override void Update (World world)
		{
			for (int i = needs.Count-1; i > -1; i--){
				needs[i].Update(world,this);
			}
			if (orders.Count == 0) {
				var need = GetNeedUrgent();
				if(need !=null) need.UpdateAction(world,this);
			} else {
				var order = orders.Peek();
				order.Update(world,this);
				if(order.stateMe == NWorld.NEntity.NUnit.NOrder.Order.State.Success){
					orders.Dequeue();
				}else if(order.stateMe == NWorld.NEntity.NUnit.NOrder.Order.State.Failure) {
					orders.Clear();
				}
				else{
					Debug.Log(order + " " +order.stateMe);
				}

			}
			base.Update (world);
		}
		public bool Move(int x, int y){
			return false;
		}
		bool IsTaskPossible(NTask.Task task){
			foreach (var s in task.skillsNeeded) {
				bool hasSkill = false;
				for(int i = 0 ; i < skills.Count;i++){
					if(skills[i].type == s){
						hasSkill = true;
						break;
					}
				}
				if(!hasSkill) return false;
			}
			return true;
		}
		public void AddSkill(NSkill.Skill skill){
		}
		public void AddNeed(NNeed.Need need){
			need.Init (this);
			this.needs.Add (need);
		}
		public bool SetTask(World world,NTask.Task task){
			if (IsTaskPossible (task)) {
				taskCurrent = task;
				taskCurrent.Init(world,this);
				return true;
			}
			return false;
		}
		NNeed.Need GetNeedUrgent(){
			NNeed.Need need = null;
			float point = 0;
			for(int i = 0 ; i < needs.Count;i++){
				if(needs[i].point > point){
					point = needs[i].point;
					need = needs[i];
				}
			}
			return need;
		}
	}

}
