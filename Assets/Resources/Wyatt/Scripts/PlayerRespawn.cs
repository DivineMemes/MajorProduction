using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    Vector3 respawnPosition;
    void Start()
    {
        respawnPosition = gameObject.transform.position;
    }

    private void Update()
    {
        //Respawn();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer.Equals(16))
        {
            respawnPosition = collision.gameObject.transform.position;
            Destroy(collision.gameObject);
        }
    }
    
    void Respawn()
    {   
        //do respawn when you die
    }

}
