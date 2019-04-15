using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moiro : MonoBehaviour
{

    public float velocity/* = 5*/;
    public float velocitybase = 4;
    public float normalradius;
    public float currentradius;
    public SphereCollider mysoud;
    public float velocitycrouch = 2;
    public float turnspeed = 10;
    Vector2 input;
    public float maxvol = 1.0f;
    public float pich = 1.0f;
    float angle;
    float horizontal;
    float vertical;
    public float height = 0.5f;
    public float heightnormle = 0.9f;
    public float heigthcrouch = .5f;
    public float walls = 0.5f;
    public float traps = 0.5f;
    public float roof = 0.5f;
    public float heightpadding = 0.05f;
    public float wallpadding = 0.05f;
    public float trappadding = 0.05f;
    public float roofpadding = 0.05f;
    public LayerMask ground;
    public LayerMask wall;
    public LayerMask roofs;
    public LayerMask trap;
    public float airtime = 0;
    public float maxGroundAngle = 120;
    public bool debug;
    float groundAngle;
    public bool runisdown;
    public bool crouchisdown;
    public bool grounded;
    public int i;
    bool inawall;
    Vector3 forword;
    public bool isworking;
    RaycastHit hitInfo;
    public Rigidbody rm;
    public controler controller;
    Quaternion targetRotation;
    Transform cam;
    public Collider collider;
    public float jump;
    public float velocityrun = 8f;
    public AnimationCurve velocityCurve;
    public AnimationCurve jumpCurve;
    public float down;
    public float timeToReachTerminalVelocity = 4;
    public bool once;
    public float timereset;
    public bool hid;
    public float duration = 3.0f;
    public Transform size;
    public Vector3 normelsize = new Vector3 (1,2,1);
    public Vector3 crouchsize = new Vector3 (1,1,1);

    // Use this for initialization
    void Start()
    {
        cam = Camera.main.transform;
        velocitybase = velocity;
        currentradius = normalradius;
        size.localScale = normelsize;
        gamemanger.GM.once = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gamemanger.GM.pause == true)
        {
            controller.you.enabled = false;
            return;
        }
        controller.you.enabled = true;
        mysoud.radius = currentradius;
        Input();
        
        CalculateDirection();
        CalculateForward();
        CalculateGroundAngle();
        if(!controller.run && !controller.crouch)
        {
            velocity = velocitybase;
        }
        crouch_run();
        wall1();
        wall2();
        wall3();
        wall4();
        wall5();
        wall6();
        wall7();
        wall8();
        ceiling();
        CheckGround();
        //checkwall();
        ApplyGravity();
        DrawDebugLines();
        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1)
        {
            return;
        }
        //if (controller.jump == true)
        //{
        //    float amount = 0.0f;
        //    amount += Time.deltaTime;
        //    float perc = amount / duration;
        //    transform.position += transform.up  * jump  * Time.deltaTime;
        //}
        Rotate();
        Move();
        walk();
        //trap1();
    }
    void OnCollisionStay(Collision hit)
    {
        foreach (ContactPoint contact in hit.contacts)
        {

            if (hit.gameObject.layer == wall)
            {
                //print("hit me");
            }
            Debug.DrawRay(contact.point, contact.normal, Color.blue);

        }

    }
    void Input()
    {
        if (hid == false)
        {
            horizontal = controller.horizontal;
            vertical = controller.vertical;
            input.x = horizontal;
            input.y = vertical;
        }
        if(hid== true)
        {
            horizontal = 0;
            velocity = 0;
            input.x = horizontal;
            input.y = vertical;
        }
    }
    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);
    }
    void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;

    }
    void Move()
    {
        if (groundAngle >= maxGroundAngle) return;
       
        transform.position += forword * velocity * Time.deltaTime;
       
        

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
        groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);

    }
    private void FixedUpdate()
    {
        if (grounded == true)
        {
            airtime = 0;
        }
    }
    void CheckGround()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hitInfo, height + heightpadding, ground))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < height)
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
            airtime += Time.deltaTime;
            float curveEval = velocityCurve.Evaluate(Mathf.Clamp01(airtime / timeToReachTerminalVelocity));
            transform.position += (Vector3.down * curveEval * down);
            //transform.position += Physics.gravity * Time.deltaTime;
        }
    }
    void DrawDebugLines()
    {
        if (!debug) return;
        Debug.DrawLine(transform.position, transform.position + forword * height * 2, Color.blue);
        Debug.DrawLine(transform.position, transform.position + forword * height * 2, Color.green);
        Debug.DrawLine(transform.position, transform.position + (forword - transform.up) * height * 2, Color.green);
        Debug.DrawLine(transform.position, transform.position + (forword + transform.up) * height * 2, Color.green);
        Debug.DrawLine(transform.position, transform.position + transform.right * height * 2, Color.cyan);
        Debug.DrawLine(transform.position, transform.position + (transform.right + transform.up) * height * 2, Color.cyan);
        Debug.DrawLine(transform.position, transform.position + (transform.right - transform.up) * height * 2, Color.cyan);
        Debug.DrawLine(transform.position, transform.position + -transform.right * height * 2, Color.magenta);
        Debug.DrawLine(transform.position, transform.position + (-transform.right - transform.up) * height * 2, Color.magenta);
        Debug.DrawLine(transform.position, transform.position + (-transform.right + transform.up) * height * 2, Color.magenta);
        Debug.DrawLine(transform.position, transform.position + transform.up * height * 2, Color.black);
        Debug.DrawLine(transform.position, transform.position + (transform.up - (transform.right * 0.1f)) * height * 2, Color.black);
        Debug.DrawLine(transform.position, transform.position + (transform.up + (transform.right * 0.1f)) * height * 2, Color.black);
        Debug.DrawLine(transform.position, transform.position + (transform.up + (transform.forward * 0.1f)) * height * 2, Color.black);
        Debug.DrawLine(transform.position, transform.position + (transform.up - (transform.forward * 0.1f)) * height * 2, Color.black);
        Debug.DrawLine(transform.position, transform.position - Vector3.up * height, Color.red);
        Debug.DrawLine(transform.position, transform.position - (Vector3.up + (transform.right * 0.1f)) * height, Color.red);
    }
    void wall1()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, walls + wallpadding, wall))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < walls)
            {
                //print("hit me wall front of me");
                transform.position = Vector3.Lerp(transform.position, transform.position + -transform.forward * walls, 9 * Time.deltaTime);
            }
        }

    }
    void ceiling()
    {
        if (Physics.Raycast(transform.position, transform.up, out hitInfo, roof + roofpadding, roofs))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < roof)
            {
                //print("hit me cealling");
                transform.position = Vector3.Lerp(transform.position, transform.position + -transform.up * roof, 9 * Time.deltaTime);
            }
        }
    }
    void wall2()
    {
        if (Physics.Raycast(transform.position, transform.right, out hitInfo, walls + wallpadding, wall))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < walls)
            {
                //print("hit me left wall ");
                transform.position = Vector3.Lerp(transform.position, transform.position + -transform.right * walls, 9 * Time.deltaTime);
            }
        }

    }
    void wall3()
    {
        if (Physics.Raycast(transform.position, -transform.right, out hitInfo, walls + wallpadding, wall))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < walls)
            {
               // print("hit me right wall ");
                transform.position = Vector3.Lerp(transform.position, transform.position + transform.right * walls, 9 * Time.deltaTime);
            }
        }
    }
    void wall4()
    {
        if (Physics.Raycast(transform.position, -transform.right - transform.up, out hitInfo, walls + wallpadding, wall))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < walls)
            {
               // print("hit me right wall ");

                transform.position = Vector3.Lerp(transform.position, transform.position + transform.right * walls, 9 * Time.deltaTime);
            }
        }
    }
    void wall5()
    {
        if (Physics.Raycast(transform.position, transform.right + transform.up, out hitInfo, walls + wallpadding, wall))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < walls)
            {
                //print("hit me left wall ");
                transform.position = Vector3.Lerp(transform.position, transform.position + -transform.right * walls, 9 * Time.deltaTime);
            }
        }

    }
    void wall6()
    {
        if (Physics.Raycast(transform.position, transform.forward - transform.up, out hitInfo, walls + wallpadding, wall))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < walls)
            {
                //print("hit me wall front of me");
                transform.position = Vector3.Lerp(transform.position, transform.position + -transform.forward * walls, 9 * Time.deltaTime);
            }
        }

    }
    void wall7()
    {
        if (Physics.Raycast(transform.position, transform.forward + transform.up, out hitInfo, walls + wallpadding, wall))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < walls)
            {
                //print("hit me wall front of me");
                transform.position = Vector3.Lerp(transform.position, transform.position + -transform.forward * walls, 9 * Time.deltaTime);
            }
        }

    }
    void wall8()
    {
        if (Physics.Raycast(transform.position, transform.right - transform.up, out hitInfo, walls + wallpadding, wall))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < walls)
            {
               // print("hit me left wall ");
                transform.position = Vector3.Lerp(transform.position, transform.position + -transform.right * walls, 9 * Time.deltaTime);
            }
        }

    }
    void trap1()
    {
        if (Physics.Raycast(transform.position, transform.forward - transform.up, out hitInfo, traps + trappadding, trap))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < traps)
            {
                //print("hit me a trap");
            }
        }
    }
    void enmeys()
    {

    }
   void crouch_run()
    {
        if (controller.crouch&&!crouchisdown)
        {
            velocity /= 2;
            height = heigthcrouch;
            size.localScale = crouchsize;
            if(velocity == velocitycrouch)
            {
                crouchisdown = true;
            }
            

        }

        if (controller.run&&!runisdown)
        {

            velocity *=2;
            if(velocity == velocityrun)
            {
                runisdown = true;
            }
          
        }
        if (!controller.crouch)
        {
            height = heightnormle;
            size.localScale = normelsize;
            //velocity = velocitybase;
        }
        if (velocity == velocitybase)
        {
            runisdown = false;
            crouchisdown = false;
        }
    }
    void walk()
    {
        AudioClip clip = null;
        RaycastHit hit;
        //if (i == controller.footsteps.Length)
        //{
        //    i = 0;
        //}
        
        if (Physics.Raycast(transform.position, -transform.up,out hit, 1.2f))
        {
            if(hit.collider.CompareTag("ground"))
            {

                pich = UnityEngine.Random.Range(0.2f,3.0f);
                for (i = 0; i < controller.footsteps.Length; i++)
                {
                    clip = controller.footsteps[i];
                   
                }
                //if (i == controller.footsteps.Length)
                //{
                //    i = 0;
                //}
                //maxvol = UnityEngine.Random.Range(0.2f, 0.9f);

            }
            if(clip!= null&&!once)
            {
                controller.me.PlayOneShot(clip, maxvol);
                controller.me.pitch = pich;
                once = true;
                Invoke("reset", timereset);
            }
        }
    }
    void reset()
    {
        once = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy"&&controller.noinput == false)
        {
            isworking = true;
            controller.sound.enabled = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemy")
        {
            isworking = false;
            controller.sound.enabled = false;
        }
    }
   
}
