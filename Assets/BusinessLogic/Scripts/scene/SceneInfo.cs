using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneInfo : ScriptableObject
{
	public int  number;
	public string sceneName;

	public int currentPoint = 0;

	public Vector2[] enterPoints;
}
