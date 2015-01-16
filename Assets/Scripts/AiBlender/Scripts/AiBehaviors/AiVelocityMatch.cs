using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviour for matching the velocity of a target agent.
/// </summary>
public class AiVelocityMatch : AiBehaviour {
    /// <summary>
    /// Maximum acceleration
    /// </summary>
	public float m_MaxAccel;
    /// <summary>
    /// Time to apply the behaviour
    /// </summary>
	public float m_TimeToTarget = 0.1f;
    /// <summary>
    /// Color for drawing the velocity (debugging)
    /// </summary>
	public Color m_LineColor = Color.white;

	// Use this for initialization
	void Start () {
		base.Init();
	}
	
    /// <summary>
    /// Returns the steering.
    /// </summary>
    /// <returns></returns>
    public override AiSteering GetSteering()
    {
        AiSteering steering = new AiSteering();
        if (m_Agent != null && m_Target != null)
        {
            Vector3 targetVel = m_Target.GetComponent<AiAgent>().Velocity;
            steering.Linear = targetVel - m_Agent.Velocity;
            steering.Linear = steering.Linear / m_TimeToTarget;
            if (steering.Linear.sqrMagnitude > m_MaxAccel * m_MaxAccel)
            {
                steering.Linear = steering.Linear.normalized * m_MaxAccel;
            }
            if (m_DrawLines)
            {
                Debug.DrawRay(gameObject.transform.position, steering.Linear);
            }
        }
        return steering;
    }
}
