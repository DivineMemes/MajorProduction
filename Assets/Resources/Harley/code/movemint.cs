using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movemint : MonoBehaviour
{
    public float speed;
    public float turnspeed;
    public controler controller;
    Vector3 desierv;
    public float height = 0.5f;
    public float heightpadding= 0.05f;
    public LayerMask ground;
    public float maxGroundAngle = 120;
    public bool debug;
    float groundAngle;
    public float drag;
    float horizontal;
    float vertical;
    public Rigidbody rm;
    Quaternion targertRotation;
    Transform cam;
    float angle;
    RaycastHit hitInfo;
    bool grounded;
    Vector3 forword;
    public float down;
    public float stop;
    public Collider collider;
    // Use this for initialization
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Input();
        CalculateDrection();
        CalculateForward();
        CalculateGroundAngle();
        CheckGround();
        ApplyGravity();
        DrawDebugLines();
        if (Mathf.Abs(desierv.x) <= 1 && Mathf.Abs(desierv.z) <= 1)
        {
            return;
        }
        
        Rotate();
        Move();
      if(controller.noinput == true)
        {
            rm.velocity = Vector3.zero;

        }
        

        
    }

    void OnCollisionStay(Collision hit)
    {
        foreach (ContactPoint contact in hit.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.blue);

        }
    }
    void CalculateForward()
    {
        if (!grounded)
        {
            forword = transform.forward;
            
            return;
        }
        forword = Vector3.Cross(hitInfo.normal, -transform.right);
    }
    void CalculateGroundAngle()
    {
        if (!grounded)
        {
            groundAngle = 90;
            return;
        }
        groundAngle = Vector3.Angle(hitInfo.normal,  transform.forward);
        
    }
    
    void CheckGround()
    {
        if(Physics.Raycast(transform.position,-transform.up,out hitInfo,height + heightpadding ,ground))
        {
            if(Vector3.Distance(transform.position, hitInfo.point) < height)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + transform.up * height, 5 * Time.deltaTime);
            }
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
    void ApplyGravity()
    {
        if (!grounded)
        {
            transform.position += Physics.gravity * Time.deltaTime;
        }
    }
    void DrawDebugLines()
    {
        if (!debug) return;
        Debug.DrawLine(transform.position, transform.position + forword * height * 2, Color.blue);
        Debug.DrawLine(transform.position, transform.position - Vector3.up * height,Color.red);
    }
    void Move()
    {
        if (groundAngle >= maxGroundAngle) return;
        if (desierv.magnitude > 1f) desierv.Normalize();
        rm.velocity = desierv.normalized; //+ forword;
    }
    void CalculateDrection()
    {
        angle = Mathf.Atan2(desierv.x,desierv.z);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }
    void Rotate()
    {
        targertRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targertRotation, turnspeed * Time.deltaTime);
    }
    void Input()
    {
        horizontal = controller.horizontal;
        Vector3 myright = new Vector3(horizontal, 0, 0);
        vertical = controller.vertical;
        Vector3 myup = new Vector3(0, 0 , vertical);
        desierv = (myup + myright) * speed * Time.deltaTime;
    }
}
