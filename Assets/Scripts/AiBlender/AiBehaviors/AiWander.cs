using UnityEngine;
using System.Collections;

public class AiWander : AiFace {

	public float m_MaxAccel;
	public float m_WanderOffset;
	public float m_WanderRadius;
	public float m_WanderRate;
	public float m_WanderOrientation;


	// Use this for initialization
	void Start () {
		base.Init();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Agent != null) {
			m_Agent.SetSteering(GetSteering());
		}
	}

	public override AiSteering GetSteering ()
	{

		return base.GetSteering ();
	}
}
