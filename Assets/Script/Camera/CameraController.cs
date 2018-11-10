using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform refTrf;
    public float offset = 3;
    public int camSpeed = 3;
    private Transform plyTrf;
    private CharacterController ply;
    private float lastY;
	public bool invertY;
	public float maxUp;
	public float minUp;

    void Start()
    {
        plyTrf = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ply = plyTrf.GetComponent<CharacterController>();
        lastY = plyTrf.position.y;
		Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X") * Time.fixedDeltaTime;
        float vertical = Input.GetAxis("Mouse Y") * Time.fixedDeltaTime;

        // on bouge la caméra sur l'axe Y
        if (lastY != plyTrf.position.y)
        {
            float diff = plyTrf.position.y - lastY;

            transform.position += new Vector3(0, diff, 0);
        }

        // rotation
        
		/*} else {
			transform.RotateAround(plyTrf.position, Vector3.right, -vertical * camSpeed);
		}

		if(transform.position.y < plyTrf.position.y)
		{
			print("camera_Baixa");
			transform.position = new Vector3(transform.position.x, plyTrf.position.y + 0.15f, transform.position.z);
		}

		//camera limit
		if(refTrf.rotation.eulerAngles.x > maxUp && refTrf.rotation.eulerAngles.x < 180f)
		{
			print("camera_Alta");
			refTrf.rotation = Quaternion.Euler(maxUp,0,0);
		}

		if (refTrf.rotation.eulerAngles.x > 180f && refTrf.rotation.eulerAngles.x < 360f + minUp)
		{
			print("camera_Alta");
			refTrf.rotation = Quaternion.Euler(360f + minUp, 0, 0);
		}*/
        
        

        // position + recalcul du offset
        float oldPosition = transform.position.y;

        Vector3 dreamPosition = ply.transform.position - transform.forward * offset;
        transform.position = Vector3.Lerp(transform.position, dreamPosition, camSpeed);

        transform.position = new Vector3(transform.position.x, oldPosition, transform.position.z); //*/

        // on met à jour la position du ref
        refTrf.position = transform.position;

		Vector3 rotation = transform.TransformVector(new Vector3(0, vertical, 0));

		transform.RotateAround(refTrf.position, refTrf.up, horizontal * camSpeed);
		transform.RotateAround (refTrf.position, refTrf.right, vertical * camSpeed);
		transform.LookAt(refTrf.position);

        //refTrf.forward = new Vector3(transform.forward.x, 0, transform.forward.z);

       

        lastY = plyTrf.position.y;

    }

}
