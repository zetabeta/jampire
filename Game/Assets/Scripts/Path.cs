using UnityEngine;
using System.Collections;

public class PathState
{
	public int index;
	public Transform currentNode, nextNode;
	public float invDistance;
	public float progress;
	public Vector3 position;
	public float rotation;
	public float idleTime;
}

public class Path : MonoBehaviour
{
	public PathState Begin()
	{
		PathState state = new PathState();
		state.currentNode = transform.GetChild(0);
		state.currentNode.GetComponent<Waypoint>().PlayAction(state);

		if (transform.childCount > 1)
		{
			state.nextNode = transform.GetChild(1);
			state.invDistance = 1.0f / (state.nextNode.position - state.currentNode.position).magnitude;

			state.rotation = Mathf.Asin((state.nextNode.position - state.currentNode.position).normalized.z) * Mathf.Rad2Deg + 90.0f;
			if ((state.nextNode.position - state.currentNode.position).x >= 0)
				state.rotation = 360.0f - state.rotation;
		}
		state.position = getPositionForState(state);
		return state;
	}

	private Vector3 getPositionForState(PathState state)
	{
		if (state.nextNode != null)
			return Vector3.Lerp(state.currentNode.position, state.nextNode.position, state.progress);
		else
			return state.currentNode.position;
	}

	public void Progress(PathState state, float by)
	{
		if (state.nextNode == null)
			return;

		if (state.idleTime > 0)
		{
			state.idleTime -= by;
			if (state.idleTime > 0)
				return;
		}

		state.progress += by * state.invDistance;
		if (state.progress >= 1.0f)
		{
			state.progress = 0;
			state.currentNode = state.nextNode;
			state.currentNode.GetComponent<Waypoint>().PlayAction(state);
			++state.index;

			if (transform.childCount > state.index + 1)
			{
				state.nextNode = transform.GetChild(state.index + 1);
				state.invDistance = 1.0f / (state.nextNode.position - state.currentNode.position).magnitude;

				state.rotation = Mathf.Asin((state.nextNode.position - state.currentNode.position).normalized.z) * Mathf.Rad2Deg + 90.0f;
				if ((state.nextNode.position - state.currentNode.position).x >= 0)
					state.rotation = 360.0f - state.rotation;
			}
			else
				state.nextNode = null;
		}

		state.position = getPositionForState(state);
	}
}
