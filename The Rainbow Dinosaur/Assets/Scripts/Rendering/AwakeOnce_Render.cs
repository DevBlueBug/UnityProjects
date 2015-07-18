using UnityEngine;
using System.Collections;

public class AwakeOnce_Render : MonoBehaviour
{
	public Prefabs_RenderEntityEquipped prefabsA;
	public Prefabs_RenderItem prefabsB;

	void Awake(){
		prefabsA.Init ();
		prefabsB.Init ();
		Destroy (this);
	}
}

