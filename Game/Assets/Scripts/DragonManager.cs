using UnityEngine;
using System.Collections;

public class DragonManager : MonoBehaviour
{
	GameObject dragged;
	Vector3 lastPosition;
	Vector3 currentPosition;
	Vector3 initialPosition;
	float initialIdleTime;
	Mode mode;

	enum Mode
	{
		None,
		PreScroll,
		Scroll,
		Drag,
	}

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			RaycastHit info;
			Ray mousePoint = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(mousePoint, out info, float.PositiveInfinity, 1 << LayerMask.NameToLayer("GroundPlane")))
				currentPosition = info.point;

			if (mode == Mode.None)
			{
				mode = Mode.PreScroll;
				initialPosition = lastPosition = currentPosition;
				initialIdleTime = 0;
			}
			else if (mode == Mode.PreScroll)
			{
				if ((currentPosition - initialPosition).magnitude < 0.4f)
					initialIdleTime += Time.deltaTime;
				else
					mode = Mode.Scroll;

				if (initialIdleTime > 0.3f)
				{
					dragged = selectObject(currentPosition);
					if (dragged != null)
					{
						dragged.GetComponent<Draggable>().IsBeingDragged = true;
						mode = Mode.Drag;
#if UNITY_ANDROID
						Handheld.Vibrate();
#endif
					}
				}
			}

			Vector3 move = (currentPosition - lastPosition);
			if (mode == Mode.Drag)
			{
				Draggable cd = dragged.GetComponent<Draggable>();
				if (cd == null || cd.MayBeMoved)
					dragged.transform.position += move;
			}
			else if (mode == Mode.Scroll || mode == Mode.PreScroll)
			{
				move.x = 0;

				Camera.main.transform.position -= move;
				currentPosition -= move;
			}

			lastPosition = currentPosition;
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (dragged != null)
				dragged.GetComponent<Draggable>().IsBeingDragged = false;
			dragged = null;
			mode = Mode.None;
		}
	}

	private GameObject selectObject(Vector3 currentPosition)
	{
		float tempDistance = float.PositiveInfinity;
		GameObject toDrag = null;

		foreach (GameObject draggable in GameObject.FindGameObjectsWithTag("Draggable"))
		{
			float distance = Vector3.Distance(currentPosition, draggable.transform.position);
			if (distance < tempDistance)
			{
				tempDistance = distance;
				toDrag = draggable;
			}
		}

		return toDrag;
	}
}
