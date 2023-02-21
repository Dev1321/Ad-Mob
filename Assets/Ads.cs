using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Ads : MonoBehaviour
{
    BannerView bannerView;
    private InterstitialAd interstitial;
    private string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
    string adUnitId = "ca-app-pub-3940256099942544/6300978111";
    private object adValue;

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
        bannerView.Destroy();
        bannerView = null;
    }

    private void RequestBanner()
    {
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

        Debug.Log("Loading banner ad.");
        bannerView.LoadAd(adRequest);

        bannerView.OnBannerAdLoaded += OnBannerAdLoaded;
        bannerView.OnAdClicked += OnAdClicked;
        bannerView.OnAdFullScreenContentOpened += OnAdFullScreenContent;
        bannerView.OnAdFullScreenContentClosed += OnAdFullScreenContentClosed;
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
         {
             Debug.LogError("Banner view failed to load an ad with error :" + error);
         };
    }
    private void OnAdFullScreenContent()
    {
        Debug.Log("Banner view full screen content opened.");
    }

    private void OnAdFullScreenContentClosed()
    {
        Debug.Log("Banner view full screen content closed.");
    }
    private void OnAdClicked()
    {
        Debug.Log("Banner view was clicked.");
    }

    private void OnBannerAdLoaded()
    {
        Debug.Log("Banner view loaded an ad with response : "
          + bannerView.GetResponseInfo());
    }
    private void RequestInterstitial()
    {
        this.interstitial = new InterstitialAd(_adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);

        interstitial.OnAdClicked += OnAdClickint;
        interstitial.OnAdFullScreenContentOpened += OnAdFullScreenContentint;
        interstitial.OnAdFullScreenContentClosed += OnAdFullScreenContentClosedint;
    }
    private void OnAdClickint()
    {
        Debug.Log("Interstitial ad was clicked.");
    }
    private void OnAdFullScreenContentint()
    {
        Debug.Log("Interstitial ad full screen content opened.");
    }
    private void OnAdFullScreenContentClosedint()
    {
        Debug.Log("Interstitial ad full screen content closed.");
    }
}