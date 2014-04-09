using UnityEngine;
using System.Collections;

public class AiObstacleAvoidance : AiSeek {

	public float m_AvoidDistance;

	public float m_LookAhead;

	// Use this for initialization
	void Start () {
		Init();
	}

	public override void Init ()
	{
		base.Init ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public override AiSteering GetSteering () {
		if (m_Agent != null) {

		}
		return base.GetSteering ();
	}
}
