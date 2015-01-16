using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiMeshCreator : MonoBehaviour {

	public GameObject m_VertexMesh;
	public GameObject m_Vertex;

	Vector3 m_VertexPos;
	int vid;

	List<GameObject> m_Vertices;

	List<GameObject> m_Centroids;

	List< List<int> > m_Edges = new List< List<int> >();

	// Use this for initialization
	void Start () {
		m_Vertices = new List<GameObject>();
		m_Edges = new List<List<int> >();
		m_Centroids = new List<GameObject>();
		vid = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			Vector3 src, dst;
			Vector3 a, b, c;
			int id;
			GameObject newVertex;
			m_VertexPos = GetScreenToWorldPos(Input.mousePosition);
			newVertex = Instantiate(m_VertexMesh, m_VertexPos, new Quaternion()) as GameObject;

			m_Edges.Add(new List<int>());
			if (m_Vertices.Count > 1) {
				List<int> closest = GetClosestTwo(m_VertexPos);
				for (int i = 0; i < closest.Count; i++) {
					id = closest[i];
					m_Edges[id].Add(vid);
					m_Edges[vid].Add(id);
				}
				m_Vertices.Add(newVertex);
				vid++;
				a = m_Vertices[closest[0]].transform.position;
				b = m_Vertices[closest[1]].transform.position;
				c = m_Vertices[vid-1].transform.position;
				AddCentroid(a, b, c);
			} else if (m_Vertices.Count == 1) {
				m_Vertices.Add(newVertex);
				vid++;
				m_Edges[0].Add(1);
				m_Edges[1].Add(0);
			} else {
				m_Vertices.Add(newVertex);
				vid++;
			}
		}
	}

	void AddCentroid (Vector3 a, Vector3 b, Vector3 c) {
		Vector3 pos = new Vector3();
		pos.x = (a.x + b.x + c.x)/3.0f;
		pos.y = (a.y + b.y + c.y)/3.0f;
		pos.z = (a.z + b.z + c.z)/3.0f;
		GameObject centroid = Instantiate(m_Vertex, pos, new Quaternion()) as GameObject;
		m_Centroids.Add(centroid);
	}

	void OnDrawGizmos () {
		Color colorAux = Gizmos.color;
		int i, j, id;
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
				Gizmos.color = Color.red;
				Gizmos.DrawRay(dst, direction);
			}

			// Draw Graph
			for (i = 0; i < m_Edges.Count; i++) {
				src = m_Vertices[i].transform.position;
				for (j = 0; j < m_Edges[i].Count; j++) {
					id = m_Edges[i][j];
					dst = m_Vertices[id].transform.position;
					direction = dst - src;
					Gizmos.color = Color.red;
					Gizmos.DrawRay(src, direction);
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
