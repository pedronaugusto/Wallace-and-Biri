using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBreakBox : MonoBehaviour {

	public float timerBoxDestroy;
	public float boxDestroyCooldown;
	public GameObject destroyObject;

	//Color colorStart;
	//Color colorEnd;

	// Use this for initialization
	void Start () {

		//colorStart = GetComponentInChildren<MeshRenderer> ().material.color;
		//colorEnd = new Color (colorStart.r, colorStart.g, colorStart.b, 0.0f);

	}

	// Update is called once per frame
	void Update () {

		timerBoxDestroy += Time.fixedDeltaTime;

		//AlphaOut ();

		if (timerBoxDestroy > boxDestroyCooldown) {
			Destroy (destroyObject);
		}
	}

	/*public void AlphaOut()
	{
		float c;
		float alpha = GetComponentInChildren<MeshRenderer> ().material.color.a;

		for (c = 0.0f; c < 1.0f; c += timerBoxDestroy / boxDestroyCooldown) {

			Color newColor = new Color (colorStart.r, colorStart.g, colorStart.b, Mathf.Lerp (alpha, 0, c));
			GetComponentInChildren<MeshRenderer> ().material.color = newColor;
		}
	}*/
}
