﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
//    private bool paused = false;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.S))
        {
            Cursor.visible = true;

        }

        if (Input.GetKeyDown(KeyCode.P))
        {

            Time.timeScale = 0;
         
        }
       
        if (Input.GetKeyDown(KeyCode.M) )
        {
            Time.timeScale = 1;
         
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {

            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {

            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {

            SceneManager.LoadScene(2);
        }

    }


}
