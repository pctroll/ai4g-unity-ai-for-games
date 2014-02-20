using UnityEngine;
using System.Collections;

public class AiKFlee : AiBehavior {

	GameObject m_Escaper, m_Target;
	float m_MaxSpeed;
	bool m_IsSetUp;
	public AiKFlee (GameObject escaper, GameObject target, float maxSpeed) : base () {
		m_Escaper = escaper;
		m_Target = target;
		m_MaxSpeed = maxSpeed;
		m_IsSetUp = true;
		if (m_MaxSpeed < 0.1f) {
			m_MaxSpeed = 0.1f;
		}
		if (escaper == null || target == null)
			m_IsSetUp = false;
	}

	public override AiSteering GetSteering ()
	{
		if (!m_IsSetUp) {
			return m_Steering;
		}
		m_Steering.Linear = m_Escaper.transform.position - m_Target.transform.position;
		m_Steering.Linear = m_Steering.Linear.normalized * m_MaxSpeed;
		SetNewOrientation(m_Escaper.transform, m_Steering.Linear);
		return m_Steering;
	}
}
