using UnityEngine;
using System.Collections;

public abstract class AiBehavior : MonoBehaviour {

	public float Weight;
	public bool DrawLines;

	protected AiAgent m_Agent;
	protected AiSteering m_Steering;

	public void Start () {
		if (Weight <= 0.0f)
			Weight = 1.0f;
		m_Agent = gameObject.GetComponent<AiAgent>();
		if (m_Agent == null)
			Debug.LogError("No AiAgent component found");
	}

	public void SetNewOrientation (Transform transform, Vector3 velocity) {
		if (velocity.sqrMagnitude > 0.0f) {
			transform.LookAt(transform.position + velocity);
		}
	}

	public void DrawCircle (Vector3 position, Color color, float radius = 1.0f) {
		Vector3 current, next, direction;
		float x, z;
		float increment = 0.3f;
		for (float i = 0; i < 2*Mathf.PI; i += increment) {
			x = radius * Mathf.Cos(i);
			z = radius * Mathf.Sin(i);
			current = new Vector3(x, position.y, z) + position;
			x = radius * Mathf.Cos(i + increment);
			z = radius * Mathf.Sin(i + increment);
			next = new Vector3(x, position.y, z) + position;
			direction = next - current;
			Debug.DrawRay(current, direction, color);
		}
	}
}
