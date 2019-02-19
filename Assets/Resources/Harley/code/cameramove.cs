using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour {
    public Transform target;
    public Vector3 offsetpos;
    public float movespeed = 5;
    public float turnspeed =10;
    public float smoothspeed = 0.5f;
    Quaternion targetRotation;
    Vector3 targetpos;
    bool smoothRotating = false;
	
	// Update is called once per frame
	void Update () {
        moveWithTarget();
        //LookAtTarget();
        if (Input.GetKeyDown(KeyCode.G) && !smoothRotating)
        {
            StartCoroutine("RotateAroundTarget",45);
        }
        if  (Input.GetKeyDown(KeyCode.H)&& !smoothRotating)
        {
            StartCoroutine("RotateAroundTarget", -45);
        }

    }
    void moveWithTarget()
    {
        targetpos = target.position + offsetpos;
        transform.position = Vector3.Lerp(transform.position, targetpos, movespeed * Time.deltaTime);
    }
    //void LookAtTarget()
    //{
    //    targetRotation = Quaternion.LookRotation(target.position - transform.position);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);
    //}
    IEnumerator RotateAroundTarget(float angle)
    {
        Vector3 vel = Vector3.zero;
        Vector3 targetOffsetPos = Quaternion.Euler(0, angle, 0) * offsetpos;
        float dist = Vector3.Distance(offsetpos, targetOffsetPos);
        smoothRotating = true;
        while(dist > 0.02f)
        {
            offsetpos = Vector3.SmoothDamp(offsetpos, targetOffsetPos, ref vel, smoothspeed);
            dist = Vector3.Distance(offsetpos, targetOffsetPos);
            yield return null;
        }
        smoothRotating = false;
        offsetpos = targetOffsetPos;
    }
}
