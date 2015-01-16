using UnityEngine;
using System;
using System.Collections;
/// <summary>
/// Ai behaviour class.
/// </summary>
[Serializable]
public class AiBehaviour : MonoBehaviour {
	/// <summary>
	/// The behaviours weight.
	/// </summary>
	[SerializeField]
	public float m_Weight = 1;

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
	/// The steering.
	/// </summary>
	protected AiSteering m_Steering;

	/// <summary>
	/// Behaviour's initialization code
	/// </summary>
	public virtual void Init () {
		m_Steering = new AiSteering();
		m_Character = this.gameObject;
		m_Agent = gameObject.GetComponent<AiAgent>();
		if (m_Agent == null)
			Debug.LogError("No AiAgent component found");
	}

    /// <summary>
    /// General update mode in order to get the
    /// behaviours component's steering and send it to 
    /// the agent component
    /// </summary>
	public virtual void Update () {
		if (m_Agent!= null) {
			m_Agent.SetSteering(GetSteering(), m_Weight);
		}
	}

    /// <summary>
    /// Returns the steering values to update the agent
    /// </summary>
    /// <returns>the behaviour's steering</returns>
	public virtual AiSteering GetSteering () {
		return m_Steering;
	}

    /// <summary>
    /// [deprecated]Sets the agent's orientation according to its
    /// velocity. Used along with kinematic behaviours.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="velocity"></param>
	public void SetNewOrientation (Transform transform, Vector3 velocity) {
		if (velocity.sqrMagnitude > 0.0f) {
			transform.LookAt(transform.position + velocity);
		}
	}

    /// <summary>
    /// Returns the agent's orientation according to its
    /// velocity. Used along with kinematic behaviours.
    /// </summary>
    /// <param name="currentOrientation"></param>
    /// <param name="velocity"></param>
    /// <returns></returns>
	public float GetNewOrientation (float currentOrientation, Vector3 velocity) {
		float orientation = currentOrientation;
		if (velocity.sqrMagnitude > 0.0f) {
			orientation = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
		}
		return orientation;
	}

    /// <summary>
    /// Convert a given rotation from a 0,360 interval
    /// to -180,180 interval.
    /// 
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns>Rotation from -180 to 180</returns>
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

    /// <summary>
    /// Transforms a given orientation (degrees) to a direction vector
    /// </summary>
    /// <param name="orientation"></param>
    /// <returns></returns>
	public Vector3 GetOriAsVec (float orientation) {
		Vector3 vector  = Vector3.zero;
		vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
		vector.z = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1.0f;
		return vector.normalized;
	}

    /// <summary>
    /// Draws a circle around a position. Used for debugging.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="color"></param>
    /// <param name="radius"></param>
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
