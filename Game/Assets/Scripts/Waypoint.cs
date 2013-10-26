using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WaypointAction
{
	None,
	EndLevel,
	PlayAnimation,
	PlaySound,
	Wait,
}

public class Waypoint : MonoBehaviour
{
	public WaypointAction action;
	public GameObject actionTarget;
	public string animationName;
	public float waitTime;

	public void PlayAction(PathState state)
	{
		switch (action)
		{
			case WaypointAction.EndLevel:
				StartCoroutine(loadNextLevel(waitTime));
				break;

			case WaypointAction.PlayAnimation:
				actionTarget.animation.Play(animationName);
				break;

			case WaypointAction.PlaySound:
				actionTarget.audio.Play();
				break;

			case WaypointAction.Wait:
				state.idleTime = waitTime;
				break;
		}
	}

	private IEnumerator loadNextLevel(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
