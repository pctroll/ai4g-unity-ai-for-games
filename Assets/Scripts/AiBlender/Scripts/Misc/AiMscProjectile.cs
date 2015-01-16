using UnityEngine;
using System.Collections;
/// <summary>
/// Class for creating the projectile 'behaviour'
/// </summary>
public class AiMscProjectile : MonoBehaviour {

    /// <summary>
    /// Defines whether the projectile has been correctly set
    /// with the proper member function. Works like initialization.
    /// </summary>
	private bool m_IsSet = false;
    /// <summary>
    /// Firing position (origin)
    /// </summary>
	private Vector3 m_FiringPos;
    /// <summary>
    /// Shooting direction
    /// </summary>
	private Vector3 m_Direction;
    /// <summary>
    /// Projectile's speed
    /// </summary>
	private float m_Speed;
    /// <summary>
    /// Time the projectile is fired
    /// </summary>
	private float m_TimeInit;
	
	
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

    /// <summary>
    /// Sets the initial values for the projectile to be correctly fired and updated
    /// </summary>
    /// <param name="firingPos"></param>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
	public void Set (Vector3 firingPos, Vector3 direction, float speed) {
		m_IsSet = true;
		m_TimeInit = Time.time;
		m_FiringPos = firingPos;
		m_Direction = direction;
		m_Speed = speed;
	}
}
