using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

using System;

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


   // Soundbank_Manager sbm;

    [SerializeField]
    public int currentWorldTries;

    public bool hasBallBeenShot;

    public List<Vector3> contactPoints;

    public AI ai;
    private RewardedAd rewardBasedVideo;

    public LineRenderer lr;

    public Transform HintEndPoint;

    public bool hasCollided = false;
    public bool isAdOpened;
    private BannerView bannerView;
    public InterstitialAd interstital;
    // Use this for initialization
    public GameObject InGameUI;
        
    
    public void HandleOnAdEarned(object sender, EventArgs args)
    {
        ShowHint();
        isAdOpened = false;

    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Ad ", "Ad Opened", "Level" +sceneName);
        
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Ad ", "Ad Closed", "Level" +sceneName);

        isAdOpened = false;


    }
    public void OnBannerAdLoaded(object sender, EventArgs args)
    {
        bannerView.Show();


    }

    private void Awake()
    {
        
#if UNITY_ANDROID
        string appId = "ca-app-pub-1149253882244477~7771421578";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        // Create an empty ad request.
      //  RequestInterstital();
      //  RequestBanner();
      //  this.bannerView.OnAdLoaded += OnBannerAdLoaded;
      //  bannerView.Show();
        ai = GetComponent<AI>();


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
            if (GetComponent<TutorialManager>() != null)
            {

                GetComponent<TutorialManager>().enabled = false;
            }
            PlayerPrefs.SetInt("currentLevelTries", PlayerPrefs.GetInt("currentLevelTries", 0) + 1 );

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
                //lr.SetColors(Color.gray, Color.blue);
                lr.startColor = Color.green;
                lr.endColor = Color.blue;
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

            PlayerPrefs.SetInt("currentLevelTries", 0);
        }

        if (PlayerPrefs.GetInt("currentLevelTries", 0) % 5 == 0 && PlayerPrefs.GetInt("currentLevelTries", 0) >1)
        {

            RequestInterstital();
        }
    }
    void Start () {
        //this.animation.Play();
        sceneName = SceneManager.GetActiveScene().name;
        lim = FindObjectOfType<limitation>();
        if (PlayerPrefs.GetInt("HintShown" + sceneName, 0) == 1)
        {

            ShowHint();
        }
     //   sbm = FindObjectOfType<Soundbank_Manager>();
        isRestartInitiated = false;
       // Cursor.visible = false;
        isPaused = false;
        if(lim!=null)
        collisionLeft = lim.limit;



    }
    public void soundBankManagerFunction()
    {
      //  sbm = FindObjectOfType<Soundbank_Manager>();


      //  sbm.MenuSystem();
    }

    public void HideHint()
    {
       // PlayerPrefs.SetInt("HintShown" + sceneName, 1);

        LineRenderer lr = transform.GetChild(3).GetComponent<LineRenderer>();
        lr.gameObject.SetActive(false);



    }

    public void ShowHintAd()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            isAdOpened = true;
            this.rewardBasedVideo.Show();
        }
        
    }

	// Update is called once per frame
	void Update () {    

        if(isRestartInitiated&& restartStartTime + restartDelay <Time.time  && !hasCollided){
            doRestart();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            ToggleGame();
        }

    }
    public void doRestart()
    {


        PlayerPrefs.SetInt("start_type", 0);
      //  AkSoundEngine.PostEvent("Play_Restart", gameObject);
       // AkSoundEngine.PostEvent("Stop_Goal_Static", gameObject);

      //  AkSoundEngine.PostEvent("Stop_Aiming", gameObject);
        
        PlayerPrefs.SetInt("currentWorldTries", currentWorldTries + 1);
        PlayerPrefs.SetInt("totalWorldTries", PlayerPrefs.GetInt("totalWorldTries", 0) + 1);
        //   PlayerPrefs.SetInt("hasBallBeenShot", 0);
        GameObject.Find("AdManager").GetComponent<AdManager>().DestroyAd();
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
    public void ToggleGame()
    {
        if (isPaused)
        {
            GameObject.Find("AdManager").GetComponent<AdManager>().ShowAd();
            isPaused = false;
            unPauseGame();
        }
        else
        {
            GameObject.Find("AdManager").GetComponent<AdManager>().DestroyAd();

            isPaused = true;
            PauseGame();
        }

    }

    public void RateGame()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.thefall.team7anulagarwalp");
    }
    public void ShowHint()
    {

        PlayerPrefs.SetInt("HintShown" + sceneName, 1);


        Vector3 redPos = lim.transform.position;
        Vector3 bluePos = HintEndPoint.position;
        Vector3 dir = bluePos - redPos;
        float distance = Vector3.Distance(redPos, bluePos);
        float maxDistance = 3;
        float variableDistance = 3 / distance;
        //  Vector3 oneThird = redPos + dir * (distance *varible);

        float radius = 3; //radius of *black circle*
        Vector3 centerPosition = lim.transform.position; //center of *black circle*
        float distancea = Vector3.Distance(HintEndPoint.position, centerPosition); //distance from ~green object~ to *black circle*

        if (distance > radius) //If the distance is less than the radius, it is already within the circle.
        {
            Vector3 fromOriginToObject = HintEndPoint.position - centerPosition; //~GreenPosition~ - *BlackCenter*
            fromOriginToObject *= radius / distance; //Multiply by radius //Divide by Distance
            HintEndPoint.position = centerPosition + fromOriginToObject; //*BlackCenter* + all that Math
        }



        Vector3.Angle(lim.transform.position, HintEndPoint.position) ;
        LineRenderer lr =  transform.GetChild(3).GetComponent<LineRenderer>();
       lr.gameObject.SetActive(true);

        lr.startWidth = 0.1f;
        lr.endWidth = 0.2f;
        //lr.SetPosition(0, HintEndPoint.position);
        lr.SetPosition(0, HintEndPoint.position);
        lr.SetPosition(1, lim.transform.position);
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Hint ", "Hint Shown", "Level" + sceneName);

    }
    public void nextScene(){
        getScene();
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("hasBallBeenShot", 0);
        switch (ai.currentMood)
        {
            case Brain.MoodTypes.VDisappointed:
                PlayerPrefs.SetInt("AIMood",0);
                break;
            case Brain.MoodTypes.Disappointed:
                PlayerPrefs.SetInt("AIMood", 1);
                break;
            case Brain.MoodTypes.Neutral:
                PlayerPrefs.SetInt("AIMood", 2);
                break;
            case Brain.MoodTypes.Happy:
                PlayerPrefs.SetInt("AIMood", 3);
                break;

            case Brain.MoodTypes.VHappy:
                PlayerPrefs.SetInt("AIMood", 4);
                break;
        }


        GameObject.Find("AdManager").GetComponent<AdManager>().DestroyAd();

        // interstitial.Destroy();
        //  PlayerPrefs.SetInt("start_type", 1);
        SceneManager.LoadScene(buildIndex + 1);

    }

    public void loadScene(string name){
        //d  sbm = FindObjectOfType<Soundbank_Manager>();
      //  GameObject.Find("AdManager").GetComponent<AdManager>().DestroyAd();

        //sbm.MenuSystem();
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("hasBallBeenShot", 0);

       // PlayerPrefs.SetInt("start_type", 1);
        SceneManager.LoadScene(name);

    }
    public void startSoundBankMusic(int level)
    {
     //   sbm = FindObjectOfType<Soundbank_Manager>();


     //   sbm.startMusic(level);
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
        InGameUI.SetActive(false);
        uIManager.displayWin();
    }




    private void RequestRewareded()
    {

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1149253882244477/7085025864";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        this.rewardBasedVideo = new RewardedAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        this.rewardBasedVideo.LoadAd(request);


        this.rewardBasedVideo.OnAdOpening += HandleOnAdOpened;
        this.rewardBasedVideo.OnAdClosed += HandleOnAdClosed;
        this.rewardBasedVideo.OnUserEarnedReward += HandleOnAdEarned;
    }
    private void RequestInterstital()
    {

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1149253882244477/8526578976";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstital = new InterstitialAd(adUnitId);

        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstital.LoadAd(request);
        if (this.interstital.IsLoaded())
        {

            this.interstital.Show();
        }

    }

    private void RequestBanner()
    {


#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1149253882244477/4519831455";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.bannerView.LoadAd(request);
    }

}
