using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {
    [SerializeField]
    public GameObject winScreen;

    [SerializeField]
    public GameObject loseScreen;

    [SerializeField]
    public Text bouncesLeft;

    [SerializeField]
    public GameObject pauseMenu;

    [SerializeField]
    public GameManager gm;

    public bool isWinInProgress;
	// Use this for initialization
	void Start () {
        gm = GetComponent<GameManager>();
        updateBouncesLeft(gm.collisionLeft);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateBouncesLeft(int number){
        if(bouncesLeft!=null)
        bouncesLeft.text = "Bounces Left:" + number;

    }
    public void displayPauseMenu(){


        pauseMenu.SetActive(true);
    }
    public void removePauseMenu(){

        pauseMenu.SetActive(false);

    }
    public void displayWin(){

        winScreen.SetActive(true);
       // Time.timeScale = 0f;
        isWinInProgress = true;
    }

    public void displayLose(){

        loseScreen.SetActive(true);
       // Time.timeScale = 0f;

    }
}
