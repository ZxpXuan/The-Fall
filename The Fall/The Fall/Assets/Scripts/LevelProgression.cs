using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class LevelProgression : MonoBehaviour {

    PlayerPrefs playerPrefs;
    int levelsUnlocked;
    [SerializeField]
    List<Button> levelButtons;

    [SerializeField]
    public bool unlockAllLevels;

    private BannerView bannerView;
    // Use this for initialization
    void Start () {

      


       // this.RequestBanner();


        foreach (Button bt in levelButtons)
        {
            bt.interactable = false;

        }
        levelsUnlocked=  PlayerPrefs.GetInt("levelsUnlocked", 1);
        int i = 0;
        while(i<levelsUnlocked){

            levelButtons[i].interactable = true;
            i++;
        }
        if(unlockAllLevels || PlayerPrefs.GetString("levelsUnlockedTrue","False")=="True"){
            foreach(Button bt in levelButtons){
                bt.interactable = true;

            }
        }
	}
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1149253882244477/4519831455";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.bannerView.LoadAd(request);
        this.bannerView.Show();
    }
    public void removeBanner()
    {
       // this.bannerView.Hide();
      //  this.bannerView.Destroy();

    }
    public void UnlockAllLevels()
    {
        
       
        PlayerPrefs.SetString("levelsUnlockedTrue", "True");
        foreach (Button bt in levelButtons)
        {
            bt.interactable = true;

        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
