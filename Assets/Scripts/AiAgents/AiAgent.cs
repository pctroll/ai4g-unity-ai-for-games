using UnityEngine;
using System.Collections;

public class AiAgent : MonoBehaviour {

	public GameObject Target;
	public float MaxAccel;
	public float MaxSpeed;
	public float TargetRadius;
	public float SlowRadius;
	public float TimeToTarget;

	float m_Orientation;
	float m_Rotation;
	Vector3 m_Velocity;
	AiSteering m_Steering;

	public Vector3 Velocity
	{
		get { return m_Velocity; }
	}

	// Use this for initialization
	void Start () {
		//Debug.Log("Start AiAgent");
		m_Velocity = Vector3.zero;
		m_Orientation = 0.0f;
		m_Steering = new AiSteering();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Update AiAgent");

		Vector3 translation = m_Velocity * Time.deltaTime;
		//transform.position += m_Velocity * Time.deltaTime;
		transform.Translate(translation, Space.World);

		m_Orientation = transform.rotation.eulerAngles.y;
		m_Orientation += m_Rotation * Time.deltaTime;
		transform.Rotate(Vector3.up, m_Orientation, Space.Self);
	}

	void LateUpdate () {
		//Debug.Log("LateUpdate AiAgent");
		m_Velocity += m_Steering.Linear * Time.deltaTime;
		m_Orientation += m_Steering.Angular * Time.deltaTime;
		if (m_Velocity.sqrMagnitude > MaxSpeed * MaxSpeed) {
			m_Velocity = m_Velocity.normalized * MaxSpeed;
		}
	}

	public void SetSteering (AiSteering steering, bool isAdditive = false) {
		//Debug.Log("SetSteering AiAgent");
		if (!isAdditive) {
			m_Steering = steering;
		}
		else {
			// TODO
			// pending additive steering behavior for blending
		}
	}
}