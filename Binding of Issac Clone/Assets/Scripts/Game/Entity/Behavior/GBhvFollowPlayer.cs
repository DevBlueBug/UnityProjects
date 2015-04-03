//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using Utility;
using Game.Entity.Task;
namespace Game.Entity.Behavior
{
	public class GBhvFollowPlayer : GBehavior
	{
		public float interval; //smaller the unit more accurate 
		EasyTimer timer;
		public void Start(){
			timer = new EasyTimer (999, interval);
		}
		public override void Do (GEntity entity, GRoom room)
		{
			if (!timer.Tick (Time.deltaTime)) return;
			
			//Debug.Log ("HI");
			var node = room.mapAstar.GetPath (
				new Vector2 ( Mathf.RoundToInt(entity.position.x),Mathf.RoundToInt(entity.position.z)),
			new Vector2 (Mathf.RoundToInt(GameMaster.Player.position.x),
			             Mathf.RoundToInt(GameMaster.Player.position.z)));


			System.Collections.Generic.Stack<Vector2> positions = new System.Collections.Generic.Stack<Vector2> ();

			int count = 0;
			while (node.nodePrevious != null && count++ < 20) {
				//Debug.Log("node was at " + node.x + " "  + node.y + " , " + count);
				positions.Push(new Vector2(node.x,node.y));
				node = node.nodePrevious;
			}
			count = 0;
			entity.myTasks = new System.Collections.Generic.List<GTask> ();
			while (positions.Count > 0 && count++ < 20) {
				var p = positions.Pop();
				entity.AddTask( new GTaskMove(new Vector3(p.x,0,p.y) ) );
			}
			return;
		}
	}
}

