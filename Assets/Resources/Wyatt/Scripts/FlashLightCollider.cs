using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FlashLightCollider : MonoBehaviour
{

    CapsuleCollider collider;
    bool start;
    int layerMask;
    float originalHeight;
    void Start()
    {
        collider = gameObject.GetComponent<CapsuleCollider>();
        //originalHeight = collider.height;

        layerMask = 1 << 13;
        layerMask = ~layerMask;

    }

    void Update()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward*-1), out hit, Mathf.Infinity, layerMask))
        {
            if(!collider.enabled)
            {
                collider.enabled = true;
            }
            Vector3 scaleDir = Vector3.zero;
            scaleDir[collider.direction] = 1;
            originalHeight = hit.distance;
            collider.height = originalHeight*2;
            collider.center = scaleDir * (originalHeight*-1); //negative number because forward of the parented flashlight gameobject is backwards
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward*-1) * hit.distance, Color.blue);
            //Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            collider.enabled = false;
           // Debug.Log("nothing");
        }
    }
}
