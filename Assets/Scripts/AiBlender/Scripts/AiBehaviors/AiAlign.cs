using UnityEngine;
using System.Collections;

public class AiAlign : AiBehaviour {

	public float m_MaxAngularAccel;

	public float m_MaxRotation;

	public float m_TargetRadius;

	public float m_SlowRadius;

	public float m_TimeToTarget = 0.1f;

	// Use this for initialization
	void Start () {
		base.Init();
	}
	
	// Update is called once per frame
	public override void Update () {
		if (m_Agent != null) {
			m_Agent.SetSteering(GetSteering());
		}
	}

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
