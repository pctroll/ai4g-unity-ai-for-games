using UnityEngine;
using System.Collections;

public class AiStWander : MonoBehaviour {

	public MonoBehaviour m_Next;
	public AiWander m_Wander;
	public float m_DistanceToChase;
	public GameObject m_Target;

	void OnEnable () {
		m_Wander.enabled = true;
	}

	void OnDisable () {
		m_Wander.enabled = false;
	}

	// Use this for initialization
	void Start () {
		m_Wander.enabled = this.enabled;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 positionTarget = m_Target.transform.position;
		Vector3 positionObject = gameObject.transform.position;
		float distance = Vector3.Distance(positionTarget, positionObject);
		if (distance <= m_DistanceToChase) {
			this.enabled = false;
			m_Next.enabled = true;
		}
	}
}
