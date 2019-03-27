using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camracontrol : MonoBehaviour {
    float xAxisClamp = 0.0f;
    public Rigidbody playerBody;
    public Transform game;
    public float mouseSensitivity_x2;
    public float mouseSensitivity_y2;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //if (gamemaniger.GM.pose == true)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;

        //    return;
        //}
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        RotateCamera();
    }
    void RotateCamera()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity_x2;
        float rotAmountY = mouseY * mouseSensitivity_y2;

        xAxisClamp -= rotAmountY;

        Vector3 targetRotCam = transform.rotation.eulerAngles;
        //Vector3 targetRotBody = game.transform.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;
        targetRotCam.y += rotAmountX;

        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            targetRotCam.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRotCam.x = 270;
        }

        print(mouseY);


        //transform.rotation = Quaternion.Euler(targetRotCam);
        //game.rotation = Quaternion.Euler(targetRotBody);
    }
   
}
