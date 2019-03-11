using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controler : MonoBehaviour {
    public float horizontal;
    public float vertical;
    public bool jump;
    public moiro play;
    public bool noinput;
    public Collider sound;
    public bool downrun;
    public bool downcrouch;
    public AudioSource me;
    public AudioClip[] footsteps;
    public bool crouch;
    //public Animator you;
    public bool run;
    public bool light;
    public GameObject stop;
    public float number = 0;
    public Light myflaselight;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (gamemanger.GM.pause == true)
        {
             if (Input.GetKeyDown(KeyCode.Escape))
            {
                gamemanger.GM.pause = false;
                stop.SetActive(false);
            }
            return;
        }
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            vertical = -1;
            //me.clip = walk;
            me.Play();

            //you.ResetTrigger("idle");
            //you.SetTrigger("walk");
            //sound.enabled = true;
            noinput = false;
        }
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            vertical = 1;
            //me.clip = walk;
            me.Play();
            //you.ResetTrigger("idle");
            //you.SetTrigger("walk");
            //sound.enabled = true;
            noinput = false;
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            horizontal = -1;
            //you.ResetTrigger("idle");
            //you.SetTrigger("walk");
            //sound.enabled = true;
            noinput = false;
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKeyDown(KeyCode.A))
        {
            horizontal = 1;
            //you.ResetTrigger("idle");
            //you.SetTrigger("walk");
            //sound.enabled = true;
            noinput = false;
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            horizontal = 0;
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            vertical = 0;
        }
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)&& !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            noinput = true;
            //you.ResetTrigger("walk");
           // you.SetTrigger("idle");
            //sound.enabled = false;
        }
        if (play.grounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.position += transform.up * play.jump * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.LeftControl) && run == false)
        {
            crouch = true;
        }
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            crouch = false;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            light = !light;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamemanger.GM.pause = true;
            stop.SetActive(true);
        }
        
        if (light == true)
        {
            myflaselight.enabled = true;
        }
        if (light == false)
        {
            myflaselight.enabled = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && crouch == false)
        {
            number = 1;
           
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            number = 0;
        }
      if(number == 1)
        {
            run = true;
        }
      else
        {
            run = false;
        }
        //if(gamemanger.GM.grab == true)
        //{
        //    you.SetTrigger("grab");
        //}
        //if (gamemanger.GM.grab == false)
        //{
        //    you.ResetTrigger("grab");
        //}

    }
}
