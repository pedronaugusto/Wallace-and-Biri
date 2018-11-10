using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	public Transform destination;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButton("Teleport_Joystick")) {
			print ("teleport");
			Teleportation ();
		}

	}

	public void Teleportation()
	{
		
			transform.position = destination.position;
			transform.rotation = destination.rotation;

	}
}
