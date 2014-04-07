#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AiVertex : MonoBehaviour {

    /// <summary>
    /// The successors.
    /// </summary>
    public List<AiEdge> m_Successors;

    void Awake ()
    {
        //m_Successors = new List<AiEdge>();
    }

    // Use this for initialization
    void Start ()
    {

    }
    
    // Update is called once per frame
    void Update ()
    {
    }
#if UNITY_EDITOR
    void OnDrawGizmos ()
    {
        // Draw vertices
        Gizmos.color = Color.yellow;
        Handles.color = Color.yellow;
        Vector3 direction;
        Vector3 capPosition;
        Quaternion orientation;
        foreach (AiEdge e in m_Successors)
        {
            if (e.m_Vertex != null && e.m_Cost > 0)
            {
                Gizmos.DrawLine (transform.position, e.m_Vertex.transform.position);
                direction = e.m_Vertex.transform.position - transform.position;
                orientation = Quaternion.LookRotation(direction);

                capPosition = transform.position + direction*0.75f;
                Handles.ConeCap(0, capPosition, orientation, 0.5f);
            }
        }
    }
#endif

    protected void DefineSuccessors () {

    }
}
