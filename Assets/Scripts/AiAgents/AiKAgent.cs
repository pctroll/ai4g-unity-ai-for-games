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
		m_WanderTimer = WanderTime;
		m_Steering = new AiSteering();
	}
	
	// Update is called once per frame
	void Update () {
		//m_Behavior = new AiKSeek(gameObject, Target, MaxSpeed);
		//m_Behavior = new AiKFlee(gameObject, Target, MaxSpeed);
		//m_Behavior = new AiKArrive(gameObject, Target, MaxSpeed, Radius, TimeToTarget);
		if (m_WanderTimer >= WanderTime) {
			m_Behavior = new AiKWander (gameObject, MaxSpeed, MaxRotation);
			m_WanderTimer = 0;
		} else {
			m_Behavior = new AiKMaintain(m_Steering);
		}
		m_Steering = m_Behavior.GetSteering();
		transform.position += m_Steering.Linear * Time.deltaTime;
		m_WanderTimer += Time.deltaTime;
	}
}
