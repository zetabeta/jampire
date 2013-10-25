using UnityEngine;
using System.Collections;

public class ShadowTester : MonoBehaviour
{
	void Update()
	{
		int lightHits = 0;

		foreach (GameObject lightSource in GameObject.FindGameObjectsWithTag("LightSources"))
		{
			if ((transform.position - lightSource.transform.position).sqrMagnitude > (lightSource.light.range * lightSource.light.range))
				continue;

			if (Physics.Raycast(transform.position, lightSource.transform.position - transform.position, (transform.position - lightSource.transform.position).magnitude, 1 << LayerMask.NameToLayer("ShadowCasters")))
				continue;

			++lightHits;
		}

		if (lightHits > 0)
			GetComponentInChildren<Renderer>().material.color = Color.red;
		else
			GetComponentInChildren<Renderer>().material.color = Color.white;
	}
}
