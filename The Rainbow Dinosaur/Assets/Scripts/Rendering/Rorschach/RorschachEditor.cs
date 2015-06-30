using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Rorschach) )]
public class RorschachEditor : Editor
{
	
	Rorschach rorschach;
	void OnEnable(){
		rorschach = target as Rorschach;
		Undo.undoRedoPerformed += RefreshCreator;
	}
	void OnDisable(){
		Undo.undoRedoPerformed -= RefreshCreator;
	}
	void RefreshCreator(){
		if (Application.isPlaying) {
			rorschach.Refresh();
		}
	}
	public override void OnInspectorGUI (){
		EditorGUI.BeginChangeCheck ();
		DrawDefaultInspector ();
		if (EditorGUI.EndChangeCheck () ) {
			RefreshCreator();
		}
	}
}

