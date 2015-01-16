using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviour for avoiding collisions between agents.
/// It works along with blending and movement behaviours
/// such as Seek and Flee.
/// </summary>
public class AiSeparation : AiBehaviour {
    /// <summary>
    /// Separation distance
    /// </summary>
	public float m_Threshold;
    /// <summary>
    /// Coefficient for applying separation force
    /// </summary>
	public float m_DecayCoefficient;
    /// <summary>
    /// Maximum acceleration.
    /// </summary>
	public float m_MaxAccel;
    /// <summary>
    /// List of other agents to avoid
    /// </summary>
	GameObject[] m_Targets;

	// Use this for initialization
	void Start () {
		Init();
		m_Targets = GameObject.FindGameObjectsWithTag("AiAgent");
	}
	
    /// <summary>
    /// Returns the steering.
    /// </summary>
    /// <returns></returns>
	public override AiSteering GetSteering ()
	{
		m_Steering = new AiSteering();
		if (m_Agent != null && m_Targets.Length > 0) {
			for (int i = 0; i < m_Targets.Length; i++) {
				if (m_Targets[i] == this.gameObject) {
					continue;
				}
				Vector3 direction = gameObject.transform.position - m_Targets[i].transform.position;
				float distance = direction.sqrMagnitude;
				float strenght = 0.0f;
				if (distance < (m_Threshold * m_Threshold)) {
					strenght = Mathf.Min(m_DecayCoefficient / distance, m_MaxAccel);
					m_Steering.Linear += strenght * direction.normalized;
				}
			}
		}
		return m_Steering;
	}
}
