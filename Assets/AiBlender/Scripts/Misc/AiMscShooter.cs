using UnityEngine;
using System.Collections;

public class AiMscShooter : MonoBehaviour {

	public GameObject m_Projectile;
	public float m_Up = 0.5f;
	public float m_Speed = 1.0f;
	private AiAgent m_Agent;

	// Use this for initialization
	void Start () {
		m_Agent = GetComponent<AiAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 firingPos = gameObject.transform.position;
			Vector3 direction = GetOriAsVec(m_Agent.Orientation);
			direction.y = m_Up;
			GameObject projectile = Instantiate(m_Projectile, firingPos, new Quaternion()) as GameObject;
			projectile.GetComponent<AiMscProjectile>().Set(firingPos, direction, m_Speed);
		}
	}

	public Vector3 GetOriAsVec (float orientation) {
		Vector3 vector  = Vector3.zero;
		vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
		vector.z = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1.0f;
		return vector.normalized;
	}
}
