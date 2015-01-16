using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviour for arriving to a given target
/// </summary>
public class AiArrive : AiBehaviour {
    /// <summary>
    /// Maximum acceleration
    /// </summary>
	public float m_MaxAccel;
    /// <summary>
    /// Maximum speed
    /// </summary>
    float m_MaxSpeed;
    /// <summary>
    /// Radius threshold around the target for arriving
    /// to be considered successful
    /// </summary>
	public float m_TargetRadius = 0.5f;
    /// <summary>
    /// Radius threshold for slowing down the agent's acceleration
    /// </summary>
	public float m_SlowRadius = 1.0f;
    /// <summary>
    /// Time to be considered before applying the behaviour
    /// </summary>
	public float m_TimeToTarget = 0.1f;

    /// <summary>
    /// Color for the slow radius (debugging purposes)
    /// </summary>
	public Color m_SlowRadiusColor = Color.yellow;
    /// <summary>
    /// Color for the target radius (debugging purposes)
    /// </summary>
	public Color m_TargetRadiusColor = Color.red;
	

	// Use this for initialization
	void Start () {
		base.Init();
	}

    /// <summary>
    /// Returns the steering
    /// </summary>
    /// <returns></returns>
	public override AiSteering GetSteering ()
	{
        m_TargetRadius = Mathf.Clamp(m_TargetRadius, 0.1f, Mathf.Infinity);
        m_SlowRadius = Mathf.Clamp(m_SlowRadius, 0.1f, Mathf.Infinity);
        m_TimeToTarget = Mathf.Clamp(m_TimeToTarget, 0.0f, Mathf.Infinity);
        m_MaxSpeed = m_Agent.m_MaxSpeed;
        AiSteering steering = new AiSteering();
        if (m_Agent != null && m_Character != null && m_Target != null)
        {
            Vector3 direction = m_Target.transform.position - m_Character.transform.position;
            float distance = direction.sqrMagnitude;
            // Changed this validation in order to fulfill goal
            if (distance >= m_TargetRadius * m_TargetRadius)
            {
                float targetSpeed = 0.0f;
                if (distance >= m_SlowRadius * m_SlowRadius)
                {
                    targetSpeed = m_MaxSpeed;
                }
                else
                {
                    targetSpeed = m_MaxSpeed * distance / (m_SlowRadius * m_SlowRadius);
                }
                Vector3 targetVelocity = direction.normalized * targetSpeed;
                steering.Linear = targetVelocity - m_Agent.Velocity;
                steering.Linear = steering.Linear / m_TimeToTarget;
                if (steering.Linear.sqrMagnitude > m_MaxAccel * m_MaxAccel)
                    steering.Linear = steering.Linear.normalized * m_MaxAccel;
            }
            
            if (m_DrawLines)
            {
                DrawCircle(m_Target.transform.position, m_TargetRadiusColor, m_TargetRadius);
                DrawCircle(m_Target.transform.position, m_SlowRadiusColor, m_SlowRadius);
            }
        }
        return steering;
	}
}
