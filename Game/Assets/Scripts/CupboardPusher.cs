using UnityEngine;
using System.Collections;

public class CupboardPusher : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown(0)){
			
			Ray mousePoint = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit info;
			if (Physics.Raycast(mousePoint, out info, float.PositiveInfinity, 1 << LayerMask.NameToLayer("GroundPlane"))){
				Vector3 intersectionPoint = info.point;
				rigidbody.AddForce(-intersectionPoint * 100);
			}
		}
	
	}
	
}
