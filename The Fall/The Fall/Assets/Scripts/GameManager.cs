using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool paused = false;
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
      
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
}
