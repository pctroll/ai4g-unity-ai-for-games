using UnityEngine;
using System.Collections;

public class AiMscProjectile : MonoBehaviour {

	private bool m_IsSet = false;
	private Vector3 m_FiringPos;
	private Vector3 m_Direction;
	private float m_Speed;
	private float m_TimeInit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!m_IsSet)
			return;
		float time = Time.time - m_TimeInit;
		gameObject.transform.position = m_FiringPos + m_Direction * m_Speed * time;
		gameObject.transform.position += Physics.gravity * (time * time) / 2.0f;
		if (gameObject.transform.position.y < -1.0f)
			Destroy(this.gameObject);
	}

	public void Set (Vector3 firingPos, Vector3 direction, float speed) {
		m_IsSet = true;
		m_TimeInit = Time.time;
		m_FiringPos = firingPos;
		m_Direction = direction;
		m_Speed = speed;
	}
}
