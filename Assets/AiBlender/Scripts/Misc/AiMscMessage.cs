using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class AiMscMessage : MonoBehaviour {

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
		GUILayout.BeginVertical(m_MsgStyle);
		GUILayout.Label(Application.loadedLevelName, m_MsgTitleStyle, GUILayout.Width((float)Screen.width));
		GUILayout.Label(m_MsgBody, m_MsgBodyStyle, GUILayout.Width((float)Screen.width));
		GUILayout.EndVertical();
	}
}
