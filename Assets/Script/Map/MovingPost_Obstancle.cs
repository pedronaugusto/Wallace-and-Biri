using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPost_Obstancle : MonoBehaviour {

	public Transform MovingPost;
	public Transform P1;
	public Transform P2;
	public Vector3 newPosition;
	public string currentState;
	public float smooth;
	public float resettime;

	// Use this for initialization
	void Start () {
		
		MovePost ();
	}
	
	// Update is called once per frame
	void Update () {

		MovingPost.position = Vector3.Lerp (MovingPost.position, newPosition, smooth * Time.deltaTime);
		
	}

	void MovePost()
	{
		if (currentState == "Moving to P1") {

			currentState = "Moving to P2";
			newPosition = P2.position;
		} else if (currentState == "Moving to P2") {

			currentState = "Moving to P1";
			newPosition = P1.position;
		}
		Invoke ("MovePost", resettime);
	}
}
