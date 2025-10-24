using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public InitializeAds initializeAds;
    public BannerAds bannerAds;
    public InterstitialAds interstitialAds;
    public RewardedAds rewardedAds;

    [SerializeField] private float _loadBannerTime = 5f;
    [SerializeField] private float _showBannerTime = 30f;
    [SerializeField] private float _hideBannerTime = 30f;

    public static AdsManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        rewardedAds.LoadRewardedAd();
        StartCoroutine(BannerAd());
        interstitialAds.LoadInterstitialAd();

    }

    public void ShowInterstitialAd()
    {
        interstitialAds.ShowInterstitialAd();
    }

    public void ShowRewardedAd()
    {
        rewardedAds.ShowRewardedAd();
    }

    IEnumerator BannerAd()
    {
        while (true) 
        {
            bannerAds.LoadBannerAd();
            yield return new WaitForSeconds(_loadBannerTime);
            bannerAds.ShowBannerAd();
            yield return new WaitForSeconds(_showBannerTime);
            bannerAds.HideBannerAd();
            yield return new WaitForSeconds(_hideBannerTime);
        }
    }
}

