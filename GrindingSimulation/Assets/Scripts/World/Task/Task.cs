using UnityEngine;
using System.Collections.Generic;

namespace NWorld.NTask{
	
	public class Task 
	{
		public KEnums.TypeTask type;
		public List<KEnums.TypeSkill> skillsNeeded = new List<KEnums.TypeSkill>();

		
		public virtual void Init(World world, NWorld.NEntity.NUnit.Unit unit){
			
		}
		
		public virtual void Update(World world, NWorld.NEntity.NUnit.Unit unit){
			
		}

	}
	

}