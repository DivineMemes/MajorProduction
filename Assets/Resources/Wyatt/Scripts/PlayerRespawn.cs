using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    bool hasdied;
    Vector3 respawnPosition;
    void Start()
    {
        respawnPosition = gameObject.transform.position;
    }

    private void Update()
    {
        Respawn();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer.Equals(16))
        {
            respawnPosition = collision.gameObject.transform.position;
            Destroy(collision.gameObject);
        }

        if(collision.collider.gameObject.CompareTag("Enemy"))
        {
            hasdied = true;
        }
    }
    
    void Respawn()
    {
        
        if(hasdied)
        {
            gameObject.transform.position = respawnPosition;
            hasdied = false;
        }
    }

}
