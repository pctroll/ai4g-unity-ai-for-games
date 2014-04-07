using UnityEngine;
using System.Collections;

public class AiMscPlayer : AiAgent {

	public GameObject m_Crosshair;

	/// <summary>
	/// Starts this instance. Initialization.
	/// </summary>
	void Start () {
		//Debug.Log("Start AiAgent");
		m_Velocity = Vector3.zero;
		m_Orientation = 0.0f;
		if (m_Crosshair != null) {
			m_Crosshair.gameObject.transform.position = gameObject.transform.position;
		}
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	public override void Update () {
		m_Steering = new AiSteering();
		// Movement
		m_Velocity.x = Input.GetAxis("Horizontal");
		m_Velocity.z = Input.GetAxis("Vertical");
		m_Velocity = Quaternion.AngleAxis(45, Vector3.up) * m_Velocity;
		m_Velocity *= m_MaxSpeed;
		// Crosshair
		if (m_Crosshair != null) {
			m_Crosshair.transform.position = GetScreenToWorldPos(Input.mousePosition);
			gameObject.transform.LookAt(m_Crosshair.transform.position);
		}
		else {
			gameObject.transform.LookAt(m_Velocity + transform.position);
		}

		Vector3 translation = m_Velocity * Time.deltaTime;
		transform.Translate(translation, Space.World);
		m_Orientation = transform.rotation.eulerAngles.y;
	}

	
	Vector3 GetScreenToWorldPos (Vector3 mousePosition) {
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		float playerY = gameObject.transform.position.y;
		Plane plane = new Plane(Vector2.up, new Vector3(0.0f, playerY, 0.0f));
		float distance = 0;
		if (plane.Raycast(ray, out distance)) {
			return ray.GetPoint(distance);
		}
		return Vector3.zero;
	}
}

