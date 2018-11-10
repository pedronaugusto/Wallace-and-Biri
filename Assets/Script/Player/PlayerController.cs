using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerStatus
{
	Normal,
	Dot,
	Attacking
	//HeadphoneMode,
}

public class PlayerController : MonoBehaviour {
	
    CharacterController characterController;

    public float speed;
	public float rotationSpeed;
    public float jumpSpeed;
    public float gravity;
    public int jumpsConst;
	public int attackConst;
    public float durationJump;
	public float fullCooldown;
	public float midCooldown;
	public float attackRadius;
	public float maxDistance;

	public UiController uiToggle;

	public Animator Anim;
	public Transform camera;
	public Transform camRef;

	private Vector3 cameraHorizontalForward;

    private float timerJump;
    private int nbJumps;
	private float timerAttack;
	private int nbAttack;
	private float timeAttackEnd;
	private float attackButtonClicks;
    
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 desiredDirection = Vector3.zero;

	private PlayerStatus status = PlayerStatus.Normal;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Anim = GetComponent<Animator>();
		nbAttack = attackConst;
		attackButtonClicks = attackConst;
	}

    void Update()
	{
		timerJump += Time.fixedDeltaTime;
		timeAttackEnd += Time.fixedDeltaTime;
		timerAttack += Time.fixedDeltaTime;


		float V = Input.GetAxis ("Vertical");
		float H = Input.GetAxis ("Horizontal");
		float rightHorizontal = Input.GetAxis ("Mouse X");

		if (status != PlayerStatus.Attacking) {
			if (Input.GetAxis ("Dot") > 0 && characterController.isGrounded) {
				print ("Dot");
				uiToggle.crossActive = true;
				Anim.SetBool ("Dot", true);
				setDot ();
			} else {
				uiToggle.crossActive = false;
				Anim.SetBool ("Dot", false);
				status = PlayerStatus.Normal;
			}
		}



		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		switch (status) {
		case PlayerStatus.Normal:
				
			cameraHorizontalForward = new Vector3 (camera.forward.x, 0.0f, camera.forward.z).normalized;
			desiredDirection = V * cameraHorizontalForward + H * camera.right.normalized;

			float yForward = transform.forward.y;
			transform.forward = Vector3.Lerp (transform.forward, desiredDirection, 0.3f);
			transform.forward = new Vector3 (transform.forward.x, yForward, transform.forward.z);

			moveDirection.x = desiredDirection.x * speed;
			moveDirection.z = desiredDirection.z * speed;

			if (characterController.isGrounded) {

				Anim.SetFloat ("Speed", Mathf.Abs (V) + Mathf.Abs (H));
				Anim.SetFloat ("TurningSpeed", 0);
				Anim.SetBool ("isGrounded", true);
				
			
				timerJump = 0;
				nbJumps = jumpsConst;
			}
			break;


		case PlayerStatus.Dot:

			transform.Rotate (Vector3.up, rightHorizontal * rotationSpeed * Time.deltaTime);
			desiredDirection = V * new Vector3 (transform.forward.normalized.x, 0, transform.forward.normalized.z) + H * new Vector3 (transform.right.normalized.x, 0, transform.right.normalized.z);
					
			moveDirection.x = desiredDirection.x * speed;
			moveDirection.z = desiredDirection.z * speed;



			if (characterController.isGrounded) {

				Anim.SetFloat ("Speed", V);
				Anim.SetFloat ("TurningSpeed", H);
				Anim.SetBool ("isGrounded", true);

				timerJump = 0;
				nbJumps = jumpsConst;
			}
			break;


		case PlayerStatus.Attacking:
			
			moveDirection = Vector3.zero;


			break;
		}


		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////


		float coefJump = 0;
		if (nbJumps == 2)
			coefJump = 1;
		else if (nbJumps == 1)
			coefJump = 1f;

		if (Input.GetButton ("Jump_Joystick") && nbJumps > 0 && (nbJumps == 2 || timerJump >= durationJump)) {
			if (status == PlayerStatus.Normal) {
				Anim.SetBool ("ExitAttack", true);
				if (nbJumps == 2) {
					Anim.SetTrigger ("Jump");
					Anim.SetBool ("isGrounded", false);
				} else {
					Anim.SetTrigger ("DoubleJump");
					Anim.SetBool ("isGrounded", false);
				}

				moveDirection.y = jumpSpeed * coefJump;
				nbJumps--;
			}

		}

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		if (Input.GetButton ("MeleeAttack_Joystick") && characterController.isGrounded && timerAttack > midCooldown && attackButtonClicks > 0) {
			print ("fds");
			attackButtonClicks--;
			timerAttack = 0;
			timeAttackEnd = 0;
			status = PlayerStatus.Attacking;

			float coefAttack = 0.0f;
			if (nbAttack == 3)
				coefAttack = 1.0f;
			else if (nbAttack == 2)
				coefAttack = 1.0f;
			else if (nbAttack == 1)
				coefAttack = 2.0f;

			if (nbAttack == 3) {
				Anim.SetTrigger ("Attack1");
				Anim.SetBool ("ExitAttack", false);
				//Attack ();
			} else if (nbAttack == 2) {
				Anim.SetTrigger ("Attack2");
				Anim.SetBool ("ExitAttack", false);
				//Attack ();
			} else if (nbAttack == 1) {
				Anim.SetTrigger ("Attack3");
				status = PlayerStatus.Attacking;
			}
			nbAttack--;
		} 

		if (timeAttackEnd > fullCooldown) {
			print ("fds2");
			attackButtonClicks = attackConst;
			Anim.SetBool ("ExitAttack", true);	
			nbAttack = attackConst;
			status = PlayerStatus.Normal;
		}

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		moveDirection.y -= gravity * Time.deltaTime;
		characterController.Move (moveDirection * Time.deltaTime);
	}




	void setDot()
	{
		status = PlayerStatus.Dot;
	}

	private void Attack()
	{
		RaycastHit[] hits = Physics.SphereCastAll (transform.position, attackRadius, transform.forward, maxDistance);


		for (int i = 0; i < hits.GetLength (0); i++) {

			if(hits[i].transform.gameObject.tag == "DestructibleBox")
			{
				hits[i].transform.gameObject.GetComponent<DestructibleBox>().ApplyDamage();
				print("damage");
			}
		}
	}

		
}
