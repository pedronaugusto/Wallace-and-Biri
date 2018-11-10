using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiriRef : MonoBehaviour {

    public Transform player;
    public bool camOverride;
    public Vector3 newPosition;
	public float maximumAloneTime;
	private float goTime;

	// Use this for initialization
	void Start () {
        transform.position = player.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!camOverride)
        {
            transform.position = player.position;
            transform.forward = player.forward;
			goTime = 0;
        }
        else
        {
            transform.position = newPosition;
			goTime += Time.deltaTime;
			if (goTime > maximumAloneTime) {
				camOverride = false;
			}
        }
	}
}
