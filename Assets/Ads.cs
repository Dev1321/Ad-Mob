using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Ads : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
    string adunitId = "ca-app-pub-3940256099942544/6300978111";
    string adUnitId = "ca-app-pub-3940256099942544/5224354917";
    private int counter;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
        RequestRewardedVideoAd();
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
    public void LoadRewardedInterstitial()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
            RequestRewardedVideoAd();
           
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
        interstitial.OnAdPaid += OnAdPaid;
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
    private void OnAdPaid(AdValue adValue)
    {
        Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
    }

    private void RequestRewardedVideoAd()
    {
        this.rewardedAd = new RewardedAd(adUnitId);
        this.rewardedAd.OnUserEarnedReward += EarnReward;
        rewardedAd.OnAdClicked += OnAdClickedreward;
        rewardedAd.OnAdFullScreenContentOpened += FullScreenReward;
        rewardedAd.OnAdFullScreenContentClosed += ClosedFullScreenReward;
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
        rewardedAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
    }

    public void EarnReward(object sender, Reward args)
    {
        //give user reward
        Debug.Log("User Earn 5 points");
    }
    public void OnAdClickedreward()
    {
        Debug.Log("reward ad was clicked.");
    }
    public void FullScreenReward()
    {
        Debug.Log("Reward Ad opened in full screen");
    }
    private void ClosedFullScreenReward()
    {
        Debug.Log("Reward Ad full screen closed");
    }

}