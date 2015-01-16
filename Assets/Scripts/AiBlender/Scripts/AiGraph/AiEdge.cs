using UnityEngine;
using System.Collections;

[System.Serializable]
public class AiEdge {

    public GameObject m_Vertex;
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

