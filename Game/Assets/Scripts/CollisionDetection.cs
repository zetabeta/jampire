using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour
{
	Vector3 pos;
	Vector3 lastValidPosition;
	bool collided = false;

	public bool MayBeMoved { get { return !collided; } }

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if (!collided) {
			lastValidPosition = pos;
			pos = transform.position;
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		collided = true;
		rigidbody.MovePosition (lastValidPosition);
		pos = lastValidPosition;
	}

	void OnCollisionStay ()
	{
		rigidbody.MovePosition (lastValidPosition);
		collided = false;
	}
	
	void OnCollisionExit (Collision collision)
	{
		rigidbody.MovePosition (lastValidPosition);
		collided = false;
	}
}