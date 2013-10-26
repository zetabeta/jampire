using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShadowTester))]
public class Vampire : MonoBehaviour
{
	public int energy = 20;

	public Path path;
	public ParticleEmitter buuurn;

	PathState pathState;
	float nextHitTestTime;

	void Start()
	{
		pathState = path.Begin();
	}

	void Update()
	{
		nextHitTestTime -= Time.deltaTime;
		if (nextHitTestTime < 0)
		{
			nextHitTestTime = 0.05f;

			int numHits = GetComponent<ShadowTester>().numberOfLights;
			buuurn.emit = (numHits > 0);
			energy -= numHits;

			if (energy <= 0)
			{
				buuurn.transform.parent = null;
				buuurn.emit = false;
				GameObject.Destroy(gameObject);
				return;
			}
		}

		path.Progress(pathState, Time.deltaTime);

		transform.position = pathState.position;
		//Debug.Log(pathState.rotation);
		transform.rotation = Quaternion.Euler(0, pathState.rotation, 0);
	}

	void OnGUI()
	{
		GUI.Label(new Rect(150, 50, 100, 30), energy.ToString());
	}
}
