﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeek : MonoBehaviour
{
    public Transform target;
    public Transform Flashlight;
    Vector3 velocity;

    public bool flashlightSeen = false;
    public bool targetSpotted = false;
    public bool targetWasSpotted = false;
    public bool sound;


    public AudioClip Spotted;
    AudioSource source;

    public float viewRad; //enemies range of sight
    float s_ViewRad;//s_ == script value
    [Range(0, 360)]

    public float flashLightRad;

    public float viewAng;//enemies line of sight
    public float delay;
    public LayerMask targetMask;
    public LayerMask Wall;

    public List<Transform> visibleTargets = new List<Transform>();




    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        StartCoroutine(FindTargetDelayed(delay));
        velocity = Vector3.zero;
    }

    private void Update()
    {
        if(targetSpotted)
        {
            s_ViewRad = 20;
        }
        else
        {
            s_ViewRad = viewRad;
        }
        for(int i = 0; i < visibleTargets.Count; i++)
        {
            if (visibleTargets[i]==target)
            {
                targetSpotted = true;
                if(!sound)
                {
                    source.PlayOneShot(Spotted);
                    sound = true;
                }
                targetWasSpotted = true;
                transform.LookAt(visibleTargets[i]);
            }

            if (visibleTargets[i] == Flashlight && visibleTargets[i] != target)
            {
                flashlightSeen = true;
            }
        }
        if(!visibleTargets.Contains(Flashlight))
        {
            flashlightSeen = false;
        }
        if(visibleTargets.Count == 0)
        {
            targetSpotted = false;
            flashlightSeen = false;
            sound = false;
        }
    }
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, s_ViewRad, targetMask);//create detection sphere
        Collider[] flashlightInView = Physics.OverlapSphere(transform.position, flashLightRad, targetMask);
        for (int i = 0; i < targetsInView.Length; i++)
        {
            for(int j = 0; j < flashlightInView.Length; j++)
            {

                if(flashlightInView[j].gameObject.CompareTag("flashlight"))
                {
                    visibleTargets.Add(flashlightInView[j].gameObject.transform);
                }

                Transform target = targetsInView[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if(Vector3.Angle(transform.forward, dirToTarget) < viewAng /2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);
                    if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, Wall))
                    {
                        visibleTargets.Add(target);
                    }
                }
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position, flashLightRad);
    }
    public Vector3 DirectionFromAng(float angInDeg, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angInDeg += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angInDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angInDeg * Mathf.Deg2Rad)); 
    }


    IEnumerator FindTargetDelayed(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

}
