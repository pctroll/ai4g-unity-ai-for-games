using UnityEngine;
using System.Collections;
/// <summary>
/// Ai agent.
/// </summary>
public class AiAgent : MonoBehaviour {

	/// <summary>
	/// The blending propierty.
	/// </summary>
	public bool m_Blend = false;

	/// <summary>
	/// The agent's max speed.
	/// </summary>
	public float m_MaxSpeed;

	/// <summary>
	/// Whether the behaviors must be blended or not.
	/// Internal use.
	/// </summary>
	protected bool m_Blended = false;

	/// <summary>
	/// The agent's orientation.
	/// </summary>
	protected float m_Orientation;

	/// <summary>
	/// The agent's rotation
	/// </summary>
	protected float m_Rotation;

	/// <summary>
	/// The agent's velocity.
	/// </summary>
	protected Vector3 m_Velocity;

	/// <summary>
	/// The agent's resulting steering from coupling behaviors.
	/// </summary>
	protected AiSteering m_Steering;

	/// <summary>
	/// Gets the agent's velocity.
	/// </summary>
	/// <value>The velocity.</value>
	public Vector3 Velocity
	{
		get { return m_Velocity; }
	}

	/// <summary>
	/// Gets the agent's orientation.
	/// </summary>
	/// <value>The orientation.</value>
	public float Orientation
	{
		get { return m_Orientation; }
		set { m_Orientation = value; }
	}

	public float Rotation
	{
		get { return m_Rotation; }
	}

	/// <summary>
	/// Starts this instance. Initialization.
	/// </summary>
	void Start () {
		//Debug.Log("Start AiAgent");
		m_Velocity = Vector3.zero;
		m_Orientation = 0.0f;
		m_Steering = new AiSteering();
		gameObject.transform.Rotate(Vector3.up, m_Orientation, Space.Self);
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
		transform.rotation = new Quaternion();
		m_Orientation += m_Rotation * Time.deltaTime;
		//m_Orientation += 1.0f * Time.deltaTime;
		transform.Rotate(Vector3.up, m_Orientation, Space.Self);
	}

	/// <summary>
	/// Updates everything after calculating behaviors.
	/// </summary>
	void LateUpdate () {
		//Debug.Log("LateUpdate AiAgent");
		m_Velocity += m_Steering.Linear * Time.deltaTime;
		m_Rotation += m_Steering.Angular * Time.deltaTime;
		if (m_Velocity.sqrMagnitude > m_MaxSpeed * m_MaxSpeed) {
			m_Velocity = m_Velocity.normalized * m_MaxSpeed;
		}

		if (m_Steering.Linear.Equals (Vector3.zero))
			m_Velocity = Vector3.zero;
		m_Blended = false;
	}

	/// <summary>
	/// Updates the steering.
	/// </summary>
	/// <param name="steering">Steering.</param>
	/// <param name="isAdditive">If set to <c>true</c> is additive.</param>
	public void SetSteering (AiSteering steering, bool isAdditive = false) {
		//Debug.Log("SetSteering AiAgent");
		if (!m_Blend && !m_Blended) {
			m_Steering = steering;
			m_Blended = true;
		}
		else {
			// TODO pending additive steering behavior for blending
		}
	}
}