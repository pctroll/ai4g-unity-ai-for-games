using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(AiWander))]
public class AiWanderEditor : Editor {

	public override void OnInspectorGUI ()
	{
		AiWander script = (AiWander)target;
		base.OnInspectorGUI ();
	}
}
