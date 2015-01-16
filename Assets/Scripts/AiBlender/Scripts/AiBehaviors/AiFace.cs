using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviour for facing a target agent given
/// angular acceleration. It's based on Align.
/// </summary>
public class AiFace : AiAlign {
    /// <summary>
    /// Real target to face. The behaviour's original
    /// variable is used for computing the steering.
    /// </summary>
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
	

    /// <summary>
    /// Returns the steering.
    /// </summary>
    /// <returns></returns>
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
