using UnityEngine;
using System.Collections;

public class AiKWander : AiBehavior {

	GameObject m_Character;
	float m_MaxSpeed;
	float m_MaxRotation;
	float m_Rand;

	public AiKWander (GameObject character, float maxSpeed, float maxRotation) : base () {
		m_Character = character;
		m_MaxSpeed = maxSpeed;
		m_MaxRotation = maxRotation;
		m_Rand = Random.value - Random.value;
	}

	public override AiSteering GetSteering ()
	{
		m_Steering = new AiSteering();
		m_Steering.Linear = m_Character.transform.position;
		if (m_Steering.Linear.Equals(Vector3.zero)) {
			m_Steering.Linear += new Vector3(0.1f, 0.0f, 0.1f);
		}
		m_Steering.Linear = m_Steering.Linear.normalized * m_MaxSpeed;
		m_Steering.Linear = Quaternion.AngleAxis(m_MaxRotation * m_Rand, Vector2.up) * m_Steering.Linear;
		SetNewOrientation(m_Character.transform, m_Steering.Linear);
		return m_Steering;
	}
}
