using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviour for fleeing from a target
/// </summary>
public class AiFlee : AiBehaviour {
	/// <summary>
	/// The agent's maximum acceleration.
	/// </summary>
	public float m_MaxAccel;
	/// <summary>
	/// The color of the vector line.
	/// </summary>
	public Color m_LineColor = Color.white;

	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		base.Init();
	}

    /// <summary>
    /// Returns the steering.
    /// </summary>
    /// <returns></returns>
	public override AiSteering GetSteering ()
	{
		if (m_Agent != null && m_Target != null) {
			AiSteering steering = new AiSteering();
			steering.Linear = m_Character.transform.position - m_Target.transform.position;
			steering.Linear = steering.Linear.normalized * m_MaxAccel;
			m_Agent.SetSteering(steering);
			if (m_DrawLines) {
				Debug.DrawRay(m_Character.transform.position, m_Agent.Velocity, m_LineColor);
			}
		}
		return base.GetSteering ();
	}
}
