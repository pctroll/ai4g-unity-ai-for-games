using UnityEngine;
using System.Collections;
/// <summary>
/// FSM state for chasing a target
/// </summary>
public class AiStChase : MonoBehaviour {
    /// <summary>
    /// Next state
    /// </summary>
	public MonoBehaviour m_Next;
    /// <summary>
    /// Seek behaviour component
    /// </summary>
	public AiSeek m_Seek;
    /// <summary>
    /// Behaviour to look where the agent is
    /// going to
    /// </summary>
	public AiLookWhereYoureGoing m_Look;
    /// <summary>
    /// Distance required to change the state
    /// </summary>
	public float m_DistanceToAbandon;
    /// <summary>
    /// Current distance from target
    /// </summary>
	private float m_Distance;

	// Use this for initialization
	void Start () {
		m_Seek.enabled = this.enabled;
		m_Look.enabled = this.enabled;
	}

	void OnEnable () {
		Debug.LogWarning("hello chaser");
		m_Seek.enabled = true;
		m_Look.enabled = true;
	}

	void OnDisable () {
		Debug.LogWarning("bye chaser");
		m_Seek.enabled = false;
		m_Look.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 positionTarget = m_Seek.m_Target.transform.position;
		Vector3 positionObject = gameObject.transform.position;
		m_Distance = Vector3.Distance(positionTarget, positionObject);
		if (m_Distance >= m_DistanceToAbandon) {
			this.enabled = false;
			m_Next.enabled = true;
		}
	}
}
