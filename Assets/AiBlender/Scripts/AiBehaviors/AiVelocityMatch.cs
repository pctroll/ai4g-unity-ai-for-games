using UnityEngine;
using System.Collections;

public class AiVelocityMatch : AiBehaviour {

	public float m_MaxAccel;

	public float m_TimeToTarget = 0.1f;

	public Color m_LineColor = Color.white;

	// Use this for initialization
	void Start () {
		base.Init();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Agent != null && m_Target != null) {
			Vector3 targetVel = m_Target.GetComponent<AiAgent>().Velocity;
			m_Steering.Linear = targetVel - m_Agent.Velocity;
			m_Steering.Linear = m_Steering.Linear / m_TimeToTarget;
			if (m_Steering.Linear.sqrMagnitude > m_MaxAccel * m_MaxAccel) {
				m_Steering.Linear = m_Steering.Linear.normalized * m_MaxAccel;
			}
			m_Agent.SetSteering(m_Steering);
			if (m_DrawLines) {
				Debug.DrawRay(gameObject.transform.position, m_Steering.Linear);
			}
		}
	}
}
