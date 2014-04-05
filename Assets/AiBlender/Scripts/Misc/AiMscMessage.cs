using UnityEditor;
using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class AiMscMessage : MonoBehaviour {

	private string m_MsgTitle;
	public string m_MsgBody;
	GUIStyle m_MsgTitleStyle;
	GUIStyle m_MsgBodyStyle;
	GUIStyle m_MsgStyle;

	void OnGUI () {
		m_MsgStyle = new GUIStyle(GUI.skin.box);
		m_MsgTitleStyle = new GUIStyle(GUI.skin.label);
		m_MsgTitleStyle.fontSize = (int)(Screen.height * 0.05f);
		m_MsgTitleStyle.alignment = TextAnchor.UpperCenter;
		m_MsgBodyStyle = new GUIStyle(GUI.skin.label);
		m_MsgBodyStyle.fontSize = (int)(Screen.height * 0.04f);
		m_MsgBodyStyle.alignment = TextAnchor.UpperCenter;
		m_MsgTitle = Application.loadedLevelName;
#if UNITY_EDITOR
		string[] name = EditorApplication.currentScene.Split('/');
		m_MsgTitle = name[name.Length-1].Replace(".unity", "");
#endif
		GUILayout.BeginVertical(m_MsgStyle);
		GUILayout.Label(m_MsgTitle, m_MsgTitleStyle, GUILayout.Width((float)Screen.width));
		GUILayout.Label(m_MsgBody, m_MsgBodyStyle, GUILayout.Width((float)Screen.width));
		GUILayout.EndVertical();
	}
}
