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
			if (direction.magnitude > 0.0f) {
				m_Target.GetComponent<AiAgent>().Orientation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
			}
		}
		return base.GetSteering();
	}
}
