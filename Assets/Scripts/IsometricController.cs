using UnityEngine;
using System.Collections;
/// <summary>
/// Controls.
/// </summary>
public class IsometricController: MonoBehaviour {
	/// <summary>
	/// The camera object.
	/// </summary>
	public Camera cam;
	/// <summary>
	/// The object to use as reference.
	/// </summary>
	public GameObject obj;
	/// <summary>
	/// The camera speed.
	/// </summary>
	public float camSpeed;
	/// <summary>
	/// The vehicle speed.
	/// </summary>
	public float vehicleSpeed;
	/// <summary>
	/// The zoom speed.
	/// </summary>
	public float zoomSpeed;
	/// <summary>
	/// The zoom in limit.
	/// </summary>
	public float zoomIn;
	/// <summary>
	/// The zoom out limit.
	/// </summary>
	public float zoomOut;
	/// <summary>
	/// The zoomed-state variable.
	/// </summary>
	private bool isZoomed = false;
	/// <summary>
	/// The vehicle's old position.
	/// </summary>
	Vector3 oldPos;
	/// <summary>
	/// The mouse's old position.
	/// </summary>
	Vector3 mouseOldPos;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			//obj.transform.position = GetScreenToWorldPos(Input.mousePosition);
			//StartCoroutine(CenterToPoint(obj.transform.position));
		}
		if (cam != null) {
			/*
			Vector3 mov = Vector3.zero;
			float velX = Input.GetAxis("Horizontal") * camSpeed * Time.deltaTime;
			float velY = Input.GetAxis("Vertical") * camSpeed * Time.deltaTime;
			mov.x = velX;
			mov.z = -velX;
			cam.transform.position += mov;
			mov.x = velY*2;
			mov.z = velY*2;
			cam.transform.position += mov;
			*/
			if (Input.GetKeyDown(KeyCode.Space)) {
				isZoomed = !isZoomed;
				StartCoroutine(Zoom(isZoomed));
			}
			if (Input.GetMouseButton(0) && Input.GetMouseButton(1)) {
				Vector3 translateVector = mouseOldPos - Input.mousePosition;
				translateVector.z = translateVector.y;
				translateVector.y = 0.0f;
				translateVector = Quaternion.AngleAxis(45, Vector3.up) * translateVector;
				translateVector.Normalize();
				Debug.Log(translateVector.normalized);
				cam.transform.position += translateVector * camSpeed * Time.deltaTime;
				mouseOldPos = Input.mousePosition;
			}
		}
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
			StartCoroutine(CenterToPoint(obj.transform.position));
		}
		if (obj != null) {
			float velX = Input.GetAxis("Horizontal");
			float velY = Input.GetAxis("Vertical");
			Vector3 vec = new Vector3(velX, 0.0f, velY);
			vec = Quaternion.AngleAxis(45, Vector3.up) * vec;
			oldPos = obj.transform.position;
			obj.transform.Translate(vec * vehicleSpeed * Time.deltaTime, Space.World);
			Vector3 vel = obj.transform.position - oldPos;
			if (vel.sqrMagnitude > 0.0f) {
				vel = vel + obj.transform.position;
				obj.transform.LookAt(vel);
			}
			//FollowPoint(obj.transform.position);
		}
	}
	/// <summary>
	/// Follows the point.
	/// </summary>
	/// <param name="point">Point.</param>
	void FollowPoint (Vector3 point) {
		Vector3 cameraRefPos;
		Vector3 moveVector;
		cameraRefPos = GetScreenToWorldPos(new Vector3(Screen.width/2, Screen.height/2, 0.0f));
		moveVector = (point - cameraRefPos);
		cam.transform.position += moveVector;
	}

	/// <summary>
	/// Centers the camera to a given point.
	/// </summary>
	/// <returns>The to point.</returns>
	/// <param name="point">Point.</param>
	IEnumerator CenterToPoint (Vector3 point) {
		Vector3 velocity;
		Vector3 cameraRefPos;
		float distance;
		Vector3 moveVector;
		do {
			yield return null;
			cameraRefPos = GetScreenToWorldPos(new Vector3(Screen.width/2, Screen.height/2, 0.0f));
			moveVector = (point - cameraRefPos);
			distance = moveVector.magnitude;
			velocity = moveVector.normalized;
			cam.transform.position += velocity * camSpeed * Time.deltaTime;
		} while (distance > 0.1f);
		cameraRefPos = GetScreenToWorldPos(new Vector3(Screen.width/2, Screen.height/2, 0.0f));
		moveVector = (point - cameraRefPos);
		cam.transform.position += moveVector;
	}

	/// <summary>
	/// Zooms in or zooms out the camera.
	/// </summary>
	/// <param name="isZoomed">If set to <c>true</c> is zoomed.</param>
	IEnumerator Zoom (bool isZoomed) {
		if (isZoomed) {
			do {
				cam.orthographicSize += Time.deltaTime * zoomSpeed;
				yield return null;
			} while (cam.orthographicSize < zoomOut);
			cam.orthographicSize = zoomOut;
		}
		else {
			do {
				cam.orthographicSize -= Time.deltaTime * zoomSpeed;
				yield return null;
			} while (cam.orthographicSize > zoomIn);
			cam.orthographicSize = zoomIn;
		}
	}
	/// <summary>
	/// Gets the world position given the screen coordinates.
	/// </summary>
	/// <returns>The screen to world position.</returns>
	/// <param name="mousePosition">Mouse position.</param>
	Vector3 GetScreenToWorldPos (Vector3 mousePosition) {
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		Plane plane = new Plane(Vector2.up, Vector2.zero);
		float distance = 0;
		if (plane.Raycast(ray, out distance)) {
			return ray.GetPoint(distance);
		}
		return Vector3.zero;
	}
}
