using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobManager : MonoBehaviour
{
    private static AdMobManager instance;

    private string appID = "ca-app-pub-2161417377739089~9454254081";
    private string interstitialAdID = "ca-app-pub-2161417377739089/4807258602";


    private InterstitialAd interstitial;

    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(instance);
    }

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public void RequestInterstitial()
    {

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(interstitialAdID);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

    }

    public void LevelComplete()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            Debug.Log("congrats");
            interstitial.Destroy();

        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RequestInterstitial();
            LevelComplete();
        }
    }
}
