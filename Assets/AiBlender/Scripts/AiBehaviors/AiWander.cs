using UnityEngine;
using System.Collections;

public class AiWander : AiFace {

	public float m_MaxAccel;
	public float m_WanderOffset;
	public float m_WanderRadius = 1.0f;
	public float m_WanderRate;
	private float m_WanderOrientation;
	// Use this for initialization
	void Start () {
		Init();
	}

	public override void Init ()
	{
		m_Target = new GameObject();
		m_Target.transform.position = gameObject.transform.position;
		base.Init ();
	}

	// Update is called once per frame
	void Update () {
		if (m_Agent != null) {
			m_Agent.SetSteering(GetSteering());
		}
	}

	public override AiSteering GetSteering () {
		if (m_Agent != null && m_Target != null) {
			m_WanderOrientation += Random.Range(-1.0f, 1.0f) * m_WanderRate;
			float targetOrientation = m_WanderOrientation + m_Agent.Orientation;
			Vector3 orientationVec = GetOriAsVec(m_Agent.Orientation);
			Vector3 circleCenter = (m_WanderOffset * orientationVec) + gameObject.transform.position;
			Vector3 targetCenter = circleCenter + (GetOriAsVec(targetOrientation) * m_WanderRadius);
			m_FaceTarget.transform.position = targetCenter;
			m_Steering = base.GetSteering();
			m_Steering.Linear = m_FaceTarget.transform.position - gameObject.transform.position;
			m_Steering.Linear = m_Steering.Linear.normalized * m_MaxAccel;
			if (m_DrawLines) {
				DrawCircle(circleCenter, Color.red, m_WanderRadius);
				Debug.DrawRay(gameObject.transform.position, circleCenter - gameObject.transform.position, Color.red);
				Debug.DrawRay(gameObject.transform.position, targetCenter - gameObject.transform.position);
			}
		}
		return m_Steering;
	}
}
