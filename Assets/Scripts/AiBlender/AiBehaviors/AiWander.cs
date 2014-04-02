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

	public override AiSteering GetSteering ()
	{
		if (m_Agent != null && m_Target != null) {
			m_WanderOrientation = Random.Range(-1.0f, 1.0f) * m_WanderRate;
			float targetOrientation = m_WanderOrientation + m_Agent.Orientation;
			Vector3 position = gameObject.transform.position;
			float orientation = m_Agent.Orientation;
			Vector3 orientationVec = GetOriAsVec(orientation);
			Vector3 circleCenter = (m_WanderOffset * orientationVec) + position;
			DrawCircle(circleCenter, Color.red, m_WanderRadius);
			m_FaceTarget.transform.position = (m_WanderOffset * orientationVec) + position;
			Vector3 targetCenter = circleCenter + GetOriAsVec(targetOrientation);
			Debug.DrawRay(position, targetCenter - position);
			m_FaceTarget.transform.position += GetOriAsVec(targetOrientation);
			m_Steering = base.GetSteering();
			m_Steering.Linear = m_MaxAccel * orientationVec;
		}
		return m_Steering;
	}
}
