using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviour for an agent to wander around kinematically
/// </summary>
public class AiKWander : AiBehaviour {
    /// <summary>
    /// Maximum rotation in a given frame
    /// </summary>
	public float m_MaxRotation;
    /// <summary>
    /// Color for the wandering velocity (debugging)
    /// </summary>
	public Color m_ColorVelocity = Color.white;
	void Start () {
		base.Init();
	}

	/// <summary>
	/// Returns the steering
	/// </summary>
	/// <returns></returns>
	public override AiSteering GetSteering () {
		m_Steering = new AiSteering();
		if (m_Agent != null) {
			m_Steering.Linear = m_Agent.m_MaxSpeed * GetOriAsVec(m_Agent.Orientation);
			m_Steering.Angular = m_MaxRotation * Random.Range(-1.0f, 1.0f);
			m_Agent.Orientation = GetNewOrientation(m_Agent.Orientation, m_Steering.Linear);
			if (m_DrawLines) {
				Debug.DrawRay(transform.position, GetOriAsVec(m_Agent.Orientation) * 1.5f, m_ColorVelocity);
			}
		}
		return m_Steering;
	}
}
