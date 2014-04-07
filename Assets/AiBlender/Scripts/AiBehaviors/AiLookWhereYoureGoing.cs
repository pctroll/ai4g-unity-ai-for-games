using UnityEngine;
using System.Collections;

public class AiLookWhereYoureGoing : AiAlign {


	// Use this for initialization
	void Start () {
		base.Init();
		m_Target = new GameObject();
		m_Target.AddComponent<AiAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Agent != null) {
			m_Agent.SetSteering(GetSteering(), m_Weight);
		}
	}

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
