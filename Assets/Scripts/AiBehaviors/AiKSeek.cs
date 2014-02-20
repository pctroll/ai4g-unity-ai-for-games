using UnityEngine;
using System.Collections;

public class AiKSeek : AiBehavior {
	GameObject m_Seeker, m_Target;
	float m_MaxSpeed;
	bool m_IsSetUp;
	public AiKSeek (GameObject seeker, GameObject target, float maxSpeed) : base () {
		m_Seeker = seeker;
		m_Target = target;
		m_MaxSpeed = maxSpeed;
		m_IsSetUp = true;
		if (m_MaxSpeed < 0.1f) {
			m_MaxSpeed = 0.1f;
		}
		if (seeker == null || target == null)
			m_IsSetUp = false;
	}

	public override AiSteering GetSteering ()
	{
		if (!m_IsSetUp) {
			return m_Steering;
		}
		m_Steering.Linear = m_Target.transform.position - m_Seeker.transform.position;
		m_Steering.Linear = m_Steering.Linear.normalized * m_MaxSpeed;
		SetNewOrientation(m_Seeker.transform, m_Steering.Linear);
		return m_Steering;
	}
}
