using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(CapsuleCollider))]
public class CapsuleResizer : MonoBehaviour
{
    public float trueHeight = 2.0f;
	   
	// Update is called once per frame
	void Update ()
    {
        CapsuleCollider capsule = GetComponent<CapsuleCollider>();

        Vector3 dir = Vector3.zero;

        dir[capsule.direction] = 1;

        capsule.center = dir * (trueHeight / 2);
        capsule.height = trueHeight;
    }
}
