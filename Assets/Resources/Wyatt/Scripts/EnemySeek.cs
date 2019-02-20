using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeek : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject target;
    public float maxforce;
    public float maxSpd;
    public float maxVel;
    Vector3 velocity;

    public bool targetSpotted = false;

    public float viewRad;
    [Range(0,360)]
    public float viewAng;
    public float delay;
    public LayerMask targetMask;
    public LayerMask Wall;

    public List<Transform> visibleTargets = new List<Transform>();




    private void Start()
    {
        StartCoroutine(FindTargetDelayed(delay));
        rb = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
    }

    private void Update()
    {
        for(int i = 0; i < visibleTargets.Count; i++)
        {
            if(visibleTargets[i].gameObject.CompareTag("Player"))
            {
                targetSpotted = true;
                transform.LookAt(visibleTargets[i]);

                //seek(visibleTargets[i]);
            }
            else if(visibleTargets[i].gameObject.CompareTag("Player") != true)
            {
                targetSpotted = false;
            }
        }
    }
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRad, targetMask);

        for(int i = 0; i < targetsInView.Length; i++)
        {
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
