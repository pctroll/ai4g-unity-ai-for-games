using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviour for pursuing a target.
/// It's based on Seek plus a predicttion variable.
/// </summary>
public class AiPursue : AiSeek {
    /// <summary>
    /// Maximum prediction
    /// </summary>
	public float m_MaxPrediction;
	/// <summary>
	/// Real target to evade. The behaviour's original member
    /// is used for computing its steering
	/// </summary>
	GameObject m_PursueTarget;

	// Use this for initialization
	void Start () {
		base.Init();
		m_PursueTarget = m_Target;
		m_Target = new GameObject();
	}

    /// <summary>
    /// Returns the steering.
    /// </summary>
    /// <returns></returns>
	public override AiSteering GetSteering ()
	{
		if (m_Agent != null && m_PursueTarget != null) {
			Vector3 direction = m_PursueTarget.transform.position - gameObject.transform.position;
			float distance = direction.magnitude;
			float speed = m_Agent.Velocity.magnitude;
			float prediction = 0.0f;
			if (speed <= distance / m_MaxPrediction) {
				prediction = m_MaxPrediction;
			}
			else {
				prediction = distance / speed;
			}
			m_Target.transform.position = m_PursueTarget.transform.position;
			Vector3 targetPrediction = m_PursueTarget.GetComponent<AiAgent>().Velocity * prediction;
			m_Target.transform.position += targetPrediction;
		}
		return base.GetSteering ();
	}
}
