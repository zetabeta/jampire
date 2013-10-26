using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour
{
	Vector3 pos;
	Vector3 lastValidPosition;
	bool isColliding = false;
	bool isBeingDragged = false;
	bool isCastingShadows;

	public bool MayBeMoved { get { return !isColliding; } }
	public bool IsBeingDragged
	{
		get { return isBeingDragged; }
		set
		{
			isBeingDragged = value;
			foreach (Collider collider in GetComponentsInChildren<Collider>())
				collider.enabled = !isBeingDragged;
		}
	}

	// Use this for initialization
	void Start()
	{
		isCastingShadows = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (!isColliding)
		{
			lastValidPosition = pos;
			pos = transform.position;
		}

		if (isBeingDragged)
			setCastShadows((((int)(Time.time * 20)) % 2) == 0);
		else
			setCastShadows(true);
	}

	void setCastShadows(bool enabled)
	{
		if (isCastingShadows == enabled)
			return;

		foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
			renderer.castShadows = enabled;

		isCastingShadows = enabled;
	}

	void OnCollisionEnter(Collision collision)
	{
		isColliding = true;
		rigidbody.MovePosition(lastValidPosition);
		pos = lastValidPosition;
	}

	void OnCollisionStay()
	{
		rigidbody.MovePosition(lastValidPosition);
		isColliding = false;
	}

	void OnCollisionExit(Collision collision)
	{
		rigidbody.MovePosition(lastValidPosition);
		isColliding = false;
	}
}
