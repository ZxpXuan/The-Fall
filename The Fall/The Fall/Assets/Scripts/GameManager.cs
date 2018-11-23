using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class GameManager : MonoSingleton<GameManager> {
   //private bool paused = false;
    private int buildIndex;
    private string sceneName;  

    public UIManager uIManager;

    public bool isPaused;

    public int collisionLeft;

    public limitation lim;

    private float pau = 0;

    public float restartDelay;
    private float restartStartTime;
    private bool isRestartInitiated;

   // [SerializeField]
    //List<Animator> objectToDisable;


    Soundbank_Manager sbm;

    [SerializeField]
    public int currentWorldTries;

    public bool hasBallBeenShot;

    public List<Vector3> contactPoints;
    // Use this for initialization
    
    private void Awake()
    {

        levelmanager[] lm = FindObjectsOfType<levelmanager>();

        int x = 0;
        while (x < lm.Length){

            lm[x].uIManager = GetComponent<UIManager>();

            x++;
        }
        lim = FindObjectOfType<limitation>();
        currentWorldTries = 0;
        hasBallBeenShot = false;
        if (PlayerPrefs.GetInt("start_type", 99) == 0)
        {
            Animator[] anim= FindObjectsOfType<Animator>();
            int i = 0;
            while(i< anim.Length)
            {
                anim[i].enabled = false;

                i++;
            }

           // //foreach (Animator anima in objectToDisable)
           // {
               // anima.enabled = false;

          //  }
            if (PlayerPrefs.GetInt("hasBallBeenShot", 0) == 1)
            {
                LineRenderer lr = GetComponent<LineRenderer>();
                float x1 = PlayerPrefs.GetFloat("xShot", 0);
                float y1 = PlayerPrefs.GetFloat("yShot", 0);
                currentWorldTries = PlayerPrefs.GetInt("currentWorldTries", 0);

                lr.SetPosition(0, new Vector3(x1, y1, 0));
                lr.SetPosition(1, lim.transform.position);

                lr.startWidth = 0.1f;
                lr.endWidth = 0.2f;
             //   PlayerPrefs.SetInt("hasBallBeenShot", 0);

            }
        //    GetComponent<AI>().justSetState(Brain.GameState.Mute);

            PlayerPrefs.SetInt("start_type", 1);
        }
        else
        {
     //       GetComponent<AI>().justSetState(Brain.GameState.Start);


        }
    }
    void Start () {
        sbm = FindObjectOfType<Soundbank_Manager>();
        isRestartInitiated = false;
       // Cursor.visible = false;
        isPaused = false;
        if(lim!=null)
        collisionLeft = lim.limit;



    }
    public void soundBankManagerFunction()
    {
        sbm = FindObjectOfType<Soundbank_Manager>();


        sbm.MenuSystem();
    }
	// Update is called once per frame
	void Update () {

        if (Time.timeScale == 0)
        {
            pau = 1;
        }

        if (Time.timeScale == 1)
        {
            pau = 0;
        }


        if(Input.GetKeyDown(KeyCode.S))
        {
            Cursor.visible = true;

        }


            if (Input.GetKeyDown(KeyCode.Space))
            {
            
            Time.timeScale = pau;
               /// paused = true;

            }



            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            
            //    Time.timeScale = 1;
            //   // paused = false;

            //}
        //}
       



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
        if (Input.GetMouseButtonDown(1))
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
        AkSoundEngine.PostEvent("Stop_Goal_Static", gameObject);

        AkSoundEngine.PostEvent("Stop_Aiming", gameObject);
        
        PlayerPrefs.SetInt("currentWorldTries", currentWorldTries + 1);
        PlayerPrefs.SetInt("totalWorldTries", PlayerPrefs.GetInt("totalWorldTries", 0) + 1);
     //   PlayerPrefs.SetInt("hasBallBeenShot", 0);

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
        PlayerPrefs.SetInt("hasBallBeenShot", 0);
        print("nextt");
      //  PlayerPrefs.SetInt("start_type", 1);
        SceneManager.LoadScene(buildIndex + 1);


    }

    public void loadScene(string name){
        sbm = FindObjectOfType<Soundbank_Manager>();

        //sbm.MenuSystem();
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("hasBallBeenShot", 0);

       // PlayerPrefs.SetInt("start_type", 1);
        SceneManager.LoadScene(name);

    }
    public void startSoundBankMusic(int level)
    {
        sbm = FindObjectOfType<Soundbank_Manager>();


        sbm.startMusic(level);
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


    public void winGame()
    {


    }
}
