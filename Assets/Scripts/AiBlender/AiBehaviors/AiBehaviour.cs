using UnityEngine;
using System.Collections;
/// <summary>
/// Ai behaviour class.
/// </summary>
public abstract class AiBehaviour : MonoBehaviour {
	/// <summary>
	/// The behaviours weight.
	/// </summary>
	public float m_Weight;

	/// <summary>
	/// Whether to draw debug lines.
	/// </summary>
	public bool m_DrawLines;

	/// <summary>
	/// The agent target's game object.
	/// </summary>
	public GameObject m_Target;

	/// <summary>
	/// The agent's game object.
	/// </summary>
	protected GameObject m_Character;

	/// <summary>
	/// The m_ agent.
	/// </summary>
	protected AiAgent m_Agent;

	/// <summary>
	/// The m_ steering.
	/// </summary>
	protected AiSteering m_Steering;

	public virtual void Init () {
		m_Steering = new AiSteering();
		if (m_Weight <= 0.0f)
			m_Weight = 1.0f;
		m_Character = this.gameObject;
		m_Agent = gameObject.GetComponent<AiAgent>();
		if (m_Agent == null)
			Debug.LogError("No AiAgent component found");
	}

	public virtual AiSteering GetSteering () {
		return m_Steering;
	}

	public void SetNewOrientation (Transform transform, Vector3 velocity) {
		if (velocity.sqrMagnitude > 0.0f) {
			transform.LookAt(transform.position + velocity);
		}
	}

	public float MapToRange (float rotation) {
		rotation %= 360.0f;
		if (Mathf.Abs(rotation) > 180.0f) {
			if (rotation < 0.0f)
				rotation += 360.0f;
			else
				rotation -= 360.0f;
		}
		return rotation;
	}

	public Vector3 GetOriAsVec (float orientation) {
		Vector3 vector  = Vector3.zero;
		vector.x = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1.0f;
		vector.z = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
		return vector.normalized;
	}

	public void DrawCircle (Vector3 position, Color color, float radius = 1.0f) {
		position.y = 0.15f;
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
