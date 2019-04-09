using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour {
    public float raylength = 10;
    RaycastHit hit;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);
        if (Physics.Raycast(transform.position, transform.position + transform.up, out hit, raylength))
        {

            if(hit.collider.tag == "Player")
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //print("hit me");
                SceneManager.LoadScene("you win");
            }
        }
   }
}
