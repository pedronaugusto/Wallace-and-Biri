using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour {

	private float rightVertical;
	public float yRotationSpeed;
	public float minAngle;
	public float maxAngle;

	private float rotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		rightVertical = Input.GetAxis ("Mouse Y");
		rotation += rightVertical * yRotationSpeed * Time.deltaTime;
		rotation = Mathf.Clamp (rotation, minAngle, maxAngle);

		transform.localEulerAngles = new Vector3 (rotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
	}
}
