using UnityEngine;
using System.Collections.Generic;


namespace NWorld.NEntity.NUnit.NOrder {
	
	public class Order
	{
		public enum State{Processing, Success,Failure};
		public delegate void D_UpdateMethod(World world,Unit unit);
		D_UpdateMethod E_UpdateMethod;
		public State stateMe = State.Processing;

		public Order(World world, Entity entity){
			E_UpdateMethod = UpdateInit;

		}
		public virtual bool Init(World world, Unit unit){
			return true;
		}
		public void Update(World world, Unit unit){
			E_UpdateMethod (world, unit);
		}
		public void UpdateInit(World world, Unit unit){
			UpdateInit_Process (world, unit);
			E_UpdateMethod = Update_Process;
			if (stateMe == State.Processing) {
				E_UpdateMethod(world,unit);
			}
		}
		public virtual void UpdateInit_Process(World world, Unit unit){

		}
		public virtual void Update_Process(World world, Unit unit){
			this.stateMe = State.Success;
		}
	}
	
}
