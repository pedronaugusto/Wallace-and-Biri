using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {

    public BiriRef biriRef;
    public PlayerController player;
    private Vector3 biriNewPosition;
	Camera cam;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("BiriSend"))
        {
			Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
            {
                biriNewPosition = hit.point;
            }
            biriRef.newPosition = biriNewPosition;
            biriRef.camOverride = true;
         
		}
	}
}

