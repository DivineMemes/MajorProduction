﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gamemanger : MonoBehaviour {
    public static gamemanger GM;
    public bool pause;
    public bool grab;
    public bool win;
    public int once;
    public int thrownumber;
    public bool throwme;
    //public GameObject grabui;
    //public GameObject normalui;
    //public GameObject hidui;
    public bool hide;
    // Use this for initialization
    void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
       
    }
    void Update()
    {
        
        if(win == true && once == 0)
        {
            
            SceneManager.LoadScene("you win");
            once = 1;
            //win = false;
        }
        if(once == 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //if (hide== false && grab == false)
        //{
        //    hidui.SetActive(false);
        //    normalui.SetActive(true);
        //    grabui.SetActive(false);
        //}
       
        
        
    }
}
