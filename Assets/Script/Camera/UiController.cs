using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour {

	public bool crossActive;
	public CanvasGroup crosshairGroup;
	private PlayerStatus status;

	// Use this for initialization
	void Start () {
		
		crossActive = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!crossActive) {
			crosshairGroup.alpha = 0;
		} else {
			crosshairGroup.alpha = 1;
		}
	}
}
