using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject target;
    public float maxforce;
    public float maxSpd;
    public float maxVel;
    Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
    }
    void Update()
    {
        transform.LookAt(target.transform);
        seek(target.transform);
    }


    void pursuit(Transform target)
    {
        //int T = 3;
        //Vector3 futurepos = target.transform.position + target.GetComponent<Rigidbody>().velocity * T;
        //return seek(new Vector3 (futurepos.));
    }
    void seek(Transform target)
    {
        Vector3 desiredVel = target.transform.position - transform.position;
        desiredVel = desiredVel.normalized * maxVel * Time.deltaTime;
        Vector3 steering = desiredVel - velocity;
        steering = Vector3.ClampMagnitude(steering, maxforce);
        steering = steering / rb.mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, maxSpd);
        rb.velocity += velocity * Time.deltaTime;

    }
}
