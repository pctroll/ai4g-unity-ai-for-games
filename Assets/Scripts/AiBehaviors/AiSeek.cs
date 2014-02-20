using UnityEngine;
using System.Collections;

public class AiSeek : AiBehavior {

	GameObject m_Character;
	GameObject m_Target;
	float m_MaxAccel;

	public AiSeek (GameObject character, GameObject target, float maxAccel) : base () {
		m_Character = character;
		m_Target = target;
		m_MaxAccel = maxAccel;
	}

	public override AiSteering GetSteering ()
	{
		m_Steering.Linear = m_Target.transform.position - m_Character.transform.position;
		m_Steering.Linear = m_Steering.Linear.normalized * m_MaxAccel;
		return m_Steering;
	}
}
