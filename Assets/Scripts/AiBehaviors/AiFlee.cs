using UnityEngine;
using System.Collections;

public class AiFlee : MonoBehaviour {

	public float Weight;
	public bool DrawLines;
	public Color LineColor;

	AiAgent m_Agent;
	float m_MaxAccel;
	GameObject m_Character;
	GameObject m_Target;
	
	void Start () {
		m_Agent = gameObject.GetComponent<AiAgent>();
		if (m_Agent == null)
			Debug.LogError("No AiAgent component found");
	}
	
	void Update () {
		m_Character = m_Agent.gameObject;
		m_Target = m_Agent.Target;
		if (m_Agent != null && m_Character != null && m_Target != null) {
			m_MaxAccel = m_Agent.MaxAccel;
			AiSteering steering = new AiSteering();
			steering.Linear = m_Character.transform.position - m_Target.transform.position;
			steering.Linear = steering.Linear.normalized * m_Agent.MaxAccel;
			m_Agent.SetSteering(steering);
			if (DrawLines) {
				Vector3 destination = m_Character.transform.position + steering.Linear;
				Debug.DrawRay(m_Character.transform.position, destination, LineColor);
			}
		}
	}
}
