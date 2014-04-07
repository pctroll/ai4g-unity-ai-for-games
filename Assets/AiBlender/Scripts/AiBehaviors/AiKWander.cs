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

	public override void Update () {
		if (m_Agent != null) {
			m_Agent.SetSteering(GetSteering(), m_Weight);
		}
	}
	
	public override AiSteering GetSteering () {
		if (m_Agent != null) {
			Vector3 v = GetOriAsVec(m_Agent.Orientation);
			float f = m_MaxRotation * Random.Range(-1.0f, 1.0f);
			Vector3 t = GetOriAsVec(f) * 2.0f + v;
			t.y = m_Agent.transform.position.y;
			Debug.Log(t);
			Debug.DrawRay(transform.position, v + transform.position - transform.position, Color.yellow);
			Debug.DrawRay(transform.position, t - transform.position, Color.red);
			//m_Steering.Linear = m_Agent.m_MaxSpeed * GetOriAsVec(m_Agent.Orientation);
			//m_Steering.Angular = m_MaxRotation * Random.Range(-1.0f, 1.0f);
			if (m_DrawLines) {
				//Debug.DrawRay(transform.position, GetOriAsVec(m_Agent.Orientation) * 3.0f, Color.red);
				//Debug.DrawRay(transform.position, GetOriAsVec(m_Steering.Angular) + m_Steering.Linear * m_Agent.m_MaxSpeed, m_ColorVelocity);
			}
		}
		return m_Steering;
	}
}
