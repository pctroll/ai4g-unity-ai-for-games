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
			m_Agent.SetSteering(GetSteering());
		}
	}

	public override AiSteering GetSteering ()
	{
		if (m_Agent != null) {
			if (m_Agent.Velocity.sqrMagnitude > 0.0f) {
				Vector3 forward = gameObject.transform.position + m_Agent.Velocity;
				m_Target.transform.LookAt(forward);
				m_Target.GetComponent<AiAgent>().Orientation = m_Target.transform.rotation.eulerAngles.y;
			}
		}
		return base.GetSteering ();
	}
}
