using UnityEngine;
namespace NItem.NWeapon
{
	public class Shotgun : Weapon
	{
		float spreadAngle;
		int spreadCount;
		public Shotgun ():base(-2,80,10,.3f)
		{
			spreadAngle = 2.3f *(3.14f/180.0f);
			spreadCount = 3;
		}
		public override void Process (Entity entity, Room room, UnityEngine.Vector3 direction)
		{
			base.Process (entity, room, direction);
				
			var pos = entity.transform.localPosition;
			float 
				backswing = this.backSwing / spreadCount,
				angle = Mathf.Atan2 (direction.y, direction.x),
				hpChange = this.hpChange / spreadCount;


			for (float i = 0; i< spreadCount; i++) {
				//Debug.Log("COUNT " + i + " : "+Mathf.RoundToInt(i/2.0f + .1f));
				float angleNew = angle + spreadAngle * Mathf.RoundToInt(i/2.0f + .1f)* ((i%2 ==0)?1:-1);
				angleNew += spreadAngle* Random.Range(-.5f,.5f);
				var dir = Utility.EasyMath.GetVectorAngle(angleNew);
				AddBackswing(entity,-dir,backswing);

				AddBullet (room, pos.x, pos.y,hpChange,speed* Random.Range(.7f,1.0f),backswing, dir);
			}


			//AddBullet(room,pos.x,pos.y, speed * Random.Range(.8f,1.1f),backSwing,angle + angleDistorted));
			//AddBullet(room,pos.x,pos.y, speed* Random.Range(.8f,1.1f),backSwing, Utility.EasyMath.GetVectorAngle(angle - angleSpread));

		}
	}
}

