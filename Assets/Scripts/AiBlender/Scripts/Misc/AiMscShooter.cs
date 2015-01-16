using UnityEngine;
using System.Collections;
/// <summary>
/// Player controller for firing projectiles
/// </summary>
public class AiMscShooter : MonoBehaviour {

    /// <summary>
    /// Projectile's prefab
    /// </summary>
	public GameObject m_Projectile;
    /// <summary>
    /// Up velocity
    /// </summary>
	public float m_Up = 0.5f;
    /// <summary>
    /// Shooting speed
    /// </summary>
	public float m_Speed = 1.0f;
    /// <summary>
    /// Agent component
    /// </summary>
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

    /// <summary>
    /// Transform the orientation float value to vector
    /// </summary>
    /// <param name="orientation"></param>
    /// <returns></returns>
	public Vector3 GetOriAsVec (float orientation) {
		Vector3 vector  = Vector3.zero;
		vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
		vector.z = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1.0f;
		return vector.normalized;
	}
}
