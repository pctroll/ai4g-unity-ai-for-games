using UnityEngine;
using System.Collections;

public class AiKSeek : AiBehaviour {

	// Use this for initialization
	void Start () {
		base.Init();
	}

	public override AiSteering GetSteering () {
		if (m_Agent!= null && m_Target != null) {
			m_Steering.Linear = m_Target.transform.position - gameObject.transform.position;
			m_Steering.Linear = m_Steering.Linear.normalized * m_Agent.m_MaxSpeed;
			SetNewOrientation(gameObject.transform, m_Steering.Linear);
		}
		return m_Steering;
	}
}
