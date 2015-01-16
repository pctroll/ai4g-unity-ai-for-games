using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviour for arriving to a target inematically
/// </summary>
public class AiKArrive : AiBehaviour {
    /// <summary>
    /// Target radius for stopping
    /// </summary>
	public float m_Radius = 2.0f;
    /// <summary>
    /// Time for applying the behaviour
    /// </summary>
	public float m_TimeToTarget = 0.25f;

	// Use this for initialization
	void Start () {
		base.Init();
	}

    /// <summary>
    /// Returns the steering.
    /// </summary>
    /// <returns></returns>
	public override AiSteering GetSteering ()
	{
		if (m_Agent!= null && m_Target != null) {
			m_Steering.Linear = m_Target.transform.position - m_Agent.transform.position;
			if (m_Steering.Linear.sqrMagnitude < m_Radius * m_Radius) {
				return new AiSteering();
			}
			m_Steering.Linear /= m_TimeToTarget;
			if (m_Steering.Linear.sqrMagnitude > m_Agent.m_MaxSpeed * m_Agent.m_MaxSpeed) {
				m_Steering.Linear = m_Steering.Linear.normalized * m_Agent.m_MaxSpeed;
			}
			m_Agent.Orientation = GetNewOrientation(m_Agent.Orientation, m_Steering.Linear);
			//SetNewOrientation(gameObject.transform, m_Steering.Linear);
		}
		return m_Steering;
	}
}
