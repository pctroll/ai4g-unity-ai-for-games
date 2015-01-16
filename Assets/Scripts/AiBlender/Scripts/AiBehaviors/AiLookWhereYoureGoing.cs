using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviour for aligning the agent to the velocity
/// vector. Used along with blending and other behaviours
/// such as Seek or Flee.
/// </summary>
public class AiLookWhereYoureGoing : AiAlign {

	// Use this for initialization
	void Start () {
		base.Init();
		m_Target = new GameObject();
		m_Target.AddComponent<AiAgent>();
	}

    /// <summary>
    /// Returns the steering
    /// </summary>
    /// <returns></returns>
	public override AiSteering GetSteering ()
	{
		if (m_Agent != null) {
			if (m_Agent.Velocity.sqrMagnitude > 0.0f) {
				float orientation = Mathf.Atan2(m_Agent.Velocity.x, m_Agent.Velocity.z) * Mathf.Rad2Deg;
				m_Target.GetComponent<AiAgent>().Orientation = orientation;
			}
		}
		return base.GetSteering ();
	}
}
