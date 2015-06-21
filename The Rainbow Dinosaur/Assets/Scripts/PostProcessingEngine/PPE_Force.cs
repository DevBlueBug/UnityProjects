using UnityEngine;
public class PPE_Force
{
	public int turn;
	public float x,y,magnitude,distance;
	public Vector3 position;
	public PPE_Force (float x, float y, float force,float distance)
	{
		this.x = x;this.y = y;
		this.magnitude = force;
		this.distance = distance;
		this.turn = 1;
		position = new Vector3 (x, y, 0);
	}
	public PPE_Force (float x, float y, float force,float distance,int turn)
	{
		this.x = x;this.y = y;
		this.magnitude = force;
		this.distance = distance;
		this.turn = turn;
		position = new Vector3 (x, y, 0);
	}
}