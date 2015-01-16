using UnityEngine;
using System.Collections;
/// <summary>
/// Class for representing a graph's edge
/// </summary>
[System.Serializable]
public class AiEdge {
    /// <summary>
    /// Neighbor
    /// </summary>
    public GameObject m_Vertex;
    /// <summary>
    /// Edge cost
    /// </summary>
    public int m_Cost = 1;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="vertex"></param>
    /// <param name="cost"></param>
    public AiEdge(GameObject vertex = null, int cost = 1)
    {
        m_Vertex = vertex;
        m_Cost = cost;
    }
}

