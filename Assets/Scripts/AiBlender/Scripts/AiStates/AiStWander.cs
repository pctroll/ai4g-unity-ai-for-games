using UnityEngine;
using System.Collections;
/// <summary>
/// FSM state for wandering around
/// </summary>
public class AiStWander : MonoBehaviour {
    /// <summary>
    /// Next FSM state
    /// </summary>
	public MonoBehaviour m_Next;
    /// <summary>
    /// Wander behaviour component
    /// </summary>
	public AiWander m_Wander;
    /// <summary>
    /// Distance to decide chasing
    /// </summary>
	public float m_DistanceToChase;
    /// <summary>
    /// Target object (must have an agent component)
    /// </summary>
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
