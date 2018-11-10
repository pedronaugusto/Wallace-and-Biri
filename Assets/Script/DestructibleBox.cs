using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructibleBox : MonoBehaviour {

		public int maxHitPoints;
		public float hitAngle = 360.0f;
		public float hitForwardRotation = 360.0f;
	 	
		public int currentHitPoints { get; private set; }

		public GameObject destroyObject;
		public GameObject gameObject;

		public UnityEvent Death, OnResetDamage;


		private float timeSinceLastHit = 0.0f;
		public Collider collider;

		void Start()
		{
			ResetDamage();
			collider = GetComponent<Collider>();
		}

		void Update()
		{
			
		}

		public void ResetDamage()
		{
			currentHitPoints = maxHitPoints;
			OnResetDamage.Invoke();
		}

		public void SetColliderState(bool enabled)
		{
			collider.enabled = enabled;
		}

		public void ApplyDamage()
		{
			if (currentHitPoints <= 0)
			{
				
				return;
			}

			Vector3 forward = transform.forward;
		forward = Quaternion.AngleAxis(hitForwardRotation, transform.up) * forward;

			//we project the direction to damager to the plane formed by the direction of damage
		Vector3 positionToDamager = transform.forward - transform.position;
			positionToDamager -= transform.up * Vector3.Dot(transform.up, positionToDamager);

			if (Vector3.Angle(forward, positionToDamager) > hitAngle * 0.5f)
				return;

			currentHitPoints -= 1;

		if (currentHitPoints <= 0) {
			
		}
			Instantiate (destroyObject, transform.position, transform.rotation);
			Destroy (gameObject);
			
		}

		void LateUpdate()
		{
			
		}
	}
		
