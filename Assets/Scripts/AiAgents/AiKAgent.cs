using UnityEngine;
using System.Collections;

public class AiKAgent : MonoBehaviour {

	public GameObject Target;
	public float MaxSpeed;
	public float TimeToTarget;
	public float Radius;
	public float MaxRotation;
	public float WanderTime;
	float m_WanderTimer;
	Vector3 m_Velocity;
	float m_Orientation;
	float m_Rotation;
	AiBehavior m_Behavior;
	AiSteering m_Steering;
	// Use this for initialization
	void Start () {
		m_WanderTimer = WanderTime;
		m_Steering = new AiSteering();
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += m_Velocity * Time.deltaTime;
		m_Orientation = m_Rotation;


		//m_Behavior = new AiKSeek(gameObject, Target, MaxSpeed);
		//m_Behavior = new AiKFlee(gameObject, Target, MaxSpeed);
		//m_Behavior = new AiKArrive(gameObject, Target, MaxSpeed, Radius, TimeToTarget);
		m_Behavior = new AiKWander (gameObject, MaxSpeed, MaxRotation);
		/*if (m_WanderTimer >= WanderTime) {
			m_Behavior = new AiKWander (gameObject, MaxSpeed, MaxRotation);
			m_WanderTimer = 0;
		} else {
			m_Behavior = new AiKMaintain(m_Steering);
		}
		m_WanderTimer += Time.deltaTime;*/

		m_Steering = m_Behavior.GetSteering();
		m_Velocity += m_Steering.Linear * Time.deltaTime;
		m_Rotation = m_Steering.Angular * Time.deltaTime;
	}
}
