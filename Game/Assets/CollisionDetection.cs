using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour
{
	
	Vector3 pos;
	Vector3 lastPosition;
	bool collided = false;
	
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!collided){
		if (Input.GetKey("space")){
			transform.Translate(Vector3.left *Time.deltaTime);
			lastPosition = pos;
			pos = transform.position;
		}
		}
		
	}

	void OnCollisionEnter(){
		collided = true;
		Debug.Log("here");
		transform.position = lastPosition;
		pos = lastPosition;
	}
	
	void OnCollisionExit(){
		collided = false;
	}
	
	
	
}