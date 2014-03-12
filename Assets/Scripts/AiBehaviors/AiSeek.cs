using UnityEngine;
using System.Collections;

public class AiSeek : AiBehavior {

	public Color LineColor;
	GameObject m_Character;
	GameObject m_Target;
	float m_MaxAccel;

	void Start () {
		base.Start();
	}

	void Update () {
		m_Character = m_Agent.gameObject;
		m_Target = m_Agent.Target;
		if (m_Agent != null && m_Character != null && m_Target != null) {
			m_MaxAccel = m_Agent.MaxAccel;
			AiSteering steering = new AiSteering();
			steering.Linear = m_Target.transform.position - m_Character.transform.position;
			steering.Linear = steering.Linear.normalized * m_MaxAccel;
			m_Agent.SetSteering(steering);
			if (DrawLines) {
				Debug.DrawRay(m_Character.transform.position, m_Agent.Velocity, LineColor);
			}
		}
	}
}