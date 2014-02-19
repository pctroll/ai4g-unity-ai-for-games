using UnityEngine;
using System.Collections;

public abstract class AiBehavior {

	protected AiSteering m_Steering;

	public virtual AiSteering GetSteering () {
		return new AiSteering();
	}

	public void SetNewOrientation (Transform transform, Vector3 velocity) {
		if (velocity.sqrMagnitude > 0.0f) {
			transform.LookAt(transform.position + velocity);
		}
	}
}
