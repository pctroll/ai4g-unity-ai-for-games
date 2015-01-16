using System;
using UnityEngine;
using System.Collections;
/// <summary>
/// AI Seek behavior.
/// </summary>
[Serializable]
public class AiSeek : AiBehaviour {
	/// <summary>
	/// The agent's maximum acceleration.
	/// </summary>
	public float m_MaxAccel;

	/// <summary>
	/// The color of the vector line.
	/// </summary>
	public Color m_LineColor = Color.white;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		base.Init();
	}

	/// <summary>
	/// Update this instance. It's used for updating the agent's steering.
	/// </summary>
	void Update () {
		if (m_Agent != null) {
			m_Agent.SetSteering(GetSteering(), m_Weight);
		}
	}
	public override AiSteering GetSteering ()
	{
		if (m_Agent != null && m_Target != null) {
			m_Steering.Linear = m_Target.transform.position - m_Character.transform.position;
			m_Steering.Linear = m_Steering.Linear.normalized * m_MaxAccel;
			if (m_DrawLines) {
				Debug.DrawRay(m_Character.transform.position, m_Agent.Velocity, m_LineColor);
			}
		}
		return m_Steering;
	}

}