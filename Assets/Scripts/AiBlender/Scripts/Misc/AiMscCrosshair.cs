using UnityEngine;
using System.Collections;
/// <summary>
/// Class for handling the on-screen crosshair
/// </summary>
public class AiMscCrosshair : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = GetScreenToWorldPos(Input.mousePosition);
		newPosition.y = gameObject.transform.position.y;
		gameObject.transform.position = newPosition;
	}

    /// <summary>
    /// Transform the mouse position on screen
    /// to the worl position
    /// </summary>
    /// <param name="mousePosition"></param>
    /// <returns></returns>
	Vector3 GetScreenToWorldPos (Vector3 mousePosition) {
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		Plane plane = new Plane(Vector2.up, Vector3.zero);
		float distance = 0;
		if (plane.Raycast(ray, out distance)) {
			return ray.GetPoint(distance);
		}
		return Vector3.zero;
	}
}
