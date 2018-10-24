﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
//    private bool paused = false;
    private int buildIndex;
    private string sceneName;

    public UIManager uIManager;

    public bool isPaused;

    public int collisionLeft;

    public limitation lim;

    public float restartDelay;
    private float restartStartTime;
    private bool isRestartInitiated;

    [SerializeField]
    List<Animator> objectToDisable;
    // Use this for initialization

    private void Awake()
    {
        if (PlayerPrefs.GetInt("start_type", 99) == 0)
        {
            foreach (Animator anim in objectToDisable)
            {
                anim.enabled = false;

            }
            PlayerPrefs.SetInt("start_type", 1);
        }
    }
    void Start () {
        isRestartInitiated = false;
       // Cursor.visible = false;
        isPaused = false;
        collisionLeft = lim.limit;
       
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            doRestart();
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                isPaused = false;
                unPauseGame();
            }
            else{

                isPaused = true;
                PauseGame();
            }
        }
        
        if(isRestartInitiated&& restartStartTime + restartDelay <Time.time && !uIManager.isWinInProgress){
            doRestart();


        }
    }
    public void doRestart()
    {
        PlayerPrefs.SetInt("start_type", 0);
        AkSoundEngine.PostEvent("Play_Restart", gameObject);
        getScene();
        SceneManager.LoadScene(buildIndex);

    }
    public void restartLevel()
    {
        
        isRestartInitiated = true;
        restartStartTime = Time.time;


    }

    public void PauseGame(){
        Time.timeScale = 0f;
        uIManager.displayPauseMenu();

    }

    public void unPauseGame(){
        Time.timeScale = 1f;
        uIManager.removePauseMenu();

    }
	public void nextScene(){
        getScene();
        Time.timeScale = 1f;

        SceneManager.LoadScene(buildIndex + 1);


    }

    public void loadScene(string name){
        Time.timeScale = 1f;

        SceneManager.LoadScene(name);

    }

    public void updateBounces(int number)
    {
        collisionLeft = number;

        uIManager.updateBouncesLeft(number);

    }
    void getScene()
    {
        sceneName = SceneManager.GetActiveScene().name;
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
