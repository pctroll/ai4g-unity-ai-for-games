using UnityEngine;
using System.Collections;

public class AiArrive : AiBehaviour {


	public float m_MaxAccel;
	public float m_TargetRadius = 0.5f;
	public float m_SlowRadius = 1.0f;
	public float m_TimeToTarget = 0.1f;

	public Color m_SlowRadiusColor = Color.yellow;
	public Color m_TargetRadiusColor = Color.red;

	float m_MaxSpeed;

	// Use this for initialization
	void Start () {
		base.Init();
	}
	
	// Update is called once per frame
	public override void Update () {
		//m_Character = m_Agent.gameObject;
		m_TargetRadius = Mathf.Clamp(m_TargetRadius, 0.1f, Mathf.Infinity);
		m_SlowRadius = Mathf.Clamp(m_SlowRadius, 0.1f, Mathf.Infinity);
		m_TimeToTarget = Mathf.Clamp(m_TimeToTarget, 0.0f, Mathf.Infinity);
		m_MaxSpeed = m_Agent.m_MaxSpeed;
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
			if (m_DrawLines) {
				DrawCircle(m_Target.transform.position, m_TargetRadiusColor, m_TargetRadius);
				DrawCircle(m_Target.transform.position, m_SlowRadiusColor, m_SlowRadius);
			}
		}
	}

	public override AiSteering GetSteering ()
	{
		return base.GetSteering ();
	}
}
