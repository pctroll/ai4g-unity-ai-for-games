using UnityEngine;
using System.Collections;

public class AiSeparation : AiBehaviour {

	public float m_Threshold;
	public float m_DecayCoefficient;
	public float m_MaxAccel;
	GameObject[] m_Targets;

	// Use this for initialization
	void Start () {
		Init();
		m_Targets = GameObject.FindGameObjectsWithTag("AiAgent");
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Agent != null) {
			m_Agent.SetSteering(GetSteering(), m_Weight);
		}
	}

	public override AiSteering GetSteering ()
	{
		m_Steering = new AiSteering();
		if (m_Agent != null && m_Targets.Length > 0) {
			for (int i = 0; i < m_Targets.Length; i++) {
				if (m_Targets[i] == this.gameObject) {
					continue;
				}
				Vector3 direction = gameObject.transform.position - m_Targets[i].transform.position;
				float distance = direction.sqrMagnitude;
				float strenght = 0.0f;
				if (distance < (m_Threshold * m_Threshold)) {
					strenght = Mathf.Min(m_DecayCoefficient / distance, m_MaxAccel);
					m_Steering.Linear += strenght * direction.normalized;
				}
			}
		}
		return m_Steering;
	}
}
