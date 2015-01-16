using UnityEngine;
using System.Collections;
/// <summary>
/// Behavour for fleeing for a target kinematically
/// </summary>
public class AiKFlee : AiBehaviour {

	// Use this for initialization
	void Start () {
		base.Init();
	}
	/// <summary>
	/// Returns the steering.
	/// </summary>
	/// <returns></returns>
	public override AiSteering GetSteering () {
		if (m_Agent!= null && m_Target != null) {
			m_Steering.Linear = gameObject.transform.position - m_Target.transform.position;
			m_Steering.Linear = m_Steering.Linear.normalized * m_Agent.m_MaxSpeed;
			m_Agent.Orientation = GetNewOrientation(m_Agent.Orientation, m_Steering.Linear);
		}
		return m_Steering;
	}
}
