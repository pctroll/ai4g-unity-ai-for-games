using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiMeshCreator : MonoBehaviour {

	public GameObject m_VertexMesh;

	Vector3 m_VertexPos;

	List<GameObject> m_Vertices;

	// Use this for initialization
	void Start () {
		m_Vertices = new List<GameObject>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			GameObject newVertex;
			m_VertexPos = GetScreenToWorldPos(Input.mousePosition);
			newVertex = Instantiate(m_VertexMesh, m_VertexPos, new Quaternion()) as GameObject;
			m_Vertices.Add(newVertex);
		}
	}

	void OnDrawGizmos () {
		Color colorAux = Gizmos.color;
		if (m_Vertices != null) {
			Vector3 src = GetScreenToWorldPos(Input.mousePosition);
			if (m_Vertices.Count > 1) {
				List<int> closest = GetClosestTwo(src);
				for (int i = 0; i < closest.Count; i++) {
					Vector3 dst = m_Vertices[closest[i]].transform.position;
					Vector3 direction = src - dst;
					Gizmos.color = Color.red;
					Gizmos.DrawRay(dst, direction);
				}
			} else if (m_Vertices.Count > 0) {
				Vector3 dst = m_Vertices[0].transform.position;
				Vector3 direction = src - dst;
				Gizmos.color = Color.red;
				Gizmos.DrawRay(dst, direction);
			}
		}

		Gizmos.color = colorAux;
	}

	Vector3 GetScreenToWorldPos (Vector3 mousePosition) {
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		Plane plane = new Plane(Vector2.up, Vector3.zero);
		float distance = 0;
		if (plane.Raycast(ray, out distance)) {
			return ray.GetPoint(distance);
		}
		return Vector3.zero;
	}

	List<int> GetClosestTwo (Vector3 position) {
		float distanceA = Mathf.Infinity;
		float distanceB = Mathf.Infinity;
		int a = -1;
		int b = -1;
		for (int i = 0; i < m_Vertices.Count; i++) {
			Vector3 vertPos = m_Vertices[i].transform.position;
			float dist = Vector3.Distance(position, vertPos);
			if (dist < distanceA) {
				distanceB = distanceA;
				b = a;
				distanceA = dist;
				a = i;
			} else if (dist < distanceB) {
				distanceB = dist;
				b = i;
			}
		}
		List<int> closest = new List<int>();
		closest.Add(a);
		closest.Add(b);
		return closest;
	}
}
