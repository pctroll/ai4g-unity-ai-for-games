using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace Editor.Menu
{

    public class AiMenuGraph : MonoBehaviour
    {
        private static GameObject m_ParentGraph = null;

        [MenuItem("MenuGraph/Graph/Create Graph")]
        static void CreateGraph()
        {
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Graph.prefab", typeof(GameObject));
            m_ParentGraph = Instantiate(prefab, Vector3.zero, new Quaternion()) as GameObject;
            m_ParentGraph.name = "Graph";
        }

        [MenuItem("MenuGraph/Graph/Create Graph", true)]
        static bool ValidateCreateGraph()
        {
            return !IsGraph();
        }

        [MenuItem("MenuGraph/Graph/Destroy Graph")]
        static void DestroyGraph()
        {
            GameObject.DestroyImmediate(m_ParentGraph);
            m_ParentGraph = null;
        }

        [MenuItem("MenuGraph/Graph/Destroy Graph", true)]
        static bool ValidateDestroyGraph()
        {
            return IsGraph();
        }

        [MenuItem("MenuGraph/Vertex/Add/With Collider")]
        static void AddVertexWithCollider()
        {
            AddVertex(true);
        }

        [MenuItem("MenuGraph/Vertex/Add/With Collider", true)]
        static bool ValidateAddVertexWithCollider()
        {
            return IsGraph();
        }

        [MenuItem("MenuGraph/Vertex/Add/No Collider")]
        static void AddVertexNoCollider ()
        {
            AddVertex(false);
        }


        [MenuItem("MenuGraph/Connect Graph")]

        [MenuItem("MenuGraph/Vertex/Add/No Collider", true)]
        static bool ValidateAddVertexNoCollider()
        {
            return IsGraph();
        }

        [MenuItem("MenuGraph/Vertex/Connect/All")]

        static void ConnectGraph()
        {
            GameObject[] vertices = GameObject.FindGameObjectsWithTag("AiVertex");
            AiVertex vertexA, vertexB;
            int i, j;
            // Reset ALL the successors o/
            for (i = 0; i < vertices.Length; i++)
            {
                vertexA = vertices[i].GetComponent<AiVertex>();
                vertexA.m_Successors = new List<AiEdge>();
            }
            // Connect ALL the vertices o/

            for (i = 0; i < vertices.Length - 1; i++)
            {
                for (j = i + 1; j < vertices.Length; j++)
                {
                    vertexA = vertices[i].GetComponent<AiVertex>();
                    vertexB = vertices[j].GetComponent<AiVertex>();
                    vertexA.m_Successors.Add(new AiEdge(vertices[j]));
                    vertexB.m_Successors.Add(new AiEdge(vertices[i]));

                }
            }
        }

        [MenuItem("MenuGraph/Connect Graph", true)]
        static bool ValidateConnectGraph ()
        {
            GameObject[] vertices = GameObject.FindGameObjectsWithTag("AiVertex");
            if (vertices.Length > 1)
                return true;
            return false;
        }        



        /// <summary>
        /// 
        /// </summary>
        /// <param name="collider"></param>
        static void AddVertex(bool collider)
        {
            if (!IsGraph())
                CreateGraph();
            Object prefab;
            if (collider)
            {
                prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/VertexWC.prefab", typeof(GameObject));
            }
            else
            {
                prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/VertexNC.prefab", typeof(GameObject));
            }
            GameObject newVertex = Instantiate(prefab, Vector3.zero, new Quaternion()) as GameObject;
            newVertex.name = newVertex.name.Replace("(Clone)", "");
            newVertex.transform.parent = m_ParentGraph.transform;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static bool IsGraph()
        {
            if (m_ParentGraph != null)
                    return true;
            return false;
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}