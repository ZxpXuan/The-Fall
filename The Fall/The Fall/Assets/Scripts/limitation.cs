﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class limitation : MonoBehaviour {
    private int i = 0;
    public int limit = 5;
    private int buildIndex;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        getscene();
	}
    void getscene()
    {
        //sceneName = SceneManager.GetActiveScene().name;
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void OnCollisionEnter(Collision collision)
    {

        i = i + 1;
        Debug.Log(i);
        if (i > limit)
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}
