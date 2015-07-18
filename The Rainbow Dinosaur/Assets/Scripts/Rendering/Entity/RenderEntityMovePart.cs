using UnityEngine;
using System.Collections.Generic;

public class RenderEntityMovePart : MonoBehaviour
{
	public List<Animator> animators;
	public RenderSprite renderSPR;
	public void SetTrigger(string trigger){
		for (int i = 0; i < animators.Count; i++) {
			animators[i].SetTrigger(trigger);
		}
	}
}

