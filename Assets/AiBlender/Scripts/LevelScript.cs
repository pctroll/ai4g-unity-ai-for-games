using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour 
{
	public int experience;
	
	public int Level
	{
		get{ return experience / 750; }}
}