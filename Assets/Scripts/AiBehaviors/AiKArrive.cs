using UnityEngine;
using System.Collections;

public class AiKArrive : AiBehavior {
	
	GameObject m_Character;
	GameObject m_Target;
	float m_MaxSpeed;
	float m_Radius;
	float m_TimeToTarget;

	public AiKArrive (GameObject character, GameObject target, float maxSpeed, float radius, float timeToTarget) : base () {
		m_Character = character;
		m_Target = target;
		m_MaxSpeed = maxSpeed;
		m_Radius = radius;
		m_TimeToTarget = timeToTarget;
	}

	public override AiSteering GetSteering ()
	{
		m_Steering.Linear = m_Target.transform.position - m_Character.transform.position;
		if (m_Steering.Linear.sqrMagnitude < m_Radius * m_Radius) {
			return m_Steering;
		}
		m_Steering.Linear /= m_TimeToTarget;
		if (m_Steering.Linear.sqrMagnitude > m_MaxSpeed * m_MaxSpeed) {
			m_Steering.Linear = m_Steering.Linear.normalized * m_MaxSpeed;
		}
		SetNewOrientation(m_Character.transform, m_Steering.Linear);
		return m_Steering;
	}
}
