using UnityEngine;
using System.Collections;

public class AiFace : AiAlign {

	protected GameObject m_FaceTarget;

	// Use this for initialization
	void Start () {
		Init();
	}

	public override void Init ()
	{
		base.Init ();
		m_FaceTarget = m_Target;
		m_Target = new GameObject();
		m_Target.AddComponent<AiAgent>();
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
			Vector3 direction = m_FaceTarget.transform.position - gameObject.transform.position;
			// inverse validation
			if (direction.sqrMagnitude > 0.0f) {
				// Unity's way of doing things
				m_Target.transform.LookAt(m_FaceTarget.transform.position);
				m_Target.GetComponent<AiAgent>().Orientation = m_Target.transform.rotation.eulerAngles.y;
			}
		}
		return base.GetSteering();
	}
}
