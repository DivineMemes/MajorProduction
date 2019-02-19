using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public LayerMask layer;
    public float radius;
    bool pickedUp;
    void Start()
    {

    }

    void Update()
    {
        if (!pickedUp)
        {
            Collider[] objectsInRange = Physics.OverlapSphere(gameObject.transform.position, radius, layer);
            for (int i = 0; i < objectsInRange.Length; i++)
            {
                if (objectsInRange[i].gameObject.CompareTag("Player"))
                {

                }
            }
        }
    }
}
