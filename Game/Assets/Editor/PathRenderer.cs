using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Path))]
public sealed class PathRenderer : Editor
{
	void OnSceneGUI()
	{
		Path path = target as Path;

		Transform lastChild = null;

		for (int i = 0; i < path.transform.childCount; ++i)
		{
			Transform child = path.transform.GetChild(i);

			if (lastChild != null)
			{
				Handles.DrawLine(lastChild.position, child.position);
			}

			lastChild = child;
		}
	}
}
