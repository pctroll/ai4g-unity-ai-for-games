using UnityEngine;
using System.Collections;

public class PlayerController : AiAgent {

	public GameObject m_Crosshair;

	/// <summary>
	/// Starts this instance. Initialization.
	/// </summary>
	void Start () {
		//Debug.Log("Start AiAgent");
		m_Velocity = Vector3.zero;
		m_Orientation = 0.0f;
		m_Steering = new AiSteering();
		m_Crosshair.gameObject.transform.position = gameObject.transform.position;
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		//Debug.Log("Update AiAgent");
		m_Steering = new AiSteering();
		Vector3 translation = m_Velocity * Time.deltaTime;
		transform.Translate(translation, Space.World);
		
		m_Orientation = transform.rotation.eulerAngles.y;
		m_Orientation += m_Rotation * Time.deltaTime;
		transform.Rotate(Vector3.up, m_Orientation, Space.Self);
		
		// Movement
		m_Velocity.x = Input.GetAxis("Horizontal");
		m_Velocity.z = Input.GetAxis("Vertical");
		m_Velocity = Quaternion.AngleAxis(45, Vector3.up) * m_Velocity;
		m_Velocity *= m_MaxSpeed;
		
		// Crosshair
		m_Crosshair.transform.position = GetScreenToWorldPos(Input.mousePosition);
		gameObject.transform.LookAt(m_Crosshair.transform.position);
	}
	
	/// <summary>
	/// Updates everything after calculating behaviors.
	/// </summary>
	void LateUpdate () {
		//Debug.Log("LateUpdate AiAgent");
		/*m_Velocity += m_Steering.Linear * Time.deltaTime;
		m_Rotation += m_Steering.Angular * Time.deltaTime;
		if (m_Velocity.sqrMagnitude > m_MaxSpeed * m_MaxSpeed) {
			m_Velocity = m_Velocity.normalized * m_MaxSpeed;
		}
		
		if (m_Steering.Linear.Equals (Vector3.zero))
			m_Velocity = Vector3.zero;
		m_Blended = false;*/
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

