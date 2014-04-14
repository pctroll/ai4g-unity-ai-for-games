using UnityEngine;
using System.Collections;

public class AiObstacleAvoidance : AiSeek {

	public float m_AvoidDistance;

	public float m_LookAhead;

	private int m_Layer;
	// Use this for initialization
	void Start () {
		Init();
	}

	public override void Init () {
		base.Init ();
		m_Target = new GameObject();
		m_Target.name = "AiObstacleAvoidance";
		m_Layer = LayerMask.NameToLayer("AiWall");
	}
	
	// Update is called once per frame
	public override void Update () {
		if (m_Agent != null) {
			m_Agent.SetSteering(GetSteering());
		}
	}

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
