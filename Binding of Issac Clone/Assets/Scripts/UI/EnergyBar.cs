using UnityEngine;
using System.Collections;

public class EnergyBar : MonoBehaviour
{
	public GameObject P_Bar;

	public GameObject bar;
	public int count, countMax;

	GameObject ObjBars;
	// Use this for initialization
	void Awake(){
	}
	void Start ()
	{
		
		ObjBars = new GameObject ("Bars");
		ObjBars.transform.parent = this.transform;
		ObjBars.transform.localPosition = Vector3.zero;	
		ObjBars.transform.localScale = Vector3.one;	
		ObjBars.transform.localRotation = Quaternion.Euler(Vector3.zero);
		SetCount (P_Bar,ObjBars.transform,countMax);
		SetCountBar (count, countMax);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	GameObject HelperInstantiateRatio(GameObject Prefab, Vector3 scale){

		var objNew = Instantiate (Prefab);
		objNew.transform.localScale =
			Vector3.Scale (objNew.transform.localScale, scale);


		return objNew;
	}
	public void SetCountBar(float count, float countMax){
		count =  Mathf.Min (count, countMax);
		float size = count / countMax;
		bar.transform.localScale = new Vector3 (1, size,1);
		bar.transform.localPosition = new Vector3 (0,-.5f + (1/countMax/2)*count,0);

	}
	public void SetCount(GameObject prefab, Transform parent, int n){
		for (int i =1; i < n; i++) {
			var bar = Instantiate (prefab);
			bar.transform.localScale =
				Vector3.Scale (bar.transform.localScale, parent.lossyScale);
			

			bar.transform.parent= parent;
			bar.transform.localPosition = 
				new Vector3(0,-.5f +(1.0f/n) * i,0);
			bar.transform.localRotation = Quaternion.Euler(Vector3.zero);

		}
	}
}

