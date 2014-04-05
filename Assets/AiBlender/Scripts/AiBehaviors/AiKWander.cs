using UnityEngine;
using System.Collections;

public class AiKWander : AiBehaviour {

	public float m_MaxRotation;

	public float Angular;
	public float Orientation;
	public Color m_ColorVelocity = Color.white;
	void Start () {
		base.Init();
	}

	public override void Update () {
		if (m_Agent != null) {
			m_Agent.SetSteering(GetSteering(), m_Weight);
		}
	}
	
	public override AiSteering GetSteering () {
		if (m_Agent != null) {
			m_Steering.Linear = m_Agent.m_MaxSpeed * GetOriAsVec(m_Agent.Orientation);
			m_Steering.Angular = Random.Range(-1.0f, 1.0f) * m_MaxRotation;
			Angular = m_Steering.Angular;
			if (m_DrawLines) {
				Debug.DrawRay(transform.position, m_Steering.Linear, m_ColorVelocity);
				Debug.DrawRay(transform.position, GetOriAsVec(m_Agent.Orientation) * 3.0f, Color.red);
			}
		}
		return m_Steering;
	}
}
