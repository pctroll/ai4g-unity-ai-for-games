using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiMeshCreator : MonoBehaviour {

	public GameObject m_VertexMesh;

	Vector3 m_VertexPos;
	int vid;

	List<GameObject> m_Vertices;
	List<List<int> > m_Edges;

	// Use this for initialization
	void Start () {
		m_Vertices = new List<GameObject>();
		m_Edges = new List<List<int> >();
		vid = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			Vector3 src, dst;
			GameObject newVertex;
			m_VertexPos = GetScreenToWorldPos(Input.mousePosition);
			newVertex = Instantiate(m_VertexMesh, m_VertexPos, new Quaternion()) as GameObject;
			vid++;
			m_Edges.Add(new List<int>());
			/*if (m_Vertices.Count > 1) {
				List<int> closest = GetClosestTwo(m_VertexPos);
				for (int i = 0; i < closest.Count; i++) {
					m_Edges[vid].Add(closest[i]);
					m_Edges[closest[i]].Add(vid);
				}
			} else if (m_Vertices.Count > 0) {
				m_Edges[0].Add(1);
				m_Edges[vid].Add(0);
			}*/
			m_Vertices.Add(newVertex);
		}
	}

	void OnDrawGizmos () {
		Color colorAux = Gizmos.color;
		int i, j;
		Vector3 src, dst, direction;
		if (m_Vertices != null) {
			src = GetScreenToWorldPos(Input.mousePosition);
			if (m_Vertices.Count > 1) {
				List<int> closest = GetClosestTwo(src);
				for (i = 0; i < closest.Count; i++) {
					dst = m_Vertices[closest[i]].transform.position;
					direction = src - dst;
					Gizmos.color = Color.red;
					Gizmos.DrawRay(dst, direction);
				}
			} else if (m_Vertices.Count > 0) {
				dst = m_Vertices[0].transform.position;
				direction = src - dst;
				Gizmos.color = Color.yellow;
				Gizmos.DrawRay(dst, direction);
			}

			for (i = 0; i < m_Edges.Count; i++) {
				src = m_Vertices[i].transform.position;
				for (j = 0; j < m_Edges[i].Count; j++) {
					Debug.Log("vertex: " + i);
					Debug.Log("neighbor: " + m_Edges[i][j]);
					/*dst = m_Vertices[m_Edges[i][j]].transform.position;
					direction = src - dst;
					Gizmos.color = Color.red;
					Gizmos.DrawRay(dst, direction);*/
				}
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
