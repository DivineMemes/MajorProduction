using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    Scene current;
    Vector3 respawnPosition;
    public bool hasdied;


    void Start()
    {
        respawnPosition = gameObject.transform.position;
    }

    private void Update()
    {
        Respawn();
        Reload();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.layer.Equals(16))
        //{
        //    respawnPosition = collision.gameObject.transform.position;
        //    Destroy(collision.gameObject);
        //}

        if(collision.gameObject.tag == "Enemy")
        {
            hasdied = true;
        }
    }
    
    void Respawn()
    {
        
        if(hasdied)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            hasdied = false;
        }
    }

    void Reload()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.name);
        }
    }

}
