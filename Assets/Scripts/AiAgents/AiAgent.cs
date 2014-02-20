using UnityEngine;
using System.Collections;

public class AiAgent : MonoBehaviour {

	public GameObject Target;
	public float MaxAccel;
	public float MaxSpeed;
	public float TargetRadius;
	public float SlowRadius;
	public float TimeToTarget;

	Vector3 m_Velocity;
	float m_Orientation;
	AiBehavior m_Behavior;
	AiSteering m_Steering;

	public Vector3 Velocity
	{
		get { return m_Velocity; }
	}

	// Use this for initialization
	void Start () {
		m_Velocity = Vector3.zero;
		m_Orientation = 0.0f;
		m_Steering = new AiSteering();
	}
	
	// Update is called once per frame
	void Update () {
		//m_Behavior = new AiSeek(gameObject, Target, MaxAccel);
		m_Behavior = new AiFlee(gameObject, Target, MaxAccel);
		//m_Behavior = new AiArrive (gameObject, Target, MaxAccel, MaxSpeed, TargetRadius, SlowRadius, TimeToTarget);
		m_Steering = m_Behavior.GetSteering();
		transform.position += m_Velocity * Time.deltaTime;
		//transform.rotation = transform.rotation + Quaternion.AngleAxis(m_Orientation, Vector3.up) * m_Velocity;
		m_Velocity += m_Steering.Linear * Time.deltaTime;
		m_Orientation += m_Steering.Angular * Time.deltaTime;
		if (m_Velocity.sqrMagnitude > MaxSpeed * MaxSpeed) {
			m_Velocity = m_Velocity.normalized * MaxSpeed;
		}
	}
}
