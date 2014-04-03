using UnityEditor;
using UnityEngine;
using System.Collections;
[CustomEditor(typeof(AiSeek))]
public class AiSeekEditor : Editor {

	public override void OnInspectorGUI ()
	{
		AiSeek t = (AiSeek)target;
		t.m_Weight = EditorGUILayout.Slider("Weight", t.m_Weight, AiEditor.MIN_WEIGHT, AiEditor.MAX_WEIGHT);
		if (GUI.changed)
			EditorUtility.SetDirty(t);
		//base.OnInspectorGUI ();

	}
}
