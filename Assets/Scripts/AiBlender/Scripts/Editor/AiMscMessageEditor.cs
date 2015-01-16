using UnityEngine;
using UnityEditor;
using System.Collections;
/// <summary>
/// Custom editor for the OnScreen message
/// </summary>
[CustomEditor(typeof(AiMscMessage))]
public class AiMscMessageEditor : Editor {

	public override void OnInspectorGUI ()
	{
		AiMscMessage t = target as AiMscMessage;
		EditorStyles.textField.wordWrap = true;
		t.m_MsgBody = EditorGUILayout.TextArea(t.m_MsgBody, GUILayout.MinHeight(50));
	}
}
