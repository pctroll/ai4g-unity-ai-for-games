using UnityEngine;
using System.Collections;
/// <summary>
/// Ai steering class.
/// </summary>
public class AiSteering {
/// <summary>
/// The angular velocity.
/// </summary>
	public float Angular;
/// <summary>
/// The linear velocity.
/// </summary>
	public Vector3 Linear;
/// <summary>
/// The orientation.
/// </summary>
	public Quaternion Orientation;
/// <summary>
/// Initializes a new instance of the <see cref="AiSteering"/> class.
/// </summary>
	public AiSteering () {
		Angular = 0.0f;
		Linear = Vector3.zero;
		Orientation = new Quaternion();
	}
}
