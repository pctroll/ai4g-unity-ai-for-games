using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviour for aligning to a given target agent
/// </summary>
public class AiAlign : AiBehaviour {
    /// <summary>
    /// Maximum angular acceleration
    /// </summary>
	public float m_MaxAngularAccel;
    /// <summary>
    /// Maximum rotation allowed in a frame
    /// </summary>
	public float m_MaxRotation;
    /// <summary>
    /// Radius threshold for an alignment to be
    /// considered correct
    /// </summary>
	public float m_TargetRadius;
    /// <summary>
    /// Radius threshold for slowing down angular
    /// velocity during alignment
    /// </summary>
	public float m_SlowRadius;
    /// <summary>
    /// Time before applying the beahaviour
    /// </summary>
	public float m_TimeToTarget = 0.1f;

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
		if (m_Agent != null && m_Target != null) {
			float targetOrientation = m_Target.GetComponent<AiAgent>().Orientation;
			float characterOrientation = m_Character.GetComponent<AiAgent>().Orientation;
			float rotation = targetOrientation - characterOrientation;
			rotation = MapToRange(rotation);
			float rotationSize = Mathf.Abs(rotation);
			float targetRotation = 0.0f;
			if (rotationSize >= m_TargetRadius) {
				if (rotationSize > m_SlowRadius)
					targetRotation = m_MaxRotation;
				else
					targetRotation = m_MaxRotation * rotationSize / m_SlowRadius;
				targetRotation *= rotation / rotationSize;
				m_Steering.Angular = targetRotation - m_Agent.Rotation;
				m_Steering.Angular /= m_TimeToTarget;
				float angularAccel = Mathf.Abs(m_Steering.Angular);
				if (angularAccel > m_MaxAngularAccel) {
					m_Steering.Angular /= angularAccel;
					m_Steering.Angular *= m_MaxAngularAccel;
				}
				m_Agent.SetSteering(m_Steering);
			}
		}
		return base.GetSteering ();
	}
}
