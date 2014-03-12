using UnityEngine;
using System.Collections;

public class AiArrive : AiBehavior {

	public Color SlowRadius;
	public Color TargetRadius;
	GameObject m_Character;
	GameObject m_Target;
	float m_MaxAccel;
	float m_MaxSpeed;
	float m_TargetRadius;
	float m_SlowRadius;
	float m_TimeToTarget;

	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		m_Character = m_Agent.gameObject;
		m_Target = m_Agent.Target;
		m_TargetRadius = Mathf.Clamp(m_Agent.TargetRadius, 0.1f, Mathf.Infinity);
		m_SlowRadius = Mathf.Clamp(m_Agent.SlowRadius, 0.1f, Mathf.Infinity);
		m_TimeToTarget = Mathf.Clamp(m_Agent.TimeToTarget, 0.0f, Mathf.Infinity);
		m_MaxSpeed = m_Agent.MaxSpeed;
		m_MaxAccel = m_Agent.MaxAccel;
		if (m_Agent != null && m_Character != null && m_Target != null) {
			AiSteering steering = new AiSteering();
			Vector3 direction = m_Target.transform.position - m_Character.transform.position;
			float distance = direction.sqrMagnitude;
			// Changed this validation in order to fulfill goal
			if (distance >= m_TargetRadius * m_TargetRadius) {
				float targetSpeed = 0.0f;
				if (distance >= m_SlowRadius * m_SlowRadius) {
					targetSpeed = m_MaxSpeed;
				}
				else {
					targetSpeed = m_MaxSpeed * distance / (m_SlowRadius * m_SlowRadius);
				}
				Vector3 targetVelocity = direction.normalized * targetSpeed;
				steering.Linear = targetVelocity - m_Agent.Velocity;
				steering.Linear = steering.Linear / m_TimeToTarget;
				if (steering.Linear.sqrMagnitude > m_MaxAccel * m_MaxAccel)
					steering.Linear = steering.Linear.normalized * m_MaxAccel;

			}
			m_Agent.SetSteering(steering);
			if (DrawLines) {
				DrawCircle(m_Target.transform.position, TargetRadius, m_TargetRadius);
				DrawCircle(m_Target.transform.position, SlowRadius, m_SlowRadius);
			}
		}
	}
}
