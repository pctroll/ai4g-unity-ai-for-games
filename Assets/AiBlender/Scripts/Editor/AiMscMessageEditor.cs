using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(AiMscMessage))]
public class AiMscMessageEditor : Editor {

	public override void OnInspectorGUI ()
	{
		AiMscMessage t = target as AiMscMessage;
		EditorStyles.textField.wordWrap = true;
		t.m_MsgBody = EditorGUILayout.TextArea(t.m_MsgBody, GUILayout.MinHeight(50));
	}
}
