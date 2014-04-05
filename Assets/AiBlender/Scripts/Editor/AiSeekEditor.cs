using UnityEditor;
using UnityEngine;
using System.Collections;
[CustomEditor(typeof(AiSeek))]
public class AiSeekEditor : Editor {

	public override void OnInspectorGUI ()
	{
		AiSeek t = (AiSeek)target;

		t.m_Target = (GameObject)EditorGUILayout.ObjectField("Target", t.m_Target, typeof(GameObject), true);
		t.m_Weight = EditorGUILayout.Slider("Weight", t.m_Weight, AiEditor.MIN_WEIGHT, AiEditor.MAX_WEIGHT);
		t.m_MaxAccel = EditorGUILayout.FloatField("Max Acceleration", t.m_MaxAccel);
		t.m_DrawLines = EditorGUILayout.Toggle("Draw Lines", t.m_DrawLines);
		t.m_LineColor = EditorGUILayout.ColorField("Line Color", t.m_LineColor);

		t.m_MaxAccel = Mathf.Clamp(t.m_MaxAccel, AiEditor.MIN_ACCEL, AiEditor.MAX_ACCEL);
		if (GUI.changed)
			EditorUtility.SetDirty(t);
		//base.OnInspectorGUI ();
	}
}
