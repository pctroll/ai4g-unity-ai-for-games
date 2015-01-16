using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Class for representing a graph
/// </summary>
public class AiGraph : MonoBehaviour {

    private GameObject[] m_Vertices;
    private ArrayList m_Successors;
    private int m_Size;
    // Use this for initialization
    void Start ()
    {
        m_Vertices = GameObject.FindGameObjectsWithTag("AiVertex");
    }

    /// <summary>
    /// Gets the closer vertex to.
    /// </summary>
    /// <returns>
    /// The closer vertex to the given target.
    /// </returns>
    /// <param name='target'>
    /// Target.
    /// </param>
    public GameObject GetClosestVertexTo(GameObject target)
    {
        float oldDistance = Mathf.Infinity, newDistance = Mathf.Infinity;
		GameObject vertex = null;
        foreach (GameObject o in m_Vertices)
        {
            newDistance = Vector3.Distance(o.transform.position, target.transform.position);
            if (newDistance < oldDistance && PathClear(target, o))
            {
                vertex = o;
                oldDistance = newDistance;
            }
        }
        return vertex;
    }

    public bool PathClear(GameObject a, GameObject b)
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("AiWall");

        for (int i = 0; i < obstacles.Length; i++)
        {
            GameObject obst = obstacles[i];
            Vector3 direction = b.transform.position - a.transform.position;
            Ray r = new Ray(a.transform.position, direction);
            RaycastHit hit;
            if (obst.collider.Raycast(r, out hit, direction.magnitude))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Returns the shortest path from source to destination
    /// using A* algorithm.
    /// </summary>
    /// <param name='src' type='GameObject'>
    /// Source.
    /// </param>
    /// <param name='dst' type='GameObject'>
    /// Destination
    /// </param>
    public List<GameObject> Astar(GameObject src, GameObject dst)
    {
        List<AiEdge> successors;
        AiVertex auxVertex;
        int cost = 0;
        bool isInFrontier = false, isInExplored = false;
        AiNode node = new AiNode(src, 1);
        AiNode dstNode = new AiNode(dst, 1);   
        AiNode child;
        GPWiki.BinaryHeap<AiNode> frontier = new GPWiki.BinaryHeap<AiNode>();
        List<GameObject> explored = new List<GameObject>();
        frontier.Add(node);
        while (true)
        {
            if (frontier.Count == 0)
                return new List<GameObject>();
            node = frontier.Remove();
            explored.Add(node.m_GameObject);
            if (node.Equals(dstNode))
                return explored;
            auxVertex = node.m_GameObject.GetComponent<AiVertex>();
            successors = auxVertex.m_Successors;
            foreach (AiEdge e in successors)
            {
                cost = e.m_Cost;
                cost += (int)Vector3.Distance(e.m_Vertex.gameObject.transform.position, dst.transform.position);
                child = new AiNode(e.m_Vertex.gameObject, cost);
                isInFrontier = frontier.Contains(child);
                isInExplored = explored.Contains(child.m_GameObject);
                if (!isInExplored && !isInFrontier)
                    frontier.Add(child);
                else if (isInFrontier)
                {
                    foreach (AiNode n in frontier)
                    {
                        if (n.Equals(child) && (n.m_Cost > child.m_Cost))
                        {
                            frontier.Remove(child);
                            frontier.Add(child);
                        }
                    }
                }
            }
        }
    }
}