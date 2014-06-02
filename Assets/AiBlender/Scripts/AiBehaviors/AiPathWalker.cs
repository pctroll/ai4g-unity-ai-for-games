using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiPathWalker : MonoBehaviour {

	public MonoBehaviour m_Next;

	public AiGraph m_Graph;
	public GameObject m_Target;
	public float m_TargetDistance = 0.5f;

	private GameObject m_MyTarget;
	private AiBehaviour m_Seeker;
	private AiBehaviour m_Arriver;
	private List<GameObject> m_Path;
	private bool m_Arrived = false;

	void OnEnable () {
		m_Path = GetPath();
		m_Arrived = false;
	}

	void OnDisable () {
	}

	void Awake() {
		m_Path = new List<GameObject>();
	}

	// Use this for initialization
	void Start () {
		m_Seeker = GetComponent<AiKSeek>();
		m_Arriver = GetComponent<AiKArrive>();

		if (m_Graph == null) {
			Debug.LogError("Graph missing from agent");
		}
		if (m_Target == null) {
			Debug.LogError("Target missing from agent");
		}
		if (m_Seeker == null) {
			Debug.LogError("Seek AiBehaviour missing from agent");
		}
		if (m_Arriver == null) {
			Debug.LogError("Arrive AiBehaviour missing from agent");
		}
	}
	
	// Update is called once per frame
	void Update () {
		m_Seeker.enabled = false;
		m_Arriver.enabled = false;
		m_MyTarget = null;
		if (m_Path.Count > 1) {
			m_MyTarget = m_Path[0];
			m_Seeker.enabled = true;
			m_Seeker.m_Target = m_MyTarget;
		}
		else if (m_Path.Count == 1) {
			m_Arriver.enabled = true;
			m_Arriver.m_Target = m_MyTarget = m_Path[0];
			m_Arrived = true;
		}

		if (m_Path.Count == 0 && m_Arrived) {
			m_Next.enabled = true;
			this.enabled = false;
		}




		if (m_Path.Count > 1) {
			int i;
			Vector3 src;
			Vector3 dst;
			Vector3 dir;
			for (i = 0; i < m_Path.Count-1; i++) {
				src = m_Path[i].transform.position;
				dst = m_Path[i+1].transform.position;
				dir = dst - src;
				Debug.DrawRay(src, dir, Color.red);
			}
		}
	
	}

	void LateUpdate () {
		if (m_Path.Count > 0) {
			float distance = Vector3.Distance(m_Path[0].transform.position, this.gameObject.transform.position);
			if (distance <= m_TargetDistance) {
				m_Path.RemoveAt(0);
			}
		}


		if (m_Path.Count > 1) {
			int i;
			Vector3 src;
			Vector3 dst;
			Vector3 dir;
			for (i = 0; i < m_Path.Count-1; i++) {
				src = m_Path[i].transform.position;
				dst = m_Path[i+1].transform.position;
				dir = dst - src;
				Debug.DrawRay(src, dir, Color.red);
			}
		}
	}

	void OnGizmos () {
		if (m_Path.Count > 1) {
			int i;
			Vector3 src;
			Vector3 dst;
			Vector3 dir;
			for (i = 0; i < m_Path.Count; i++) {
				src = m_Path[i].transform.position;
				dst = m_Path[i+1].transform.position;
				dir = dst - src;
				//Gizmos.color = Color.red;
				//Gizmos.DrawRay(src, dir);
			}
		}
	}

	public List<GameObject> GetPath () {
		GameObject src = m_Graph.GetClosestVertexTo(this.gameObject);
		GameObject dst = m_Graph.GetClosestVertexTo(m_Target);
		List<GameObject> path = m_Graph.Astar(src, dst);
		return path;
	}
}
