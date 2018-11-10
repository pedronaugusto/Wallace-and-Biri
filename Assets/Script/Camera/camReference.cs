using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camReference : MonoBehaviour {

	public Transform player;
    public float yOffset;

	PlayerStatus status = PlayerStatus.Normal;
	// Use this for initialization
	void Start () {
        transform.position = player.position + new Vector3(0, yOffset, 0);
		//status = player.GetComponent<status>;

	}
	
	// Update is called once per frame
	void Update () {
		//status = player.GetComponent<status>;
		transform.position = player.position + new Vector3(0, yOffset, 0);
        transform.forward = new Vector3(player.forward.x, 0 , player.forward.z);
    }
}
