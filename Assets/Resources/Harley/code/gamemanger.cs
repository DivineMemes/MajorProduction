using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanger : MonoBehaviour {
    public static gamemanger GM;
    public bool pause;
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
}
