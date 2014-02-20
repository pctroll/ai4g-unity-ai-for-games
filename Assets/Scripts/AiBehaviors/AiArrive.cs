using UnityEngine;
using System.Collections;

public class AiArrive : AiBehavior {

	GameObject m_Character;
	GameObject m_Target;
	float m_MaxAccel;
	float m_MaxSpeed;
	float m_TargetRadius;
	float m_SlowRadius;
	float m_TimeToTarget;

	public AiArrive (GameObject character, GameObject target, float maxAccel, float maxSpeed, float targetRadius, float slowRadius, float timeToTarget)
		: base () {
		m_Character = character;
		m_Target = target;
		m_MaxAccel = maxAccel;
		m_MaxSpeed = maxSpeed;
		m_TargetRadius = targetRadius;
		m_SlowRadius = slowRadius;
		m_TimeToTarget = timeToTarget;
	}

	public override AiSteering GetSteering ()
	{
		float distance, targetSpeed;
		Vector3 targetVelocity;
		Vector3 characterVelocity = m_Character.GetComponent<AiAgent> ().Velocity;
		m_Steering.Linear = m_Target.transform.position - m_Character.transform.position;
		distance = m_Steering.Linear.sqrMagnitude;
		if (distance < m_TargetRadius * m_TargetRadius) {
			return new AiSteering();
		}
		if (distance > m_SlowRadius * m_SlowRadius) {
			targetSpeed = m_MaxSpeed;
		} else {
			targetSpeed = m_MaxSpeed * distance / m_SlowRadius;
		}
		targetVelocity = m_Steering.Linear;
		targetVelocity = targetVelocity.normalized * targetSpeed;
		m_Steering.Linear = targetVelocity - characterVelocity;
		m_Steering.Linear /= m_TimeToTarget;
		if (m_Steering.Linear.sqrMagnitude > m_MaxAccel) {
			m_Steering.Linear = m_Steering.Linear.normalized * m_MaxAccel;
		}
		return m_Steering;
	}
}
