using UnityEngine;
using System.Collections;

public class AiKWander : AiBehaviour {

	public float m_MaxRotation;

	public float Angular;
	public float Orientation;
	public Color m_ColorVelocity = Color.white;
	void Start () {
		base.Init();
	}
	
	public override AiSteering GetSteering () {
		if (m_Agent != null) {
			Orientation = m_Agent.Orientation;
			Angular = Random.Range(-1.0f, 1.0f) * m_MaxRotation;
			Debug.Log(Orientation + Angular + " = " + Orientation + " + " + Angular);
			m_Steering.Angular = Angular;
			if (m_DrawLines) {
				Debug.DrawRay(transform.position, GetOriAsVec(m_Steering.Angular) * m_Agent.m_MaxSpeed);
			}
		}
		return m_Steering;
	}
}
