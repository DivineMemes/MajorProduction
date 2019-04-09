using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{

    //public GameObject[] Colliders = new GameObject[0];
    Vector3 respawnPosition;
    public bool hasdied;


    void Start()
    {
        respawnPosition = gameObject.transform.position;
    }

    private void Update()
    {
        Respawn();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer.Equals(16))
        {
            respawnPosition = collision.gameObject.transform.position;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Enemy")
        {
            hasdied = true;
        }
    }
    
    void Respawn()
    {
        
        if(hasdied)
        {
            transform.parent.position = respawnPosition;
            hasdied = false;
        }
    }

}
