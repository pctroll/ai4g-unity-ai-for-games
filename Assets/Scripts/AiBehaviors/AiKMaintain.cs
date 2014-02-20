using UnityEngine;
using System.Collections;

public class AiKMaintain : AiBehavior {

	public AiKMaintain (AiSteering oldSteering) {
		m_Steering = oldSteering;
	}

	public override AiSteering GetSteering ()
	{
		return m_Steering;
	}
}
