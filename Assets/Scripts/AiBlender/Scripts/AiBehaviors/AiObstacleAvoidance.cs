using UnityEngine;
using System.Collections;
/// <summary>
/// Behaviour for avoiding walls. Used along with blending
/// and movement behaviours such as Seek or Flee.
/// </summary>
public class AiObstacleAvoidance : AiSeek {
    /// <summary>
    /// Distance for a wall to be considered harmful
    /// and to be avoided
    /// </summary>
	public float m_AvoidDistance;
    /// <summary>
    /// Sight length for screening walls
    /// </summary>
	public float m_LookAhead;
    /// <summary>
    /// Layer for ray casting
    /// </summary>
	//private int m_Layer;

	// Use this for initialization
	void Start () {
		Init();
	}
    /// <summary>
    /// Sets up the ray casting layer
    /// </summary>
	public override void Init () {
		base.Init ();
		m_Target = new GameObject();
		m_Target.name = "AiObstacleAvoidance";
		//m_Layer = LayerMask.NameToLayer("AiWall");
	}

    /// <summary>
    /// Returns the steering.
    /// </summary>
    /// <returns></returns>
	public override AiSteering GetSteering () {
		m_Steering = new AiSteering();
		if (m_Agent != null) {
			Vector3 position = gameObject.transform.position;
			Vector3 rayVector = m_Agent.Velocity.normalized * m_LookAhead;
			rayVector += position;
			Vector3 direction = rayVector - position;
			RaycastHit hit;
			if (m_DrawLines) {
				Debug.DrawRay(gameObject.transform.position, direction, Color.red);
			}
			if (Physics.Raycast(position, direction, out hit, m_LookAhead)) {
				position = hit.point + hit.normal * m_AvoidDistance;
				m_Target.transform.position = position;
				m_Steering = base.GetSteering();
			}
		}
		return m_Steering;
	}
}
