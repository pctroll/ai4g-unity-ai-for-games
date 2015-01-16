using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(AiMesh))]
public class AiMeshEditor : Editor {
	
	void OnSceneGUI () {
		if(Event.current.type == EventType.MouseDown && Event.current.button == 0) {
			Debug.Log("Left-Mouse Down");
		}
	}
}
