using UnityEngine;
using System.Collections;
/// <summary>
/// AI Flee behavior.
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
	/// Update this instance. It's used for updating the agent's steering.
	/// </summary>
	void Update () {
		if (m_Agent != null) {
			m_Agent.SetSteering(GetSteering());
		}
	}

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
