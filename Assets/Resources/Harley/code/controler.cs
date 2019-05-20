using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controler : MonoBehaviour {
    public float horizontal;
    public GameObject player;
    //public float jumphight;
    
    public float vertical;
    public bool jump;
    public moiro play;
    public bool noinput;
    public Collider sound;
    public bool downrun;
    public bool downcrouch;
    public AudioSource me;
    public AudioSource me2;
    public AudioClip[] footsteps;
    public AudioClip voice;
    public bool crouch;
    public Animator you;
    public bool run;
    public bool light;
    public GameObject stop;
    public GameObject cross_hair;
    public float number = 0;
    public GameObject myflaselight;
    public int nomore;
    public Vector3 velocity;
    public hiding mek;
    public GameObject sneak;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update() {

        if (gamemanger.GM.pause == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gamemanger.GM.pause = false;
                stop.SetActive(false);
                cross_hair.SetActive(true);
            }
            return;
        }
        if (gamemanger.GM.KeyCount == 0)
        {
            one.SetActive(false);
            two.SetActive(false);
            three.SetActive(false);
            four.SetActive(false);
        }
        if (gamemanger.GM.KeyCount == 1)
        {
            one.SetActive(true);
        }
        if (gamemanger.GM.KeyCount == 2)
        {
            one.SetActive(false);
            two.SetActive(true);
        }
        if (gamemanger.GM.KeyCount == 3)
        {

            two.SetActive(false);
            three.SetActive(true);
        }
        if (gamemanger.GM.KeyCount == 4)
        {
            three.SetActive(false);
            four.SetActive(true);
        }
        if (mek.isHiding == false)
        {
            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                vertical = -1;
                //me.clip = walk;
                me.Play();
                //sound.enabled = true;
                noinput = false;
            }
        }
        if (mek.isHiding == false)
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                vertical = 1;
                //me.clip = walk;
                me.Play();

                //sound.enabled = true;
                noinput = false;
            }
        }
        if (mek.isHiding == false)
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                horizontal = -1;

                //sound.enabled = true;
                noinput = false;
            }
        }
        if (mek.isHiding == false)
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKeyDown(KeyCode.A))
            {
                horizontal = 1;

                //sound.enabled = true;
                noinput = false;
            }
        }
            if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            horizontal = 0;
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            vertical = 0;
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            noinput = true;
           
            //sound.enabled = false;
        }
        //if (play.grounded == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
              
        //        //player.transform.position.y = play.jump;
        //        player.transform.position += transform.up * play.jump * Time.deltaTime;
        //        //this.transform.Translate(velocity.y * play.jump);
        //        //play.rm.velocity = Vector3.up * play.jump;
        //    }
        //}
        if (Input.GetKey(KeyCode.LeftControl) && run == false)
        {
            sneak.SetActive(false);
            crouch = true;
        }
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            crouch = false;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            light = !light;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamemanger.GM.pause = !gamemanger.GM.pause;
            stop.SetActive(true);
            cross_hair.SetActive(false);
        }

        if (light == true)
        {
            myflaselight.SetActive(true);
        }
        if (light == false)
        {
            myflaselight.SetActive(false);
        }
        if (vertical == 1)
        {
            you.SetBool("walk", true);
         
        }
        if (vertical == -1)
        {
            you.SetBool("walk", true);
        }
        if(horizontal == 1)
        {
            you.SetBool("walk", true);
        }
        if (horizontal == -1)
        {
            you.SetBool("walk", true);
        }
        if (horizontal == 0 && vertical == 0)
        {
            you.SetBool("walk", false);
        }
        if(noinput == true)
        {
            you.SetBool("run", false);
        }
        if (noinput == false)
        {


            if (Input.GetKey(KeyCode.LeftShift) && crouch == false)
            {
                number = 1;

                you.SetBool("run", true);

            }
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                number = 0;
                you.SetBool("run", false);
            }
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
