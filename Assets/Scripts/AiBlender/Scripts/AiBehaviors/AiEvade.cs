using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviour for evading a target agent.
/// It's based on Flee behaviour plus a prediction variable.
/// </summary>
public class AiEvade : AiFlee {

    /// <summary>
    /// Maximum prediction
    /// </summary>
	public float m_MaxPrediction;
	/// <summary>
	/// Real target to evade. The behaviour's original member
    /// is used for computing its steering
	/// </summary>
	GameObject m_EvadeTarget;
	
	// Use this for initialization
	void Start () {
		base.Init();
		m_EvadeTarget = m_Target;
		m_Target = new GameObject();
	}
	
	/// <summary>
	/// Returns the steering
	/// </summary>
	/// <returns></returns>
	public override AiSteering GetSteering ()
	{
		if (m_Agent != null && m_EvadeTarget != null) {
			Vector3 direction = m_EvadeTarget.transform.position - gameObject.transform.position;
			float distance = direction.magnitude;
			float speed = m_Agent.Velocity.magnitude;
			float prediction = 0.0f;
			if (speed <= distance / m_MaxPrediction) {
				prediction = m_MaxPrediction;
			}
			else {
				prediction = distance / speed;
			}
			m_Target.transform.position = m_EvadeTarget.transform.position;
			Vector3 targetPrediction = m_EvadeTarget.GetComponent<AiAgent>().Velocity * prediction;
			m_Target.transform.position += targetPrediction;
		}
		return base.GetSteering ();
	}
}
