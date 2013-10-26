﻿using UnityEngine;
using System.Collections;

public class DragonManager : MonoBehaviour
{
	GameObject dragged;
	Vector3 lastPosition;
	Vector3 currentPosition;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
		{
			Ray mousePoint = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit info;

			if (Physics.Raycast(mousePoint, out info, float.PositiveInfinity, 1 << LayerMask.NameToLayer("GroundPlane")))
			{
				Vector3 intersectionPoint = info.point;
				currentPosition = info.point;
				float tempDistance = float.PositiveInfinity;

				if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
				{
					if (Input.GetMouseButtonDown(0))
					{
						foreach (GameObject draggable in GameObject.FindGameObjectsWithTag("Draggable"))
						{
							float distance = Vector3.Distance(intersectionPoint, draggable.transform.position);
							if (distance < tempDistance)
							{
								tempDistance = distance;
								dragged = draggable;
							}
						}
					}
					lastPosition = currentPosition;
				}

				Vector3 move = (currentPosition - lastPosition);
				if (dragged != null)
				{
					CollisionDetection cd = dragged.GetComponent<CollisionDetection>();
					if (cd == null || cd.MayBeMoved)
						dragged.transform.position += move;
				}
				else if (Input.GetMouseButton(1))
				{
					move.x = 0;

					Camera.main.transform.position -= move;
					currentPosition -= move;
				}

				lastPosition = currentPosition;
			}
		}
	}
}
