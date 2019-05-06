using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour {
    public Transform LookAt;
    private const float Y_Angle_min = -90.0f;
    private const float Y_Angle_max = 80.0f;
    public Transform CamTransform;
    private Camera cam;
    public float heightpadding = 0.05f;
    public float height = 0.5f;
    public LayerMask groundcam;
    RaycastHit hitInfo;
    public float YOffSet;
    public float hightofcamra = 0;
    public float Distance = 10.0f;
    public float Distancey = .5f;
    public float Distancex = .5f;
    public float CurrentX = 0.0f;
    public float CurrentY = 0.0f;
    public float SensivityX = 4.0f;
    public GameObject stop;
    public GameObject cross_hair;
    public float SensivityY = 1.0f;
   // public Vector3 offset = new Vector3 (0,.5f,0);
	// Use this for initialization
	void Start () {
        //CamTransform = transform;

        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        if(gamemanger.GM.pause == true)
        {
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
        if (gamemanger.GM.pause == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
            CurrentX += Input.GetAxis("Mouse X");
        CurrentY -= Input.GetAxis("Mouse Y");
        CurrentY = Mathf.Clamp(CurrentY, Y_Angle_min, Y_Angle_max);
        Debug.DrawLine(transform.position, transform.position - Vector3.up * height, Color.red);

    }
    void LateUpdate ()
    {
        Vector3 dir = new Vector3(0,0, -Distance);
        //dir.y = YOffSet;
        Quaternion rotation = Quaternion.Euler(CurrentY, CurrentX, 0);
        CamTransform.position = LookAt.position + rotation * (dir);
        CamTransform.LookAt(LookAt.position);
    }
    //void ground()
    //{
    //    if (Physics.Raycast(transform.position, -transform.up, out hitInfo, height + heightpadding, groundcam))
    //    {
    //        if (Vector3.Distance(transform.position, hitInfo.point) < height)
    //        {
    //            transform.position = Vector3.Lerp(transform.position, transform.position + transform.up * height, 10* Time.deltaTime);
    //        }
    //    }
    //}
}
