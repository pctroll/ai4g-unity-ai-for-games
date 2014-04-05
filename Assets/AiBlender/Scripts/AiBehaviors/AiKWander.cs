using UnityEngine;
using System.Collections;

public class AiKWander : AiBehaviour {

	public float m_MaxRotation;

	public float Angular;

	public Color m_ColorVelocity = Color.white;
	void Start () {
		base.Init();
	}
	
	public override AiSteering GetSteering ()
	{
		if (m_Agent != null) {
			m_Steering.Angular = Random.Range(-1.0f, 1.0f) * m_MaxRotation;
			Angular = m_Steering.Angular;
			m_Steering.Linear = m_Agent.m_MaxSpeed * GetOriAsVec(m_Steering.Angular);
			//m_Steering.Angular = Random.Range(-1.0f, 1.0f) * m_MaxRotation;
			Debug.DrawRay(gameObject.transform.position, m_Agent.Velocity);
		}
		return new AiSteering();
	}
}
