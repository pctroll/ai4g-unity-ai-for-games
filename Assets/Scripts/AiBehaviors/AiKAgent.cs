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
	AiBehavior m_Behavior;
	AiSteering m_Steering;
	// Use this for initialization
	void Start () {
		m_WanderTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//m_Behavior = new AiKSeek(gameObject, Target, MaxSpeed);
		//m_Behavior = new AiKFlee(gameObject, Target, MaxSpeed);
		//m_Behavior = new AiKArrive(gameObject, Target, MaxSpeed, Radius, TimeToTarget);
		if (m_WanderTimer >= WanderTime) {
			m_Behavior = new AiKWander(gameObject, MaxSpeed, MaxRotation);
			m_WanderTimer = 0;
		}
		m_WanderTimer += Time.deltaTime;
		m_Steering = m_Behavior.GetSteering();
		transform.position += m_Steering.Linear * Time.deltaTime;
	}
}
