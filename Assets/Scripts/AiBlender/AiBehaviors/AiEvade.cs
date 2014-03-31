using UnityEngine;
using System.Collections;

public class AiEvade : AiFlee {

	public float m_MaxPrediction;
	
	GameObject m_EvadeTarget;
	
	// Use this for initialization
	void Start () {
		base.Init();
		m_EvadeTarget = m_Target;
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
		Vector3 direction = m_EvadeTarget.transform.position - gameObject.transform.position;
		float distance = direction.magnitude;
		float speed = m_Agent.Velocity.magnitude;
		float prediction = 0.0f;
		if (m_Agent != null && m_Target != null) {
			if (speed <= distance / m_MaxPrediction) {
				prediction = m_MaxPrediction;
			}
			else {
				prediction = distance / speed;
			}
			m_Target.transform.position = m_EvadeTarget.transform.position;
			Vector3 targetPrediction = m_EvadeTarget.GetComponent<PlayerController>().Velocity * prediction;
			m_Target.transform.position += targetPrediction;
		}
		return base.GetSteering ();
	}
}
