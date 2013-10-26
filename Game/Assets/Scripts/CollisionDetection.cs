using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour
{
	Vector3 pos;
	Vector3 lastPosition;
	bool collided = false;

	public bool MayBeMoved { get { return !collided; } }

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		lastPosition = pos;
		pos = transform.position;
	}

	void OnCollisionEnter(Collision collision)
	{
		collided = true;
		transform.position = lastPosition;
		pos = lastPosition;
	}

	void OnCollisionStay() {
		
	}
	
	void OnCollisionExit(Collision collision)
	{
		collided = false;
	}
}