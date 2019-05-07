using System.Collections;
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
    public bool stepover;
    public bool throwme;
    public int onetime;
    public bool sneak;
    //public GameObject grabui;
    //public GameObject normalui;
    //public GameObject hidui;
    public bool hide;
    public int KeyCount;
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
      if(sneak == true)
        {
            onetime = 1;
        }
      if(onetime == 1)
        {
            sneak = false;
        }
        //if (hide== false && grab == false)
        //{
        //    hidui.SetActive(false);
        //    normalui.SetActive(true);
        //    grabui.SetActive(false);
        //}
       if(KeyCount == 4)
        {
            win = true;
        }
        
        
    }
}
