using UnityEngine;
using System.Collections;

public class AiStChase : MonoBehaviour {

	public MonoBehaviour m_Next;

	public AiSeek m_Seek;
	public AiLookWhereYoureGoing m_Look;
	public float m_DistanceToAbandon;

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
