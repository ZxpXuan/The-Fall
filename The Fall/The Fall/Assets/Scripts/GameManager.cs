using System.Collections;
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
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
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
            getScene();

            SceneManager.LoadScene(buildIndex);
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

        SceneManager.LoadScene(buildIndex + 1);


    }

    public void loadScene(string name){

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
