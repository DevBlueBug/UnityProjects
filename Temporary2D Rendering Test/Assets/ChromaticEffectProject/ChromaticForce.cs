using UnityEngine;
public class ChromaticForce
{
	public int turn;
	public float x,y,magnitude,distance;
	public Vector3 position;
	public ChromaticForce (float x, float y, float force,float distance)
	{
		this.x = x;this.y = y;
		this.magnitude = force;
		this.distance = distance;
		position = new Vector3 (x, y, 0);
	}
}