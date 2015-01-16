using UnityEngine;
using System.Collections;

public class AiPursue : AiSeek {

	public float m_MaxPrediction;

	GameObject m_PursueTarget;

	// Use this for initialization
	void Start () {
		base.Init();
		m_PursueTarget = m_Target;
		m_Target = new GameObject();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Agent != null) {
			m_Agent.SetSteering(GetSteering());
		}
	}

	public override AiSteering GetSteering ()
	{
		if (m_Agent != null && m_PursueTarget != null) {
			Vector3 direction = m_PursueTarget.transform.position - gameObject.transform.position;
			float distance = direction.magnitude;
			float speed = m_Agent.Velocity.magnitude;
			float prediction = 0.0f;
			if (speed <= distance / m_MaxPrediction) {
				prediction = m_MaxPrediction;
			}
			else {
				prediction = distance / speed;
			}
			m_Target.transform.position = m_PursueTarget.transform.position;
			Vector3 targetPrediction = m_PursueTarget.GetComponent<AiAgent>().Velocity * prediction;
			m_Target.transform.position += targetPrediction;
		}
		return base.GetSteering ();
	}
}
