using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HealthBar : MonoBehaviour
{
	[System.Serializable]
	public class HeartState{
		public enum KType { Empty, Regular, Soul, Eternal, Black};
		public KType type;
		public bool isHalf;
	}
	public GameObject 
		P_Heart,
		P_HeartHalf,
		P_HeartSoul,
		P_HeartSoulHalf,
		P_HeartEternal,
		P_HeartEternalHalf,
		P_HearthBlack,
		P_HearthBlackHalf;
	public List<GameObject> positions;
	public List<HeartState> heartStates;

	List<GameObject> hearts;
	Dictionary<HeartState.KType, GameObject[]> dicHearts;

	void Awake(){
		dicHearts = new Dictionary<HeartState.KType, GameObject[]> (){
			{HeartState.KType.Regular, new GameObject[]{P_Heart,P_HeartHalf} },
			{HeartState.KType.Soul, new GameObject[]{P_HeartSoul,P_HeartSoulHalf} },
			{HeartState.KType.Eternal, new GameObject[]{P_HeartEternal,P_HeartEternalHalf} },
			{HeartState.KType.Black, new GameObject[]{P_HearthBlack,P_HearthBlackHalf} }
		};
		hearts = positions.Select (s=> (GameObject)null).ToList();


	}
	// Use this for initialization
	void Start ()
	{
		UpdateHearts ();
	}
	void UpdateHearts(){
		for (int i = 0; i < heartStates.Count; i++) {
			//Debug.Log(i);	
			SetHeart(heartStates[i], positions[i].transform, i);
		}
	}
	public void SetHeart(HeartState state,  Transform position,int n){
		if(hearts[n] != null) Destroy(hearts[n]);

		if (state.type == HeartState.KType.Empty) return;
		//Debug.Log (state.type);
		hearts[n] = 
			Utility.EasyInstantiate.InstantiateScale(
				dicHearts[state.type][(state.isHalf)?1:0], position);
		//Debug.Log (hearts [n]);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.L)) {

		}
	
	}
}

