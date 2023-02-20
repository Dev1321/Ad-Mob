using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Ads : MonoBehaviour
{
    BannerView BannerView;
    private  InterstitialAd interstitial;
    private string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
    string adUnitId = "ca-app-pub-3940256099942544/6300978111";




    void Start()
    {
        MobileAds.Initialize(initStatus => { });
       RequestInterstitial();
    }

   

 public void RunInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            RequestInterstitial();
        }
        else
        {
            RequestInterstitial();
        }
    }
    public void ShowBanner()
    {
        RequestBanner();
    }

    public void HideBanner()
    {
        BannerView.Destroy();
        BannerView = null;
    }


    private void RequestBanner()
    {
#if UNITY_ANDROID
#endif
        this.BannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

        Debug.Log("Loading banner ad.");
        BannerView.LoadAd(adRequest);
    }
    private void RequestInterstitial()
    {
        this.interstitial = new InterstitialAd(_adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
}



