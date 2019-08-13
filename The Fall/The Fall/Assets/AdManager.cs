using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class AdManager : MonoBehaviour
{

    private BannerView bannerView;

    public bool ShowAds= true;
    // Start is called before the first frame update
    void Start()
    {
        if (ShowAds)
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
            ShowAd();
        }
       // bannerView.Show();
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
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
    }
    // Update is called once per frame
public void DestroyAd()
    {
        if(ShowAds)
        bannerView.Hide();
      //  bannerView.Destroy();
      //  Destroy(this);
    }
    public void ShowAd()
    {
        if (ShowAds)
        {
            this.RequestBanner();

            AdRequest request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            bannerView.LoadAd(request);
            bannerView.Show();
        }
    }
}
