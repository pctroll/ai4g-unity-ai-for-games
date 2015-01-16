using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AiNode : IComparable<AiNode>
{

    public int m_Cost;

    public GameObject m_GameObject;


    public AiNode(GameObject gameObject, int cost = 1)
    {
        m_GameObject = gameObject;
        m_Cost = cost;
    }

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

    public bool Equals(AiNode other)
    {
        return (other.m_GameObject.GetInstanceID() == this.m_GameObject.GetInstanceID());
    }

    public override bool Equals(object obj)
    {
        AiNode other = (AiNode)obj;
        return (other.m_GameObject.GetInstanceID() == this.m_GameObject.GetInstanceID());
    }

    public override int GetHashCode()
    {
        return this.m_GameObject.GetHashCode();
    }

    public override string ToString()
    {
        return "AiNode.cost: "+m_Cost.ToString();
    }

}
