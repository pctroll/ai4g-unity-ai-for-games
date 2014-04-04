using UnityEngine;
using System.Collections;

public class AiEditor {
	
	static float m_DegreesMin	= 0.0f;
	static float m_DegreesMax	= 359.9f;
	static float m_WeightMin	= 0.0f;
	static float m_WeightMax 	= 100.0f;
	static float m_AccelMin		= 0.0f;
	static float m_AccelMax		= Mathf.Infinity;

	public static float MIN_WEIGHT
	{
		get { return m_WeightMin; }
	}

	public static float MAX_WEIGHT
	{
		get { return m_WeightMax; }
	}
	
	public static float MIN_DEGREES
	{
		get { return m_DegreesMin; }
	}

	public static float MAX_DEGREES
	{
		get { return m_DegreesMax; }
	}

	public static float MIN_ACCEL
	{
		get { return m_AccelMin; }
	}

	public static float MAX_ACCEL
	{
		get { return m_AccelMax; }
	}
}