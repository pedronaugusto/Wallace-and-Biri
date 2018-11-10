using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Biri : MonoBehaviour {

    public float followDistance;
    public Transform target;
    public Transform player;
    private NavMeshAgent agent;
	public Animator biri_anim;
	public float timer;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
		biri_anim = GetComponent<Animator> ();
		biri_anim.SetFloat ("Time", timer);
        agent.speed *= 3;

    }
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		biri_anim.SetFloat ("Time", timer);
		
        if((transform.position - target.position).magnitude > followDistance)
        {
            agent.isStopped = false;
			biri_anim.SetBool ("Run", true);
            agent.SetDestination(target.position);
			timer = 0;
            
        }
        else
        {
			biri_anim.SetBool ("Run", false);
            agent.isStopped = true;
        }
	}
}
