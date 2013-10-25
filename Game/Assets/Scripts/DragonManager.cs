using UnityEngine;
using System.Collections;

public class DragonManager : MonoBehaviour
{
	GameObject dragged;
	Vector3 lastPosition;
	Vector3 currentPosition;


	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Ray mousePoint = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit info;

			if (Physics.Raycast(mousePoint, out info, float.PositiveInfinity, 1 << LayerMask.NameToLayer("GroundPlane")))
			{
				Vector3 intersectionPoint = info.point;
				currentPosition = info.point;
				float tempDistance = float.PositiveInfinity;
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
						//Debug.Log (distance);
						lastPosition = currentPosition;
					}
					dragged.renderer.material.color = Color.red;
				}

				CollisionDetection cd = dragged.GetComponent<CollisionDetection>();

				if (cd == null || cd.MayBeMoved)
					dragged.transform.position += (currentPosition - lastPosition);

				lastPosition = currentPosition;
			}

		}

		if (Input.GetMouseButtonUp(0))
		{
			dragged.renderer.material.color = Color.gray;
		}
	}


}
