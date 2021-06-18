using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public string AppID;
    public string RewardedAdsId;
    
    public static AdManager Instance;

    private RewardedAd RewardedAd;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        MobileAds.Initialize(initStatus => { });

        this.CreateRewardedAd(CreateRequest());
    }

    private AdRequest CreateRequest()
    {
        AdRequest request;

        request = new AdRequest.Builder().Build();

        return request;
    }

    public bool IsRewardedAdLoaded()
    {
        return this.RewardedAd.IsLoaded();
    }

    #region RewardedAd
    private void OnUserEarnedReward(object sender, Reward e)
    {
        GameSettings.Instance.RestartGame();
    }

    public void CreateRewardedAd(AdRequest request)
    {
        this.RewardedAd = new RewardedAd(RewardedAdsId);
        this.RewardedAd.LoadAd(request);
        RewardedAd.OnUserEarnedReward += OnUserEarnedReward;
    }

    public void ShowRewardedAd()
    {
        if (IsRewardedAdLoaded())
        {
            this.RewardedAd.Show();
        }
        else GameSettings.Instance.RestartGame();

        this.CreateRewardedAd(CreateRequest());
        
    }
    #endregion

}
