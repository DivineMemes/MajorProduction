using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour {
    public moiro mm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (mm.grounded == true && mm.velocity >2f && mm.controller.me.isPlaying == false)
        {
            mm.controller.me.Play();
        }
	}
}
