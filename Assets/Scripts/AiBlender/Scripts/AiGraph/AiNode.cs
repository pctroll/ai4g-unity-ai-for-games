using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Class for handling a general graph node
/// for A* path finding.
/// </summary>
public class AiNode : IComparable<AiNode>
{
    /// <summary>
    /// Cost of the node (edge)
    /// </summary>
    public int m_Cost;
    /// <summary>
    /// Vertex
    /// </summary>
    public GameObject m_GameObject;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="cost"></param>
    public AiNode(GameObject gameObject, int cost = 1)
    {
        m_GameObject = gameObject;
        m_Cost = cost;
    }

    /// <summary>
    /// Function derived from the interface to compare two nodes
    /// if equal, greater than or less than
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(AiNode other)
    {
        int result = m_Cost - other.m_Cost;
        int idA = m_GameObject.GetInstanceID();
        int idB = other.m_GameObject.GetInstanceID();
        if (idA == idB)
            return 0;
        else if (result < 0)
            return -1;
        else
            return 1;
    }

    /// <summary>
    /// Function to compare if two Nodes are equal
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(AiNode other)
    {
        return (other.m_GameObject.GetInstanceID() == this.m_GameObject.GetInstanceID());
    }

    /// <summary>
    /// Function to compare if two nodes are equal
    /// using object boxing
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        AiNode other = (AiNode)obj;
        return (other.m_GameObject.GetInstanceID() == this.m_GameObject.GetInstanceID());
    }
    /// <summary>
    /// Overriden function for IComparable
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return this.m_GameObject.GetHashCode();
    }
    /// <summary>
    /// Overriden function for showing as a string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return "AiNode.cost: "+m_Cost.ToString();
    }

}
