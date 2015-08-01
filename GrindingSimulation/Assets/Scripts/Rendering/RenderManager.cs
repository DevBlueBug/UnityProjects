using UnityEngine;
using System.Collections.Generic;

public class RenderManager : MonoBehaviour
{
	public Main main;
	public RenderEntity P_RenderEntity;

	public List<RenderEntity> renderEntities;
	void Awake(){
		main.E_WorldCreated += H_WorldCreated;
	}
	void Start ()
	{
		this.transform.localPosition = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < renderEntities.Count; i++) {
			renderEntities[i].KUpdate(Time.deltaTime);
		}
	
	}
	void H_WorldCreated(NWorld.World world){
		world.E_UnitAdded += H_NewUnit;

	}
	void H_NewUnit(NWorld.World world, NWorld.NUnit.Unit unit){
		var r = Instantiate (P_RenderEntity).Init (unit);
		r.transform.parent = this.transform;
		renderEntities.Add(r);
	}
}

