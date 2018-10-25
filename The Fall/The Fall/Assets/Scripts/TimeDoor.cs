﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDoor : MonoBehaviour {
    public GameObject timedoo;
    public float showtime = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.collider.tag == "ball")
        {
            timedoo.GetComponent<Renderer>().enabled = false;
            Invoke("ShowAgain", showtime);
        }

	}

    public void ShowAgain()

    {
        timedoo.GetComponent<Renderer>().enabled = true;
    }


}
